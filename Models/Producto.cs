using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CasaDeEmpeño.Models
{
    public class Producto
    {
        public int ProductoID { get; set; }
        public string TipoProducto { get; set; }
        public string NombreProducto { get; set; }
        public bool EstadoRegistro { get; set; }
        public string EstadoProducto { get; set; }
        public DateTime FechaIngreso { get; set; }
        public decimal ValorCalculado { get; set; }
        public DateTime FechaVencimientoDevolucion { get; set; }
        public virtual ICollection<Oferta> Ofertas { get; set; }
        public virtual ICollection<Devolucion> Devoluciones { get; set; }
    }
}
