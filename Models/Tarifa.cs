using System.ComponentModel.DataAnnotations;

namespace GEPARK.Models
{
    public class Tarifa
    {
        [Key]
        public int IdTarifa { get; set; }
        public double? ValorHora { get; set; }
        public double? ValorTicketPerdido { get; set; }
        public int? TiempoSalida { get; set; }
        public int? CantidadPuestos { get; set; }
    }
}
