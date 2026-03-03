using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoultechTest.Domain.Entities;
using VoultechTest.Domain.Interfaces;
using VoultechTest.Infrastructure.Persistence;

namespace VoultechTest.Infrastructure.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly AppDbContext _context;
        public ProductoRepository(AppDbContext context) => _context = context;

        public async Task<Producto?> GetByIdAsync(int id) => await _context.Productos.FindAsync(id);
        public async Task<IEnumerable<Producto>> GetAllAsync() => await _context.Productos.ToListAsync();
        public async Task AddAsync(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
        }
    }

    public class OrdenRepository : IOrdenRepository
    {
        private readonly AppDbContext _context;
        public OrdenRepository(AppDbContext context) => _context = context;

        public async Task<Orden?> GetByIdAsync(int id)
        {
            return await _context.Ordenes
                .Include(o => o.OrdenProductos)
                .ThenInclude(op => op.Producto)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Orden>> GetAllAsync()
        {
            return await _context.Ordenes
                .Include(o => o.OrdenProductos)
                .ThenInclude(op => op.Producto)
                .ToListAsync();
        }

        public async Task<IEnumerable<Orden>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.Ordenes
                .Include(o => o.OrdenProductos)
                .ThenInclude(op => op.Producto)
                .OrderByDescending(o => o.Fecha)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task AddAsync(Orden orden)
        {
            await _context.Ordenes.AddAsync(orden);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Orden orden)
        {
            _context.Ordenes.Update(orden);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var orden = await _context.Ordenes.FindAsync(id);
            if (orden != null)
            {
                _context.Ordenes.Remove(orden);
                await _context.SaveChangesAsync();
            }
        }
    }
}
