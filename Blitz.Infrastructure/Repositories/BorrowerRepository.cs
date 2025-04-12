using Blitz.Domain.Entities;
using Blitz.Infrastructure.Helpers;
using Blitz.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Transactions;

namespace Blitz.Infrastructure.Repositories
{
    public class BorrowerRepository : IBorrower
    {
        private readonly IConfiguration _config;

        public BorrowerRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public BlitzContext Context()
        {
            return new BlitzContext(_config);
        }

        public async Task<Borrower> AddBorrowerAsync(Borrower borrower)
        {
            using (var context = Context())
            {
                await context.Borrowers.AddAsync(borrower);
                await context.SaveChangesAsync();
            }

            return borrower;
        }

        public async Task<Borrower> DeleteBorrowerAsync(int id)
        {
            var borrowerToDelete = await GetBorrowerByIdAsync(id);

            if (borrowerToDelete != null)
            {
                using (var context = Context())
                {
                    context.Borrowers.Remove(borrowerToDelete);
                    await context.SaveChangesAsync();
                }

                return borrowerToDelete;
            }

            return null;
        }

        public async Task<Borrower> GetBorrowerByIdAsync(int id) => await Context().Borrowers
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();

        public async Task<Borrower> UpdateBorrowerAsync(Borrower borrower)
        {
            using (var context = Context())
            {
                context.Borrowers.Update(borrower);
                await context.SaveChangesAsync();
            }

            return borrower;
        }

        public async Task<IReadOnlyCollection<Borrower>> GetBorrowersAsync() => await Context().Borrowers.ToListAsync();

        public async Task<List<Borrower>> AddManyBorrowersAsync(List<Borrower> borrowers,
            CancellationToken cancellationToken)
        {
            using var context = Context();
            var borrowersBatches = InsertBatchHelper.SplitIntoBatches(borrowers, 4);

            await Parallel.ForEachAsync(borrowersBatches, async (items, ca) =>
            {
                var offset = 0;
                var pageSize = 50;
                var batch = items.Skip(offset).Take(pageSize).ToList();

                do
                {
                    using var transactionScope = new TransactionScope();

                    await context.Borrowers.AddRangeAsync(batch, cancellationToken);
                    await context.SaveChangesAsync(cancellationToken);

                    offset += pageSize;

                    batch = borrowers.Skip(offset).Take(pageSize).ToList();
                    transactionScope.Complete();
                } while (batch.Count > 0);
            });

            return borrowers;
        }

        public async Task AddMultipleBorrowersAsync(List<Borrower> borrowers)
        {
            var pageSize = 500;
            var batches = InsertBatchHelper.SplitIntoBatches(borrowers, pageSize);
            var timer = new Stopwatch();

            timer.Start();

            await Parallel.ForEachAsync(batches, async (batch, ca) =>
            {
                var pageSize = 100;
                var offset = 0;
                var tempBatch = batch.Skip(offset).Take(pageSize).ToList();

                do
                {
                    using var context = Context();
                    using var transaction = await context.Database.BeginTransactionAsync();

                    try
                    {
                        await context.Borrowers.AddRangeAsync(tempBatch, ca);
                        await context.SaveChangesAsync();

                        offset += pageSize;

                        tempBatch = batch.Skip(offset).Take(pageSize).ToList();

                        if (!tempBatch.Any())
                        {
                            break;
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error inserting batch: {ex.Message}");
                        transaction.Rollback();
                    }
                } while (tempBatch.Count > 0);
            });

            timer.Stop();
        } 

        /*
         * Optimized
         */
        public async Task<List<Borrower>> AddHeavyThroughputOfBorrowers(List<Borrower> borrowers,
            CancellationToken cancellationToken)
        {
            var offset = 0;
            var pageSize = 50;
            var batch = borrowers.Skip(offset).Take(pageSize).ToList();
            var timer = new Stopwatch();

            timer.Start();

            do
            {
                using var context = Context();
                using var transaction = await context.Database.BeginTransactionAsync();

                try
                {
                    await context.Borrowers.AddRangeAsync(batch, cancellationToken);
                    await context.SaveChangesAsync();

                    offset += pageSize;

                    batch = borrowers.Skip(offset).Take(pageSize).ToList();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error inserting batch: {ex.Message}");
                    transaction.Rollback();
                }
            } while (batch.Count > 0);

            timer.Stop();
            return borrowers;
        } 
    }
}