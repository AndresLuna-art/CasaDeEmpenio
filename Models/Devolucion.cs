using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CasaDeEmpeño.Models
{
    public class Devolucion
    {
        public int DevolucionID { get; set; }
        public int ProductoID { get; set; }
        public string Comentario { get; set; }
        public bool EstadoRegistro { get; set; }
        public DateTime FechaDevolucion { get; set; }
        public virtual Producto Producto { get; set; }
    }

    public class DevolucionViewModel
    {
        public int DevolucionID { get; set; }
        public int ProductoID { get; set; }
        public string NombreProducto { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaDevolucion { get; set; }
    }
}
