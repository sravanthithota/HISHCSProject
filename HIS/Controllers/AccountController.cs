
using BAL.BL;
using BAL.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        UserBL userBL = new UserBL();
        private readonly IConfiguration Configuration;
        public AccountController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
       
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginViewModel model)
        {

            try
            {
               var ds = UserBL.Login(model);

                if (ds!=null)
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["Id"]) > 0)
                    {
                        var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, ds.Tables[0].Rows[0]["Name"].ToString()),
                    new Claim(ClaimTypes.NameIdentifier, ds.Tables[0].Rows[0]["Id"].ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };


                       authClaims.Add(new Claim("UserId", ds.Tables[0].Rows[0]["Id"].ToString()));
                        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]));

                        var token = new JwtSecurityToken(
                            issuer: Configuration["JWT:ValidIssuer"],
                            audience: Configuration["JWT:ValidAudience"],
                            expires: DateTime.UtcNow.AddYears(2),
                            claims: authClaims,
                            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                            );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            token_type = "bearer",
                            UserDetail= ds.Tables[0],
                            Menu= userBL.GetControl(ds.Tables[1]),
                            IsError = "0",
                            Msg = "Logged in successfully"
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            IsError = "1",
                            Msg = ds.Tables[0].Rows[0]["msg"].ToString()
                        });
                    }
                }
                else
                {
                    return Ok(new
                    {
                        IsError = "1",
                        Msg = "INVALIDUSER"
                    });
                }
            }
            catch (Exception ex)
            {
                return Unauthorized(new
                {
                    IsError = "1",
                    Msg = ex.Message
                });
            }

        }
    }
}
