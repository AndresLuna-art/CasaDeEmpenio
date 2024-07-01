using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CasaDeEmpeño.Models
{
    public class Oferta
    {
        public int OfertaID { get; set; }
        public int ProductoID { get; set; }
        public string NombreOfertante { get; set; }
        public string NumeroCelular { get; set; }
        public bool EstadoRegistro { get; set; }
        public decimal MontoOferta { get; set; }
        public DateTime FechaOferta { get; set; }
        public virtual Producto Producto { get; set; }
    }

    public class ProductoVendidoViewModel
    {
        public int ProductoID { get; set; }
        public string TipoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string EstadoProducto { get; set; }
        public DateTime FechaIngreso { get; set; }
        public decimal ValorCalculado { get; set; }
        public DateTime FechaVencimientoDevolucion { get; set; }
    }

    public class ProductoConOfertasViewModel
    {
        public int ProductoID { get; set; }
        public string TipoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string EstadoProducto { get; set; }
        public DateTime FechaIngreso { get; set; }
        public decimal ValorCalculado { get; set; }
        public DateTime FechaVencimientoDevolucion { get; set; }
        public List<OfertaViewModel> Ofertas { get; set; }
    }

    public class OfertaViewModel
    {
        public int OfertaID { get; set; }
        public int ProductoID { get; set; }
        public string NombreOfertante { get; set; }
        public string NumeroCelular { get; set; }
        public decimal MontoOferta { get; set; }
        public DateTime FechaOferta { get; set; }
        public string NombreProducto { get; set; }
        public string TipoProducto { get; set; }
        public bool EstadoRegistro { get; set; }
        public string EstadoProducto { get; set; }
        public DateTime FechaIngreso { get; set; }
        public decimal ValorCalculado { get; set; }
        public DateTime FechaVencimientoDevolucion { get; set; }
        public bool EsMejorOferta { get; set; } 
    }

    public class ProductoConOfertasYMejorOfertanteViewModel
    {
        public int ProductoID { get; set; }
        public string NombreProducto { get; set; }
        public string TipoProducto { get; set; }
        public string EstadoProducto { get; set; }
        public DateTime FechaIngreso { get; set; }
        public decimal ValorCalculado { get; set; }
        public DateTime FechaVencimientoDevolucion { get; set; }
        public List<OfertaViewModel> Ofertas { get; set; }
    }

    public class ProductoViewModel
    {
        public int ProductoID { get; set; }
        public string NombreProducto { get; set; }
        public decimal ValorCalculado { get; set; }
        public List<OfertaViewModel> Ofertas { get; set; }
    }


}
