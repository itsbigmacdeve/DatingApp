namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository _userRepository { get; }

        IMessageRepository _messageRepository { get; }

        ILikesRepository _likesRepository { get; }

//Si algo sale mal , se revierte la transaccion
        Task<bool> Complete();

// Si hay un cambio en la base de datos 
        bool HasChanges();
    }
}