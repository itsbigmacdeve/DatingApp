using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using AutoMapper;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;


        //Hay que implementar, los cosntrucores de cada repositorio para poder hacer uso de ellos
        //Constructor
        public UnitOfWork(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IUserRepository _userRepository => new UserRepository(_context, _mapper);

        public IMessageRepository _messageRepository => new MessageRepository(_context, _mapper);

        public ILikesRepository _likesRepository => new LikesRepository(_context);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}