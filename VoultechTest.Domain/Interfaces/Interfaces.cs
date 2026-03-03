using System.Collections.Generic;
using System.Threading.Tasks;
using VoultechTest.Domain.Entities;

namespace VoultechTest.Domain.Interfaces
{
    public interface IProductoRepository
    {
        Task<Producto?> GetByIdAsync(int id);
        Task<IEnumerable<Producto>> GetAllAsync();
        Task AddAsync(Producto producto);
    }

    public interface IOrdenRepository
    {
        Task<Orden?> GetByIdAsync(int id);
        Task<IEnumerable<Orden>> GetAllAsync();
        Task<IEnumerable<Orden>> GetPagedAsync(int pageNumber, int pageSize);
        Task AddAsync(Orden orden);
        Task UpdateAsync(Orden orden);
        Task DeleteAsync(int id);
    }

    public interface IDescuentoService
    {
        decimal CalcularTotal(IEnumerable<Producto> productos);
    }
}
