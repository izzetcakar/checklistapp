using AutoMapper;
using Book.Core.Dtos.List;
using Book.Core.Models;
using Book.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly IMapper _mapper;
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> Login(UserLoginDto userLoginDto)
        {
            var isExist = await dbContext.Users.AnyAsync(x => x.UserName == userLoginDto.UserName && x.Password == userLoginDto.Password);
            if (!isExist)
            {
                throw new Exception("User is not found");
            }
            else
            {
                return await dbContext.Users.Where(x => x.UserName == userLoginDto.UserName && x.Password == userLoginDto.Password).SingleOrDefaultAsync();
            }
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<User> Register(User user)
        {
            var nameExist = await dbContext.Users.AnyAsync(x => x.UserName == user.UserName);
            var emailExist = await dbContext.Users.AnyAsync(x => x.Email == user.Email);

            if (nameExist)
            {
                throw new Exception("Username already exists");
            }
            else if (emailExist)
            {
                throw new Exception("Email already exists");
            }
            else
            {
                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
                return await dbContext.Users.Where(x => x.UserName == user.UserName).SingleOrDefaultAsync();
            }
        }
    }
}
