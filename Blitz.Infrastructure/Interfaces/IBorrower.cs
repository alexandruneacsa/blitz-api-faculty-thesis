using Blitz.Domain.Entities;

namespace Blitz.Infrastructure.Interfaces
{
    public interface IBorrower
    {
        Task<List<Borrower>> AddManyBorrowersAsync(List<Borrower> borrowers, CancellationToken cancellationToken);
        Task AddMultipleBorrowersAsync(List<Borrower> borrowers);

        Task<IReadOnlyCollection<Borrower>> GetBorrowersAsync();
        Task<Borrower> UpdateBorrowerAsync(Borrower borrower);
        Task<Borrower> GetBorrowerByIdAsync(int id);
        Task<Borrower> DeleteBorrowerAsync(int id);
        Task<Borrower> AddBorrowerAsync(Borrower borrower);

        Task<List<Borrower>> AddHeavyThroughputOfBorrowers(List<Borrower> borrowers,
            CancellationToken cancellationToken);
    }
}