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
    public class OrganizationRepository : GenericRepository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task Create(Organization organization)
        {
            var isExist = await dbContext.Organizations.AnyAsync(x => x.Title == organization.Title);
            if(isExist)
            {
                throw new Exception("Organization already exist");
            }
            else
            {
                await dbContext.Organizations.AddAsync(organization);
                dbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveRangeIds(List<string> Ids)
        {
            List<Organization> organizations = new List<Organization>();
            organizations = await dbContext.Organizations.Where(x => Ids.Contains(x.Id.ToString())).AsNoTracking().ToListAsync();
            dbContext.RemoveRange(organizations);
            dbContext.SaveChangesAsync();
        }
    }
}
