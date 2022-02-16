using ApiFinalPr.Apps.UserApi.DTOs.AccountDto;
using ApiFinalPr.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiFinalPr.Apps.UserApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager,IMapper mapper)
        {
            _usermanager = userManager;
            _rolemanager = roleManager;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            AppUser user = await _usermanager.FindByNameAsync(registerDto.Username);
            if (user != null) return StatusCode(409);
            user = new AppUser
            {
                UserName = registerDto.Username,
                FullName = registerDto.Fullname
            };
            var result = await _usermanager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            var roleResult = await _usermanager.AddToRoleAsync(user, "Member");
            if (!roleResult.Succeeded) return BadRequest(result.Errors);
            return StatusCode(201);
        }

        //[HttpGet("roles")]
        //public async Task<IActionResult> CreateRole()
        //{
        //    var result = await _rolemanager.CreateAsync(new IdentityRole("Member"));
        //    result = await _rolemanager.CreateAsync(new IdentityRole("Admin"));
        //    result = await _rolemanager.CreateAsync(new IdentityRole("SuperAdmin"));
        //    return Ok();
        //}

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AppUser user = await _usermanager.FindByNameAsync(loginDto.Username);
            if (user == null) return NotFound();
           

            if (!await _usermanager.CheckPasswordAsync(user,loginDto.Password))
                return NotFound();


            //jwt

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim("FullName",user.FullName),
               
            };

            var roles = await _usermanager.GetRolesAsync(user);
            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList());
            string keyStr = "e3b0b6b9-eb9e-474f-8827-c5bd624e0e8e";
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(keyStr));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.Now.AddDays(3),
                issuer: "https://localhost:44311/",
                audience: "https://localhost:44311/"
                );

            string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = tokenStr });
        }

        [Authorize(Roles =("Member"))]
        public async Task<IActionResult> Get()
        {
            AppUser user =await _usermanager.FindByNameAsync(User.Identity.Name);
            AccountGetDto accountDto = _mapper.Map<AccountGetDto>(user);
            //    new AccountGetDto
            //{
            //    Id = user.Id,
            //    UserName = user.UserName,
            //    FullName = user.FullName
            //};
            return Ok(accountDto);
        }
    }
}
