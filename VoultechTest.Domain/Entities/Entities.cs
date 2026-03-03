using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VoultechTest.Domain.Entities
{
    public class Producto
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;
        
        [Range(0.01, double.MaxValue)]
        public decimal Precio { get; set; }
    }

    public class Orden
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Cliente { get; set; } = string.Empty;
        
        public DateTime Fecha { get; set; } = DateTime.Now;
        
        public decimal Total { get; set; }
        
        public List<OrdenProducto> OrdenProductos { get; set; } = new();
    }

    public class OrdenProducto
    {
        public int Id { get; set; }
        public int OrdenId { get; set; }
        public int ProductoId { get; set; }
        
        public Orden? Orden { get; set; }
        public Producto? Producto { get; set; }
    }
}
