using AutoMapper;
using Book.Core.Dtos.List;
using Book.Core.Models;
using Book.Core.Repositories;
using Book.Core.Services;
using Book.Core.UnitOfWork;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Book.Service.Services
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork,
            IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(repository, unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetId()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue("UserId");
                return result;
            }
            else
            {
                throw new NotImplementedException("Invalid Token");
            }
        }
        public async Task<User> Login(UserLoginDto userLoginDto)
        {
            return await _userRepository.Login(userLoginDto);
        }
        public Task Logout()
        {
            throw new NotImplementedException();
        }
        public async Task<User> Register(User user)
        {
            return await _userRepository.Register(user);
        }
        public async Task<bool> VerifyAdmin()
        {
            var userId = await GetIdByToken();
            var user = await _userRepository.GetByIdAsync(userId);
            if(user != null)
            {
                return user.IsAdmin.Value;
            }
            else
            {
                throw new NotImplementedException("User is not found");
            }
        }
        public async Task<Guid> GetIdByToken()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue("UserId");
                return new Guid(result);
            }
            else
            {
                throw new NotImplementedException("Invalid Token");
            }
        }
    }
}
