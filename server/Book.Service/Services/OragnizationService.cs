using AutoMapper;
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
    public class OrganisationService : Service<Organization>, IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;
        public OrganisationService(IGenericRepository<Organization> repository, IUnitOfWork unitOfWork,
            IOrganizationRepository organizationRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _organizationRepository = organizationRepository;
            _mapper = mapper;
        }

        public async Task Create(Organization organization)
        {
            await _organizationRepository.Create(organization);
        }

        public async Task RemoveRangeIds(List<string> Ids)
        {
            await _organizationRepository.RemoveRangeIds(Ids);
        }
    }
}
