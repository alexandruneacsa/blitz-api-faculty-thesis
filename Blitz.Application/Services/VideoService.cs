using Blitz.Application.Dtos;
using Blitz.Application.Helpers;
using Blitz.Application.Interfaces;
using Blitz.Domain.Entities;
using Blitz.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MimeTypes.Core;
using PexelsDotNetSDK.Api;
using PexelsDotNetSDK.Models;
using System.IO.Compression;

namespace Blitz.Application.Services
{
    public class VideoService : IVideoService
    {
        private readonly IDocument _picture;
        private readonly IConfiguration _configuration;

        public VideoService(IDocument Document, IConfiguration configuration)
        {
            _picture = Document;
            _configuration = configuration;
        }

        public async Task<MemoryStream> GetVideosAsync(string query, CancellationToken cancellationToken)
        {
            var data = await FetchVideosExternal(query);

            using MemoryStream ms = new();
            var tasksList = new List<Task>();

            using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
            {
                foreach (var task in data.videos)
                {
                    var entry = zip.CreateEntry(Guid.NewGuid().ToString() + ".mp4");
                    var videoLink = task.videoFiles.FirstOrDefault(x => x.quality.Equals("hd"))!.link;
                    var bytes = await ApiHelper.GetByteFromUrl(videoLink);
                    using var entryStream = entry.Open();

                    tasksList.Add(entryStream.WriteAsync(bytes, 0, bytes.Length, cancellationToken));
                }
                await Task.WhenAll(tasksList);
            }

            return ms;
        }

        public async Task<VideoPage> FetchVideosExternal(string query, string orientation = "", string size = "", string locale = "", int page = 1, int pageSize = 15)
        {
            var pexelsClient = new PexelsClient(_configuration["PexelsClientToken"]);
            var result = await pexelsClient.SearchVideosAsync(query, orientation, size, locale, page, pageSize);

            return result;
        }

        public async Task<BlitzWrapper<List<VideoView>>> CreateVideosAsync(List<IFormFile> formFiles)
        {
            var result = new List<VideoView>();
            var success = true;

            try
            {
                await Parallel.ForEachAsync(formFiles, async (file, token) =>
                {
                    if (!success)
                    {
                        return;
                    }

                    var fileType = MimeTypeMap.GetExtension(file.ContentType);
                    var Document = new Document()
                    {
                        Name = Guid.NewGuid().ToString(),
                        Extension = fileType,
                        ContentType = file.ContentType,
                    };

                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream, token);
                        Document.Image = stream.ToArray();
                    }

                    var addedPicture = _picture.AddDocumentAsync(Document, token);
                    var location = string.Format("{0}{1}{2}{1}", _configuration["StoragePath"], Path.DirectorySeparatorChar, "Videos");

                    if (!Directory.Exists(location))
                        Directory.CreateDirectory(location);

                    await addedPicture.ContinueWith(async x => {
                        if (x.IsFaulted)
                        {
                            success = false;
                            return;
                        }

                        await File.WriteAllBytesAsync($"{location}{Document.Name}.{Document.Extension}", Document.Image, token);
                    });

                    if (addedPicture.IsCompletedSuccessfully)
                    {
                        result.Add(new VideoView
                        {
                            ContentType = Document.ContentType,
                            Extension = Document.Extension,
                            Id = addedPicture.Id,
                            ContentByte = addedPicture.Result.Image,
                            Name = addedPicture.Result.Name
                        });
                    }
                });

                return new BlitzWrapper<List<VideoView>> { StatusCode = 200, ObjectResponse = result, ErrorMessage = "Successfull" };
            }
            catch (OperationCanceledException ex2)
            {
                return new BlitzWrapper<List<VideoView>> { StatusCode = 404, ObjectResponse = null, ErrorMessage = "Could not add the file on database or disk.\nThe task was cancelled" };
            }
            catch (Exception ex)
            {
                return new BlitzWrapper<List<VideoView>> { StatusCode = 404, ObjectResponse = null, ErrorMessage = ex.Message };
            }
        }
    }
}
