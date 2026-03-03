using System.Collections.Generic;
using VoultechTest.Domain.Entities;
using VoultechTest.Services;
using Xunit;

namespace VoultechTest.Tests
{
    public class DescuentoServiceTests
    {
        private readonly DescuentoService _descuentoService;

        public DescuentoServiceTests()
        {
            _descuentoService = new DescuentoService();
        }

        [Fact]
        public void CalcularTotal_SinDescuento_CuandoTotalMenorA500YpocosProductos()
        {
            var productos = new List<Producto>
            {
                new Producto { Id = 1, Precio = 100 },
                new Producto { Id = 2, Precio = 200 }
            };

            var total = _descuentoService.CalcularTotal(productos);

            Assert.Equal(300m, total);
        }

        [Fact]
        public void CalcularTotal_Descuento10Porciento_CuandoTotalMayorA500()
        {
            var productos = new List<Producto>
            {
                new Producto { Id = 1, Precio = 600 }
            };

            var total = _descuentoService.CalcularTotal(productos);

            Assert.Equal(540m, total);
        }

        [Fact]
        public void CalcularTotal_Descuento5PorcientoAdicional_CuandoMasDe5ProductosDistintos()
        {
            var productos = new List<Producto>
            {
                new Producto { Id = 1, Precio = 10 },
                new Producto { Id = 2, Precio = 10 },
                new Producto { Id = 3, Precio = 10 },
                new Producto { Id = 4, Precio = 10 },
                new Producto { Id = 5, Precio = 10 },
                new Producto { Id = 6, Precio = 10 }
            };

            var total = _descuentoService.CalcularTotal(productos);

            Assert.Equal(57m, total);
        }

        [Fact]
        public void CalcularTotal_Descuento15Porciento_CuandoAmbasReglasAplican()
        {
            var productos = new List<Producto>
            {
                new Producto { Id = 1, Precio = 100 },
                new Producto { Id = 2, Precio = 100 },
                new Producto { Id = 3, Precio = 100 },
                new Producto { Id = 4, Precio = 100 },
                new Producto { Id = 5, Precio = 100 },
                new Producto { Id = 6, Precio = 100 }
            };

            var total = _descuentoService.CalcularTotal(productos);

            Assert.Equal(510m, total);
        }
    }
}
