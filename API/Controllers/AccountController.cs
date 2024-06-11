using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {

        private readonly DataContext _context;
        

        public AccountController(DataContext context)
        {
            _context = context;
        }

        // le estamos pasando el dto, que es el que tiene el username y el password
        [HttpPost("register")] //POST : api/account/register
        public async Task<ActionResult<AppUser>> Register (RegisterDto registerDto)
        {
            // si el usuario ya existe, regresa un bad request
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");
            using var hmac = new HMACSHA512();

            //creamos un nuevo usuario
            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            // llamamos al contexto y le agregamos el usuario, el contexto es el que se encarga de hacer la comunicacion con la base de datos y tiene una serie de metodos que nos permiten hacer operaciones con la base de datos
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUser>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null) return Unauthorized("invalid username");

            // para verificar si la contraseña es correcta
            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("invalid password");
            }

            return user;
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

    }
}