using System.ComponentModel.DataAnnotations;

namespace GEPARK.Models
{
    public class ValidarTicket
    {
        [Key]
        public string? NroTicket { get; set; }
        public DateTime? HoraEntrada { get; set; }
        public double? Tarifa { get; set; }
        public double? ValorPagar { get; set; }
        
    }
}
