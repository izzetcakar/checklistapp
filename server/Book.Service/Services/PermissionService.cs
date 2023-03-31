using Book.Core.Dtos.Update;
using Book.Core.Models;
using Book.Core.Repositories;
using Book.Core.Services;
using Book.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Service.Services
{
    public class PermissionService : Service<Permission>, IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        public PermissionService(IGenericRepository<Permission> repository, IUnitOfWork unitOfWork,
            IPermissionRepository permissionRepository) : base(repository, unitOfWork)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<List<Permission>> GetAllByUserId(Guid UserId)
        {
            return await _permissionRepository.GetAllByUserId(UserId);
        }

        public async Task ReplyRequest(PermissionUpdateDto permissionDto)
        {
            await _permissionRepository.ReplyRequest(permissionDto);
        }
    }
}
