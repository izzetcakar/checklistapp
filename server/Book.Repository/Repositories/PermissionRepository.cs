using AutoMapper;
using Book.Core.Dtos.Generic;
using Book.Core.Dtos.Update;
using Book.Core.Models;
using Book.Core.Repositories;
using Book.Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Repository.Repositories
{
    public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
    {
        private readonly IMapper _mapper;
        public PermissionRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }

        public Task<List<Permission>> GetAllByUserId(Guid UserId)
        {
            return dbContext.Permissions.Where(x => x.UserId == UserId).AsNoTracking().ToListAsync();
        }

        public async Task ReplyRequest(PermissionUpdateDto permissionDto)
        {
            if (permissionDto.Status == Status.Allowed)
            {
                var subDto = _mapper.Map<SubDto>(permissionDto);
                var sub = _mapper.Map<Subscription>(subDto);

                var isExist = await dbContext.Subscriptions.AnyAsync(x => x.UserId == sub.UserId && x.OrganizationId == sub.OrganizationId);
                if (isExist)
                {
                    var subInDb = await dbContext.Subscriptions.Where(x => x.UserId == sub.UserId && x.OrganizationId == sub.OrganizationId).AsNoTracking().SingleOrDefaultAsync();
                    sub.Id = subInDb.Id;
                    dbContext.Subscriptions.Update(sub);
                }
                else
                {
                    sub.Id = new Guid();
                    await dbContext.Subscriptions.AddAsync(sub);
                }
            }
            
            dbContext.Permissions.Update(_mapper.Map<Permission>(permissionDto));
            
            await dbContext.SaveChangesAsync();
        }
    }
}
