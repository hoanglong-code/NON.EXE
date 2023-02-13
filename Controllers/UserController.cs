using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NON.EXE.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using NON.EXE.Models;

namespace NON.EXE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public UserController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUser()
        {
            return Ok(await _context.Users.ToListAsync());
        }
        [HttpPost("Register")]
        public async Task<ActionResult<List<User>>> Register(User request)
        {
            if (request.PassWord != null)
            {
                request.PassWord = PasswordHash.HashSha256(request.PassWord);
                _context.Users.Add(request);
                await _context.SaveChangesAsync();
            }
            return Ok(await _context.Users.ToListAsync());
        }
        [HttpPost("login")]
        public async Task<ActionResult<List<User>>> Login(User request)
        {
            try
            {
                var userFound = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName);
                if (userFound == null)
                {
                    return BadRequest("User not found!!!");
                }
                request.PassWord = PasswordHash.HashSha256(request.PassWord);
                if (request.PassWord != userFound.PassWord)
                {
                    return BadRequest("Wrong PassWord");
                }
                string token = GenerateToken(userFound);
                //userFound.Token= token;
                //await _context.SaveChangesAsync();
                //var refreshtoken = GenerateRefreshToken();
                //_ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
                //userFound.Token = token;
                //userFound.Created = DateTime.Now.AddDays(refreshTokenValidityInDays);
                //SetRefreshToken(refreshtoken);
                return Ok(token);        
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
       
        private string GenerateToken(User user)
        {
            String Secret = _configuration["Token"];
            String expireMinutes = _configuration["ExpireMinutes"];
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role),
                }),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature),
                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),               
            };
            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);
            return token;          
        }
        //private static RefreshToken GenerateRefreshToken()
        //{
        //    RefreshToken refreshToken = new();
        //    var randomNumber = new byte[32];
        //    using (var rng = RandomNumberGenerator.Create())
        //    {
        //        rng.GetBytes(randomNumber);
        //        refreshToken.Token= Convert.ToBase64String(randomNumber);
        //    }
        //    refreshToken.Expires = DateTime.UtcNow.AddDays(1);
        //    return refreshToken;
        //}
        //private void SetRefreshToken(RefreshToken newRefreshToken)
        //{
        //    var cookieOptions = new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Expires = newRefreshToken.Expires
        //    };
        //    Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
        //}
        [HttpGet("GetIdUser/{id}")]
        public async Task<ActionResult<List<User>>> GetIdUser(int id)
        {
            var dbUser = await _context.Users.FindAsync(id);
            if (dbUser == null)
                return BadRequest("User not found");
            return Ok(dbUser);
        }
        [HttpPut("UpdateUser/{id}")]
        public async Task<ActionResult<List<User>>> UpdateUser(User request)
        {
            var dbUser = await _context.Users.FindAsync(request.UserID);
            if (dbUser == null)
                return BadRequest("hero not found");
            dbUser.UserName = request.UserName;
            dbUser.PassWord = PasswordHash.HashSha256(request.PassWord);
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> DeleteUser(int UserID)
        {
            var dbUser = await _context.Users.FindAsync(UserID);
            if (dbUser == null) return BadRequest("hero not found");
            _context.Users.Remove(dbUser);
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }
        [HttpGet("GetPigigate/{page}")]
        public async Task<ActionResult<List<User>>> GetPigigate(int page)
        {
            if (_context.Users == null)
                return NotFound();
            var pageResults = 5f;
            var pageCount = Math.Ceiling(_context.Users.Count() / pageResults);
            var user = await _context.Users
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();
            var response = new UserResponseDTO
            {
                Users = user,
                CurrentPage = page,
                Pages = (int)pageCount
            };
            return Ok(response);
        }
    }
}
