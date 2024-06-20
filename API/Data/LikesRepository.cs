using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class LikesRepository : ILikesRepository
    {
        private readonly DataContext context;

        public LikesRepository(DataContext context)
        {
            this.context = context;
            
        }
        public async Task<UserLike> GetUserLike(int sourceUserId, int targetUserId)
        {
            // lo que sucede aqui , se podria decir en tipo sql, es que va y busca dentro de la tabla de likes y despues lo que realiza, es que dentro de esa tabla, en base a los parametros dados, encuentra la informacion que corresponda a esos parametros
            // a lo que entiendo que pasa, es cuando encunetra la informacion, como retorna un UserLike, regresa informacion de ese tipo, por lo cual traera el id, pero de cierta manera 2 objetos de appuser, que son los que se relacionan con el like
            return await context.Likes.FindAsync(sourceUserId, targetUserId);
        }

        public async Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId)
        {
            // Primero ejecuta 2 consultas, que pueden ser manipuldas despues, primero obtenermos todos los usuarios, y despues todos los likes
            var users = context.Users.OrderBy(u => u.UserName).AsQueryable();
            var likes = context.Likes.AsQueryable();

            // si el predicado es igual a liked, entonces se filtra los likes, para que solo se muestren los que el usuario le dio like , es decir los que le gustan
            if (predicate == "liked")
            {
                likes = likes.Where(like => like.SourceUserId == userId);
                users = likes.Select(like => like.TargetUser);
            }

            // si el predicado es igual a likedBy, entonces se filtra los likes, para que solo se muestren los que le dieron like al usuario, es decir yo Victor, a los que le gusto, por ejemplo le gusto a Melissa, Sarah, por ejemplo
            if (predicate == "likedBy")
            {
                likes = likes.Where(like => like.TargetUserId == userId);
                users = likes.Select(like => like.SourceUser);
            }


            // se retorna una lista de likes, que se obtiene de la lista de usuarios, y se mapea a un likeDto, dependiendo de los datos que se quieran mostrar liked o likedBy
            return await users.Select(user => new LikeDto
            {
                //se mappea la informacion de los usuarios, a un likeDto, es decir se le dice a que propiedades de likeDto, se le asigna la informacion de los usuarios
                UserName = user.UserName,
                Age = user.DateOfBirth.CalculateAge(),
                KnownAs = user.KnownAs,
                PhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain).Url,
                City = user.City
            }).ToListAsync();
        }

        public async Task<AppUser> GetUserWithLikes(int userId)
        {
            // este metodo trae la informacion de un usuario, adenmas de la inrformaciuon de los usuaris que le ha dado like
            return await context.Users
                .Include(x => x.LikedUsers)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}