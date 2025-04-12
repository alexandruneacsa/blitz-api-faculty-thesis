using Blitz.Domain.Entities;
using Blitz.Infrastructure.Helpers;
using Blitz.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Transactions;

namespace Blitz.Infrastructure.Repositories
{
    public class CodeRepository : ICode
    {
        private readonly IConfiguration _config;

        public CodeRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public BlitzContext Context()
        {
            return new BlitzContext(_config);
        }

        public async Task<Code> AddCodeAsync(Code code)
        {
            using (var context = Context())
            {
                await context.Codes.AddAsync(code);
                await context.SaveChangesAsync();
            }
            return code;
        }

        public async Task<Code> DeleteCodeAsync(int id)
        {
            var codeToDelete = await GetCodeByIdAsync(id);

            if (codeToDelete != null)
            {
                using (var context = Context())
                {
                    context.Codes.Remove(codeToDelete);
                    await context.SaveChangesAsync();
                }
                return codeToDelete;
            }

            return null;
        }

        public async Task<Code> GetCodeByIdAsync(int id) => await Context().Codes
             .Where(u => u.Id == id)
             .FirstOrDefaultAsync();

        public async Task<Code> UpdateCodeAsync(Code code)
        {
            using (var context = Context())
            {
                context.Codes.Update(code);
                await context.SaveChangesAsync();
            }
            return code;
        }

        public async Task<IReadOnlyCollection<Code>> GetCodesAsync() =>  await Context().Codes.ToListAsync();

        public async Task<List<Code>> AddCodesAsync(List<Code> codes, CancellationToken cancellationToken)
        {
            using var context = Context();
            var codesBatches = InsertBatchHelper.SplitIntoBatches(codes, 4);

            await Parallel.ForEachAsync(codesBatches, async (items, ca) =>
            {
                var offset = 0;
                var pageSize = 50;
                var batch = items.Skip(offset).Take(pageSize).ToList();

                do
                {
                    using var transactionScope = new TransactionScope();

                    await context.Codes.AddRangeAsync(batch, cancellationToken);
                    await context.SaveChangesAsync(cancellationToken);

                    offset += pageSize;

                    batch = codes.Skip(offset).Take(pageSize).ToList();
                    transactionScope.Complete();
                }
                while (batch.Count > 0);
            });

            return codes;
        }

        public async Task AddCodesAsync2(List<Code> codes)
        {
            var batches = InsertBatchHelper.SplitIntoBatches(codes, 50);
            
            await Parallel.ForEachAsync(batches, async (batch, ca) =>
            {
                using var context = Context();
                using var transaction = await context.Database.BeginTransactionAsync();
                try
                {
                    foreach (var entity in batch)
                    {
                        await context.Codes.AddAsync(entity, ca);
                    }
                    await context.SaveChangesAsync(ca);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error inserting batch: {ex.Message}");
                    transaction.Rollback();
                }
            });
        }
    }
}
