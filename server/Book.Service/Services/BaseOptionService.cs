using Book.Core.Dtos.List;
using Book.Core.Models;
using Book.Core.Repositories;
using Book.Core.Services;
using Book.Core.UnitOfWork;
using Book.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Service.Services
{
    public class BaseOptionService : Service<BaseOption>, IBaseOptionService
    {
        private readonly IBaseOptionRepository _baseOptionRepository;
        public BaseOptionService(IGenericRepository<BaseOption> repository, IUnitOfWork unitOfWork,
            IBaseOptionRepository baseOptionRepository) : base(repository, unitOfWork)
        {
            _baseOptionRepository = baseOptionRepository;
        }

        public async Task<BaseOptionShowDto> GetAll()
        {
            return await _baseOptionRepository.GetAll();
        }
    }
}
