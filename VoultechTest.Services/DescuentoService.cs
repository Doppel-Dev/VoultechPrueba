using System.Collections.Generic;
using System.Linq;
using VoultechTest.Domain.Entities;
using VoultechTest.Domain.Interfaces;

namespace VoultechTest.Services
{
    public class DescuentoService : IDescuentoService
    {
        public decimal CalcularTotal(IEnumerable<Producto> productos)
        {
            var listaProductos = productos.ToList();
            var subtotal = listaProductos.Sum(p => p.Precio);
            
            decimal porcentajeDescuento = 0m;
            
            if (subtotal > 500)
            {
                porcentajeDescuento += 0.10m;
            }
            
            var cantidadProductosDistintos = listaProductos.Select(p => p.Id).Distinct().Count();
            if (cantidadProductosDistintos > 5)
            {
                porcentajeDescuento += 0.05m;
            }
            
            return subtotal * (1 - porcentajeDescuento);
        }
    }
}
