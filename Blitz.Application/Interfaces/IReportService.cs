using Blitz.Application.Dtos;
using Blitz.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Blitz.Application.Interfaces
{
    public interface IReportService
    {
        Task<BlitzWrapper<List<Borrower>>> LoadBorrowersAsync(List<IFormFile> formFiles, CancellationToken cancellationToken);
        Task LoadCodesAsync(List<IFormFile> formFiles);
    }
}
