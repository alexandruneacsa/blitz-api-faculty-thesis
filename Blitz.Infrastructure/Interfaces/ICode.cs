using Blitz.Domain.Entities;

namespace Blitz.Infrastructure.Interfaces
{
    public interface ICode
    {
        Task<List<Code>> AddCodesAsync(List<Code> codes, CancellationToken cancellationToken);
        Task AddCodesAsync2(List<Code> codes);


        Task<IReadOnlyCollection<Code>> GetCodesAsync();
        Task<Code> UpdateCodeAsync(Code code);
        Task<Code> GetCodeByIdAsync(int id);
        Task<Code> DeleteCodeAsync(int id);
        Task<Code> AddCodeAsync(Code code);
    }
}