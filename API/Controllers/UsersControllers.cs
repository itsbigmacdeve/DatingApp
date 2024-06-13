﻿using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
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
    private readonly IMapper _mapper;

    public UsersControllers(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }


    //Obtiene una lista de todos los usuarios
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        //var users = await _context.Users.ToListAsync();

        var users = await _userRepository.GetUsersAsync();

        var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);

        return Ok(usersToReturn);
    }

    //Obtener el user del id que se le da
    [HttpGet("{username}")] // api/users/3
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        // var user = await _context.Users.FindAsync(id);

        // return user;

        var user= await _userRepository.GetUserByUsernameAsync(username);

        return _mapper.Map<MemberDto>(user);
    }
    
        
    
    

}
