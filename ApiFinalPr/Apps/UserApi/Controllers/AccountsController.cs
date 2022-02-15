using ApiFinalPr.Apps.UserApi.DTOs.AccountDto;
using ApiFinalPr.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFinalPr.Apps.UserApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly RoleManager<IdentityRole> _rolemanager;

        public AccountsController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _usermanager = userManager;
            _rolemanager = roleManager;
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
    }
}
