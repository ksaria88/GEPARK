using System.ComponentModel.DataAnnotations;

namespace GEPARK.Models
{
    public class PagarTicket
    {
		public string? NroTicket { get; set; }
		public double? Valor { get; set; }
		public string? Nombre { get; set; }
		public string? Identidad { get; set; }
		public string? Correo { get; set; }
		public string? Telefono { get; set; }
		public string? Direccion { get; set; }
	}
}
