using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models
{
    public class Rol
    {
        [Key]
        public int PkRol { get; set; }
        public string Nombre { get; set; }
    }
}
