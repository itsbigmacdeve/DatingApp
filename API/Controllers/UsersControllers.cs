using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
// Para crear el controlador ocupamos llamar al ControllerBase
[ApiController]
[Route("api/users")] // /api/users, le cambie de api/[controller] a api/users
public class UsersControllers : ControllerBase
{
    private readonly DataContext _context;

    public UsersControllers(DataContext context)
    {
        _context = context;
    }

    //Obtiene una lista de todos los usuarios
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();

        return users;
    }

    //Obtener el user del id que se le da
    [HttpGet("{id}")] // api/users/3
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        return user;
    }
    

}
