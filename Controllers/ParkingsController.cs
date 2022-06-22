using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GEPARK.Data;
using GEPARK.Models;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Authorization;

namespace GEPARK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingsController : ControllerBase
    {
        private readonly GEPARKContext _context;

        public ParkingsController(GEPARKContext context)
        {
            _context = context;
        }

        // GET: api/Parkings/RegistrarEntrada
        [HttpGet("RegistrarEntrada"), Authorize]
        public async Task<ActionResult<Parking>> GetRegistrarEntrada()
        {
            Parking parking = new Parking();

            bool entrada = Funcion.DevolverBool(_context.Database.GetConnectionString(), "Exec ValidarEntradaParqueadero");
            if (!entrada)
            {
                parking.Mensaje = "Parqueadero lleno. No puede ingresar.";
                return parking;
            }

            parking.HoraEntrada = DateTime.Now;
            _context.Parking.Add(parking);
            await _context.SaveChangesAsync();
            parking.NroTicket = Funcion.DevolverString(_context.Database.GetConnectionString(),
              String.Format("Select NroTicket From Parking Where idParking={0}", parking.IdParking));
            parking.Mensaje = "Ok";
            return parking;
        }

        // GET: api/Parkings/ValidarTicket
        [HttpGet("ValidarTicket/{nroTicket}"), Authorize]
        public async Task<ActionResult<ValidarTicket>> GetValidarTicket(string nroTicket)
        {
            ValidarTicket respuesta = new ValidarTicket();
            string consulta = String.Format("Exec CalcularValorPagar '{0}'", nroTicket);
            respuesta = _context.ValidarTicket.FromSqlRaw(consulta).ToList()[0];
            return respuesta;
        }

        // GET: api/Parkings/PagarTicket
        [HttpPost("PagarTicket"), Authorize]
        public async Task<ActionResult<Parking>> GetPagarTicket([FromBody] PagarTicket ticket)
        {
            Parking parking = new Parking();
            int tiempoSalida = Funcion.DevolverInt(_context.Database.GetConnectionString(),
                String.Format("Select TiempoSalida From Tarifa"));
            parking = await _context.Parking.Where(p => p.NroTicket == ticket.NroTicket).FirstAsync();

            if (parking.Pagado == true)
                parking.Valor += ticket.Valor;
            else
                parking.Valor = ticket.Valor;

            parking.Nombre = ticket.Nombre;
            parking.Identidad = ticket.Identidad;
            parking.Correo = ticket.Correo;
            parking.Telefono = ticket.Telefono;
            parking.Direccion = ticket.Direccion;
            parking.HoraMaximaSalida = DateTime.Now.AddMinutes(tiempoSalida);
            parking.Pagado = true;
            _context.Entry(parking).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return parking;
        }


        // GET: api/Parkings/RegistrarSalida
        [HttpGet("RegistrarSalida/{nroTicket}"), Authorize]
        public async Task<string> GetRegistrarSalida(string nroTicket)
        {
            Parking parking = new Parking();
            parking = await _context.Parking.Where(p => p.NroTicket == nroTicket).FirstAsync();

            if (parking.Pagado == null || !(bool)parking.Pagado)
            {
                return "No puede salir. Debe cancelar su factura.";
            }

            if (parking.HoraMaximaSalida < DateTime.Now)
            {
                return "No puede salir. La hora máxima de salida excede la hora actual. Debe acercarse a la caja para validar su ticket.";
            }

            parking.HoraSalida = DateTime.Now;
            _context.Entry(parking).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return "Ok";
        }

        // GET: api/Parkings/BuscarTickets
        [HttpGet("BuscarTickets"), Authorize]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Parking>>> GetBuscarTickets()
        {
            if (_context.Parking == null)
            {
                return NotFound();
            }
            return await _context.Parking.ToListAsync();
        }
    }
}
