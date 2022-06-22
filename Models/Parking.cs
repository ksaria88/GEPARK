using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GEPARK.Models
{
    public class Parking
    {
		[Key]
		public int IdParking { get; set; }
		public string? NroTicket { get; set; }
		public DateTime? HoraEntrada { get; set; }
		public DateTime? HoraSalida { get; set; }
		public DateTime? HoraMaximaSalida { get; set; }
		public bool? Pagado { get; set; }
		public double? Valor { get; set; }
		public string? Nombre { get; set; }
		public string? Identidad { get; set; }
		public string? Correo { get; set; }
		public string? Telefono { get; set; }
		public string? Direccion { get; set; }

		[NotMapped]
		public string? Mensaje { get; set; }
	}
}
