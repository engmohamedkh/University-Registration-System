
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using University.Core.DTO;
using University.Models;

namespace CUD01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;

        public AccountController(UserManager<ApplicationUser> _userManager,IConfiguration _config)
        {
            userManager = _userManager;
            config = _config;
        }
        [HttpPost("Register")] //Create User [Identity]
        public async Task<IActionResult> Registaration(RegisterDTO registerUser)
        {
            if(ModelState.IsValid)
            {
                //Save
                ApplicationUser user = new ApplicationUser();
                user.Email = registerUser.Emil;
                user.UserName = registerUser.UserName;
                IdentityResult res = await userManager.CreateAsync(user, registerUser.Password);
                if(res.Succeeded)
                {
                    return Ok("Account Added Succefully");
                }
                var ErrList = new List<string>();
                foreach(var err in res.Errors)
                {
                    ErrList.Add(err.Description);
                }
                return BadRequest(ErrList);
            }
            return BadRequest(ModelState);
        }


        [HttpPost("Login")] //Verify User & Create Token
        public async Task<IActionResult> Login(loginDTO loginUser)
        {
            if(ModelState.IsValid)
            {
              ApplicationUser userFromDB =await   userManager.FindByNameAsync(loginUser.UserName);
                if (userFromDB is null)
                {
                    return Unauthorized();
                }
                 bool found = await userManager.CheckPasswordAsync(userFromDB, loginUser.Password);
                if (found)
                {
                    #region Create Cliams For PayLoad
                    var Claims = new List<Claim>();
                    Claims.Add(new Claim(ClaimTypes.Name, userFromDB.UserName));
                    Claims.Add(new Claim(ClaimTypes.NameIdentifier, userFromDB.Id));
                    Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    //Check Roles User
                    var Roles = await userManager.GetRolesAsync(userFromDB);
                    foreach (var role in Roles)
                    {
                        Claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    #endregion

                    #region Secuirty Key
                    SecurityKey SecretKey =
                                  new SymmetricSecurityKey(
                                      Encoding.UTF8.GetBytes(config["JWT:Secret"]));
                    SigningCredentials SignCred =
                        new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256);
                    #endregion

                    //Create Token
                    #region Create Token
                    JwtSecurityToken token = new JwtSecurityToken(
                                  issuer: config["JWT:ValidateAssure"],
                                  audience: config["JWT:ValidateAudiance"],
                                  claims: Claims,
                                  expires: DateTime.Now.AddHours(1),
                                  signingCredentials: SignCred
                                  ); 
                    #endregion

                    return Ok(
                        new
                        {
                            token =new JwtSecurityTokenHandler().WriteToken(token),
                            exp=token.ValidTo
                        }
                        );
                }
                return Unauthorized();
            }
            return BadRequest();
        }
    }
}
