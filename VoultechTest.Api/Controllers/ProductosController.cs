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
    [Route("productos")]
    [ApiKey]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoRepository _productoRepository;

        public ProductosController(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoResponseDto>>> GetAll()
        {
            var productos = await _productoRepository.GetAllAsync();
            return Ok(productos.Select(p => new ProductoResponseDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Precio = p.Precio
            }));
        }

        [HttpPost]
        public async Task<ActionResult<ProductoResponseDto>> Create(ProductoCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var producto = new Producto { Nombre = dto.Nombre, Precio = dto.Precio };
            await _productoRepository.AddAsync(producto);
            
            return CreatedAtAction(nameof(GetAll), new { id = producto.Id }, new ProductoResponseDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio
            });
        }
    }
}
