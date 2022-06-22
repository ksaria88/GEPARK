using System.ComponentModel.DataAnnotations;

namespace GEPARK.Models
{
    public class TicketPerdido
    {
        [Key]
        public int IdTicketPerdido { get; set; }
        public double Valor { get; set; }
    }
}
