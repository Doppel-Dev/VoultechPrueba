using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoultechTest.Api.Attributes;
using VoultechTest.Api.DTOs;
using VoultechTest.Domain.Entities;
using VoultechTest.Domain.Interfaces;

namespace VoultechTest.Api.Controllers
{
    [ApiController]
    [Route("ordenes")]
    [ApiKey]
    public class OrdenesController : ControllerBase
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IDescuentoService _descuentoService;

        public OrdenesController(IOrdenRepository ordenRepository, IProductoRepository productoRepository, IDescuentoService descuentoService)
        {
            _ordenRepository = ordenRepository;
            _productoRepository = productoRepository;
            _descuentoService = descuentoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenResponseDto>>> GetAll([FromQuery] int pagina = 1, [FromQuery] int tamañoPagina = 10)
        {
            if (pagina < 1) pagina = 1;
            if (tamañoPagina < 1 || tamañoPagina > 100) tamañoPagina = 10;

            var ordenes = await _ordenRepository.GetPagedAsync(pagina, tamañoPagina);
            return Ok(ordenes.Select(MapToDto));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenResponseDto>> GetById(int id)
        {
            var orden = await _ordenRepository.GetByIdAsync(id);
            if (orden == null) return NotFound();
            return Ok(MapToDto(orden));
        }

        [HttpPost]
        public async Task<ActionResult<OrdenResponseDto>> Create(OrdenCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var productos = new List<Producto>();
            foreach (var productoId in dto.ProductoIds)
            {
                var producto = await _productoRepository.GetByIdAsync(productoId);
                if (producto == null) return BadRequest($"El producto con ID {productoId} no existe.");
                productos.Add(producto);
            }

            var orden = new Orden
            {
                Cliente = dto.Cliente,
                Fecha = System.DateTime.Now,
                Total = _descuentoService.CalcularTotal(productos),
                OrdenProductos = productos.Select(p => new OrdenProducto { ProductoId = p.Id }).ToList()
            };

            await _ordenRepository.AddAsync(orden);
            
            var ordenGuardada = await _ordenRepository.GetByIdAsync(orden.Id);
            return CreatedAtAction(nameof(GetById), new { id = orden.Id }, MapToDto(ordenGuardada!));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrdenUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var orden = await _ordenRepository.GetByIdAsync(id);
            if (orden == null) return NotFound();

            var productos = new List<Producto>();
            foreach (var productoId in dto.ProductoIds)
            {
                var producto = await _productoRepository.GetByIdAsync(productoId);
                if (producto == null) return BadRequest($"El producto con ID {productoId} no existe.");
                productos.Add(producto);
            }

            orden.Cliente = dto.Cliente;
            orden.Total = _descuentoService.CalcularTotal(productos);
            orden.OrdenProductos = productos.Select(p => new OrdenProducto { OrdenId = id, ProductoId = p.Id }).ToList();

            await _ordenRepository.UpdateAsync(orden);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var orden = await _ordenRepository.GetByIdAsync(id);
            if (orden == null) return NotFound();

            await _ordenRepository.DeleteAsync(id);
            return NoContent();
        }

        private static OrdenResponseDto MapToDto(Orden orden)
        {
            return new OrdenResponseDto
            {
                Id = orden.Id,
                Cliente = orden.Cliente,
                Fecha = orden.Fecha,
                Total = orden.Total,
                Productos = orden.OrdenProductos.Select(op => new ProductoResponseDto
                {
                    Id = op.Producto!.Id,
                    Nombre = op.Producto.Nombre,
                    Precio = op.Producto.Precio
                }).ToList()
            };
        }
    }
}
