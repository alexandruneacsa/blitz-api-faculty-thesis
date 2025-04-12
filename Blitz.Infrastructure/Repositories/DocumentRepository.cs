using Blitz.Domain.Entities;
using Blitz.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Blitz.Infrastructure.Repositories
{
    public class DocumentRepository : IDocument
    {
        private readonly IConfiguration _config;

        public DocumentRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public BlitzContext Context()
        {
            return new BlitzContext(_config);
        }

        public async Task<Document> AddDocumentAsync(Document Document, CancellationToken cancellationToken)
        {
            using var context = Context();
            await context.Documents.AddAsync(Document, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return Document;
        }

        public async Task<Document?> DeleteDocumentAsync(int id, CancellationToken cancellationToken)
        {
            using var context = Context();
            var pictureToDelete = await GetDocumentByIdAsync(id, cancellationToken);

            if (pictureToDelete != null)
            {
                context.Documents.Remove(pictureToDelete);
                await context.SaveChangesAsync(cancellationToken);
                return pictureToDelete;
            }

            return null;
        }

        public async Task<Document?> GetDocumentByIdAsync(int id, CancellationToken cancellationToken)
        {
            using var context = Context();
            return await context.Documents.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task<Document> UpdateDocumentAsync(Document Document, CancellationToken cancellationToken)
        {
            using var context = Context();
            context.Documents.Update(Document);
            await context.SaveChangesAsync(cancellationToken);
            return Document;
        }

        public async Task<IReadOnlyCollection<Document>> GetDocumentsAsync(CancellationToken cancellationToken)
        {
            using var context = Context();
            return await context.Documents.ToListAsync(cancellationToken);
        }
    }
}