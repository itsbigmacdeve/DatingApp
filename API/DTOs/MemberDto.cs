namespace API.DTOs
{
    public class MemberDto
    {

    public int Id { get; set; }
    public string UserName { get; set; }

    public string PhotoUrl { get; set; }

    public int Age { get; set; }

    public string KnownAs { get; set; }
    public DateTime created { get; set; }

    public DateTime lastActive { get; set; }
    public string gender { get; set; }
    public string Introducction { get; set; }

    public string LookingFor { get; set; }

    public string Interests { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public List<PhotoDto> Photos { get; set; } 
        
    }
}