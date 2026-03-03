using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VoultechTest.Api.DTOs
{
    public class ProductoCreateDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;
        
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }
    }

    public class ProductoResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
    }

    public class OrdenCreateDto
    {
        [Required(ErrorMessage = "El cliente es obligatorio")]
        public string Cliente { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "La orden debe tener al menos un producto")]
        [MinLength(1, ErrorMessage = "La orden debe tener al menos un producto")]
        public List<int> ProductoIds { get; set; } = new();
    }

    public class OrdenUpdateDto
    {
        [Required(ErrorMessage = "El cliente es obligatorio")]
        public string Cliente { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "La orden debe tener al menos un producto")]
        [MinLength(1, ErrorMessage = "La orden debe tener al menos un producto")]
        public List<int> ProductoIds { get; set; } = new();
    }

    public class OrdenResponseDto
    {
        public int Id { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public List<ProductoResponseDto> Productos { get; set; } = new();
    }
}
