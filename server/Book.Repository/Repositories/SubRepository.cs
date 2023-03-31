using AutoMapper;
using Book.Core.Dtos.Generic;
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
    public class SubRepository : GenericRepository<Subscription>, ISubRepository
    {
        private readonly IMapper _mapper;
        public SubRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Subscription>> GetSubsByUserId(Guid UserId)
        {
            return await dbContext.Subscriptions.Where(x=>x.UserId== UserId).AsNoTracking().ToListAsync();
        }

        public async Task<List<Organization>> GetOrgsByUserId(Guid UserId)
        {
            List<Guid> subIds = await dbContext.Subscriptions.Where(x => x.UserId == UserId).Select(x => x.OrganizationId).ToListAsync();
            return await dbContext.Organizations.Where(x=> !subIds.Contains(x.Id)).AsNoTracking().ToListAsync();
        }

        public async Task<List<SubShowDto>> GetWithTitles()
        {
            List<SubShowDto> newSubs = new List<SubShowDto>();
            var allSubs = await dbContext.Subscriptions.AsNoTracking().ToListAsync();
            foreach(var sub in allSubs)
            {
                SubShowDto newSub = _mapper.Map<SubShowDto>(sub);
                newSubs.Add(newSub);
            }
            foreach (var sub in newSubs)
            {
                sub.Title = dbContext.Organizations.Where(x => x.Id == sub.OrganizationId).Select(x => x.Title).ToString();
                sub.Username = dbContext.Users.Where(x => x.Id == sub.UserId).Select(x => x.UserName).ToString();
            }
            return newSubs;
        }

        public async Task<SubShowDto> GetWithTitlesById(Guid Id)
        {
            var sub = await dbContext.Subscriptions.Where(x => x.Id == Id).AsNoTracking().SingleOrDefaultAsync();
            var subDto = _mapper.Map<SubShowDto>(sub);

            subDto.Title = dbContext.Organizations.Where(x => x.Id == subDto.OrganizationId).Select(x => x.Title).ToString();
            subDto.Username = dbContext.Users.Where(x => x.Id == subDto.UserId).Select(x => x.UserName).ToString();
      
            return subDto;
        }

        public async Task RemoveByUserId(Guid UserId)
        {
            var subs = await dbContext.Subscriptions.Where(x => x.UserId == UserId).AsNoTracking().ToListAsync();
            dbContext.Subscriptions.RemoveRange(subs);
            await dbContext.SaveChangesAsync();
        }
    }
}
