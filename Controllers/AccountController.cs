using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app_dotnet.Dtos.Account;
using app_dotnet.Interfaces;
using app_dotnet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app_dotnet.Controllers
{
    [Route("/api/account")]
    public class AccountController: ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signinManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Username!.ToLower());

            if(user == null)
                return Unauthorized("Invalid Username");

            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password!, false);

            if(!result.Succeeded)
                return Unauthorized("Password incorrect");
            
            return Ok(
                new NewUserDto {
                    Username = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new AppUser {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };

                var createUser = await _userManager.CreateAsync(appUser, registerDto.Password!);

                if (createUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser,"User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto {
                                Username = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser)
                            }
                        );
                    } else {
                        return StatusCode(500, roleResult.Errors);
                    }
                } else {
                    return StatusCode(500, createUser.Errors);
                }

            }
            catch (Exception e)
            {
                
                return StatusCode(500, e);
            }
        }
    }
}