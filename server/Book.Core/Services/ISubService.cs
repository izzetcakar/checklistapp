using Book.Core.Dtos.Generic;
using Book.Core.Dtos.List;
using Book.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Core.Services
{
    public interface ISubService:IService<Subscription>
    {
        Task<SubShowDto> GetWithTitlesById(Guid Id);
        Task<List<SubShowDto>> GetWithTitles();
        Task<List<Organization>> GetOrgsByUserId(Guid UserId);
        Task RemoveByUserId(Guid UserId);
        Task<List<Subscription>> GetSubsByUserId();
        Task<Subscription> GetSubByOrgId(Guid OrgId);
    }
}
