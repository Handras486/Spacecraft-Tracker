using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SpacecraftTracker.Service.Interfaces;
using SpacecraftTracker.WebAPI.CustomActionFilters;
using SpacecraftTracker.WebAPI.DTO;

namespace SpacecraftTracker.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenService tokenService;

        public AuthController(UserManager<IdentityUser> userManager, ITokenService tokenService)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
        }


        [HttpPost("Register")]
        [Authorize(Roles = "Admin")]
        [ValidateEmail]
        public async Task<IActionResult> Register([FromBody] AuthDTO registerRequest)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequest.Username,
                Email = registerRequest.Username,
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequest.Password);

            if (identityResult.Succeeded)
            {
                identityResult = await userManager.AddToRoleAsync(identityUser, "Carrier");

                if (identityResult.Succeeded)
                    return Ok(new { identityUser.Id , identityUser.Email } );
            }
            return BadRequest();
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthDTO loginRequest)
        {
            var user = await userManager.FindByNameAsync(loginRequest.Username);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequest.Password);

                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        var jwtToken = tokenService.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDTO() { JwtToken = jwtToken };

                        return Ok(response);
                    }
                }
                return Unauthorized();
            }

            return BadRequest();
        }
    }
}
