using Blitz.Domain.Entities;

namespace Blitz.Infrastructure.Interfaces
{
    public interface IDocument
    {
        Task<Document> AddDocumentAsync(Document entity, CancellationToken cancellationToken);
        Task<Document?> GetDocumentByIdAsync(int id, CancellationToken cancellationToken);
        Task<Document> UpdateDocumentAsync(Document entity, CancellationToken cancellationToken);
        Task<Document?> DeleteDocumentAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Document>> GetDocumentsAsync(CancellationToken cancellationToken);
    }
}