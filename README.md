# Useful commands

```bash
cd /c/Projects/DatingApp/ 

rm -f API/datingapp.db
rm -f API/datingapp.db-wal
rm -f API/datingapp.db-shm

# one-line command
cd /c/Projects/DatingApp/ && rm -f API/datingapp.db && rm -f API/datingapp.db-wal && rm -f API/datingapp.db-shm && cd /c/Projects/DatingApp/API/ && rm -fR /c/Projects/DatingApp/API/Data/Migrations && dotnet ef migrations add InitialCreate -o Data/Migrations && dotnet watch --no-hot-reload

# command to reset users that are seeded
cd /c/Projects/DatingApp/ && rm -f API/datingapp.db && rm -f API/datingapp.db-wal && rm -f API/datingapp.db-shm && cd /c/Projects/DatingApp/API/ && dotnet watch --no-hot-reload
```

- Mismo que ejecutar:

```bash
dotnet ef database drop
```

## Concepto nuevo de superconstructores en C# 12.0 para dotnet 8.0

```cs
namespace API.Controllers;
public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
{
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

        if (user == null) return Unauthorized("invalid username");

        // para verificar si la contraseña es correcta
        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("invalid password");
        }

        return new UserDto
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };
    }
}
```

## Alternativa de SQL en (ejemplo) Flask (python)

```py

table = 'Users'
table_photo = 'Photos'

query = '''
SELECT * FROM {table} INNERJOIN ({table_photo}) where 
'''

```

## New packages for node

```bash
npm i bootswatch ngx-toastr ngx-spinner ng2-file-upload ng-gallery
```



## Clean (reset) client Angular app

```bash
rm -fR .angular/ && rm -fR node_modules/ && rm package-lock.json && npm i
```
