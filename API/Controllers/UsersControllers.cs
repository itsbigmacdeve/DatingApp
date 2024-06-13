using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
// Para crear el controlador ocupamos llamar al ControllerBase
//[ApiController]
//[Route("api/users")] // /api/users, le cambie de api/[controller] a api/users
[Authorize]
public class UsersControllers : BaseApiController
{
    private readonly IUserRepository _userRepository;

    public UsersControllers(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    //Obtiene una lista de todos los usuarios
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        //var users = await _context.Users.ToListAsync();

        return Ok( await _userRepository.GetUsersAsync());
    }

    //Obtener el user del id que se le da
    [HttpGet("{username}")] // api/users/3
    public async Task<ActionResult<AppUser>> GetUser(string username)
    {
        // var user = await _context.Users.FindAsync(id);

        // return user;

        return await _userRepository.GetUserByUsernameAsync(username);
    }
    
        
    
    

}
