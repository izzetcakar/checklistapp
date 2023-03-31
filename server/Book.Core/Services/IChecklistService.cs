using Book.Core.Dtos.List;
using Book.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Core.Services
{
    public interface IChecklistService:IService<Checklist>
    {
        Task RemoveRangeIds(List<string> Ids);
        Task<List<Checklist>> GetByOrgId(Guid OrgId);
        Task<Checklist> GetWithtems(Guid Id);
    }
}
