using System.Threading.Tasks;
using MoneySmart.Domain;

namespace MoneySmart.Data
{
    public interface IAsyncRepository<T> where T : Entity
    {
        Task<T> GetByIdAsync(long id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
    }
}