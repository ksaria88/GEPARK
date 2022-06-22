using System.ComponentModel.DataAnnotations;

namespace GEPARK.Models
{
    public class Usuario
    {
        [Key]        
        public string? Nombre { get; set; }
        public string? Clave { get; set; }
    }
}
