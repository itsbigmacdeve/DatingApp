using API.Extensions;
using Microsoft.AspNetCore.Identity;

namespace API.Entities;


public class AppUser :IdentityUser<int>
{
    // se removieron propiedades como id, username, password hash y salt

    public DateOnly DateOfBirth { get; set; }

    public string KnownAs { get; set; }
    public DateTime created { get; set; }

    public DateTime lastActive { get; set; }
    public string gender { get; set; }
    public string Introducction { get; set; }

    public string LookingFor { get; set; }

    public string Interests { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public List<Photo> Photos { get; set; } = new();
    
    public List<UserLike> LikedByUsers { get; set; }
    public List<UserLike> LikedUsers { get; set; }

    public List<Message> MessageSent { get; set; }
    public List<Message> MessageReceived { get; set; }

    public ICollection<AppUserRole> UserRoles { get; set; }

    
}
