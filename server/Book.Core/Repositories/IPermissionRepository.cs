using Book.Core.Dtos.Update;
using Book.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Core.Repositories
{
    public interface IPermissionRepository:IGenericRepository<Permission>
    {
        Task<List<Permission>> GetAllByUserId(Guid UserId);
        Task ReplyRequest(PermissionUpdateDto permissionDto);
    }
}
