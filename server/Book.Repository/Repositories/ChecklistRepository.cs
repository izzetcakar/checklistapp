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
    public class CheclistRepository : GenericRepository<Checklist>, IChecklistRepository
    {
        private readonly IMapper _mapper;
        public CheclistRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Checklist>> GetByOrgId(Guid OrgId)
        {
            return await dbContext.Checklists.Where(x=>x.OrganizationId == OrgId).AsNoTracking().ToListAsync();
        }

        public async Task<Checklist> GetWithtems(Guid Id)
        {
            return await dbContext.Checklists.Where(x => x.Id==Id).Include(x=>x.ListItems).AsNoTracking().SingleOrDefaultAsync();
        }

        public async Task RemoveRangeIds(List<string> Ids)
        {
            List<Checklist> listItems = new List<Checklist>();
            listItems = await dbContext.Checklists.Where(x => Ids.Contains(x.Id.ToString())).AsNoTracking().ToListAsync();
            dbContext.RemoveRange(listItems);
            dbContext.SaveChangesAsync();
        }
    }
}
