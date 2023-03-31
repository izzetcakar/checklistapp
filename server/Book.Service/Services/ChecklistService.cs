using AutoMapper;
using Book.Core.Dtos.List;
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
    public class ChecklistService: Service<Checklist>, IChecklistService
    {
        private readonly IChecklistRepository _checklistRepository;
        private readonly IMapper _mapper;

        public ChecklistService(IGenericRepository<Checklist> repository, IUnitOfWork unitOfWork,
            IChecklistRepository checklistRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _checklistRepository = checklistRepository;
            _mapper = mapper;
        }

        public async Task<List<Checklist>> GetByOrgId(Guid OrgId)
        {
            return await _checklistRepository.GetByOrgId(OrgId);
        }

        public async Task<Checklist> GetWithtems(Guid Id)
        {
            return await _checklistRepository.GetWithtems(Id);
        }

        public async Task RemoveRangeIds(List<string> Ids)
        {
            await _checklistRepository.RemoveRangeIds(Ids);
        }
    }
}
