using AutoMapper;
using Book.Core.Dtos.Create;
using Book.Core.Dtos.Generic;
using Book.Core.Dtos.List;
using Book.Core.Models;
using Book.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PageApp.API.Controllers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Book.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IService<User> _service;

        public UserController(IMapper mapper, IUserService userservice, IConfiguration configuration, IService<User> service)
        {
            _mapper = mapper;
            _userService = userservice;
            _configuration = configuration;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var users = await _userService.GetAllAsync();
            var userDtos = _mapper.Map<List<UserShowDto>>(users).ToList();
            return CreateActionResult(CustomResponseDto<List<UserShowDto>>.Success(200, userDtos));
        }

        [HttpGet("{id}"), Authorize]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<User>.Success(200, user));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            try
            {
                var user = await _userService.Login(userLoginDto);
                if (user != null)
                {
                    TokenDto token = new TokenDto();
                    token.token = CreateToken(user);
                    return CreateActionResult(CustomResponseDto<TokenDto>.Success(200, token));
                }
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(401, "User is not found"));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(401, ex.Message));
            }

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserCreateDto userDto)
        {
            var userInput = _mapper.Map<User>(userDto);
            userInput.Id = new Guid();
            userInput.IsAdmin = false;
            try
            {
                var user = await _userService.Register(userInput);
                if (user != null)
                {
                    TokenDto token = new TokenDto();
                    token.token = CreateToken(user);
                    return CreateActionResult(CustomResponseDto<TokenDto>.Success(200, token));
                }
                else
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(401, "User is not valid"));
                }
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(401, ex.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserCreateDto userDto)
        {
            //await userService.UpdateAsync(mapper.Map<User>(userDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            await _userService.RemoveAsync(user);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
        }

        [HttpGet("getWithToken"), Authorize]
        public async Task<IActionResult> GetUser()
        {
            var userId = _userService.GetId();
            Guid newGuid = new Guid(userId);
            var newUser = _userService.GetByIdAsync(newGuid).Result;
            if (newUser != null)
            {
                return CreateActionResult(CustomResponseDto<UserShowDto>.Success(200, _mapper.Map<UserShowDto>(newUser)));
            }
            else
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "User is not found"));
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("UserId", user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(14),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
