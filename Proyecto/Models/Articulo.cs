using System;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models
{
    public class Articulo
    {
        [Key]
        public int PkArticulo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string UrlImg { get; set; }


    }
}
 