using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SystemManagement.Common;
using SystemManagement.Dto;
using SystemManagement.Service.Contract;

namespace SystemManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JWTConfig _jwtConfig;
        private readonly IAccountService _accountService;

        public AccountController(IOptions<JWTConfig> jwtConfig, IAccountService accountService)
        {
            _jwtConfig = jwtConfig.Value;
            _accountService = accountService;
        }

        public async Task<IActionResult> Login(SysUserDto userDto)
        {
            var validateResult = await _accountService.ValidateCredentials(userDto.Account, userDto.Password);
            if (!validateResult.Item1)
            {
                return new NotFoundObjectResult("用户名或密码错误");
            }

            var user = validateResult.Item2;
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.ID.ToString()),
                new Claim(ClaimTypes.Role, user.RoleId)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SymmetricSecurityKey));

            var token = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: null,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new JsonResult(new { token = jwtToken });
        }
    }
}