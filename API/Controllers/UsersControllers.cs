using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
// Para crear el controlador ocupamos llamar al ControllerBase
//[ApiController]
//[Route("api/users")] // /api/users, le cambie de api/[controller] a api/users
[Authorize]
public class UsersControllers : BaseApiController
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;

    public UsersControllers(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _photoService = photoService;
    }


    //Obtiene una lista de todos los usuarios
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        //var users = await _context.Users.ToListAsync();

        var users = await _userRepository.GetMembersAsync();

        return Ok(users);
    }

    //Obtener el user del id que se le da
    [HttpGet("{username}")] // api/userscontrollers/3
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        // var user = await _context.Users.FindAsync(id);

        // return user;

        return await _userRepository.GetMemberAsync(username);

    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
        //var username = User.GetUsername(); // esto se cambio en la seccion 11, por si no sale igual que antes 
        //User.FindFirst(ClaimTypes.NameIdentifier)?.Value; asi estaba anres de la seccion 11

        var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

        if (user == null) return NotFound();

        _mapper.Map(memberUpdateDto, user);

        if (await _userRepository.SaveAllAsync()) return NoContent();

        //_userRepository.Update(user);

        return BadRequest("Failed to update user");
    }

    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDto>> AddPhoto () {
        var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

        if (user == null) return NotFound();

        var result = await _photoService.AddPhotoAsync(Request.Form.Files[0]);

        if (result.Error != null) return BadRequest(result.Error.Message);

        var photo = new Photo
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };

        if (user.Photos.Count == 0)
        {
            photo.IsMain = true;
        }

        user.Photos.Add(photo);

        if (await _userRepository.SaveAllAsync()) //return _mapper.Map<PhotoDto>(photo);  
        {
            return CreatedAtAction("GetUser", new {username = user.UserName}, _mapper.Map<PhotoDto>(photo)); 
        }

        return BadRequest("Problem adding photo");
    }

    [HttpPut("set-main-photo/{photoId}")]
    public async Task<ActionResult> SetMainPhoto(int photoId)
    {
        var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

        if (user == null) return NotFound();

        var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

        if (photo == null) return NotFound();

        if (photo.IsMain) return BadRequest("This is already your main photo");

        var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);

        if (currentMain != null) currentMain.IsMain = false;

        photo.IsMain = true;

        if (await _userRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Failed to set main photo");
    }

    [HttpDelete("delete-photo/{photoId}")]
    public async Task<ActionResult>DeletePhoto(int photoId)
    {
        var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

        var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);
        
        if (photo == null) return NotFound();

        if (photo.IsMain) return BadRequest("You cannot delete your main photo");

        if (photo.PublicId != null)
        {
            var result = await _photoService.DeletePhotoAsync(photo.PublicId);
            if (result.Error != null) return BadRequest(result.Error.Message);
        }

        user.Photos.Remove(photo);

        if (await _userRepository.SaveAllAsync()) return Ok();

        return BadRequest("Problem deleting photo");
    }

    
        
    
    

}
