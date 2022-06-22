using System;
using GEPARK.Data;
using GEPARK.Models;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace GEPARK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly GEPARKContext _context;

        public AuthController(GEPARKContext context)
        {

            _context = context;
        }

        [HttpPost, Route("Login")]
        public  IActionResult Login([FromBody]Usuario user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }
            var login = _context.Usuario.Find(user.Nombre);

            if (login == null)
            {
                return NotFound();
            }
           


            if (user.Nombre == login.Nombre && user.Clave == login.Clave)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:5000",
                    audience: "http://localhost:5000",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}