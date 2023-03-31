using Book.Core.Dtos.Update;
using Book.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Core.Services
{
    public interface IPermissionService:IService<Permission>
    {
        Task<List<Permission>> GetAllByUserId(Guid UserId);
        Task ReplyRequest(PermissionUpdateDto permissionDto);
    }
}
