using AutoMapper;
using Book.Core.Dtos.Generic;
using Book.Core.Dtos.List;
using Book.Core.Models;
using Book.Core.Repositories;
using Book.Core.Services;
using Book.Core.UnitOfWork;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Book.Service.Services
{
    public class SubService : Service<Subscription>, ISubService
    {
        private readonly ISubRepository _subRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public SubService(IGenericRepository<Subscription> repository, IUnitOfWork unitOfWork,
            ISubRepository subRepository,IUserService userService,IMapper mapper ) : base(repository, unitOfWork)
        {
            _subRepository = subRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<List<Organization>> GetOrgsByUserId(Guid UserId)
        {
            return await _subRepository.GetOrgsByUserId(UserId);
        }

        public async Task<List<SubShowDto>> GetWithTitles()
        {
            return await _subRepository.GetWithTitles();
        }

        public async Task<SubShowDto> GetWithTitlesById(Guid Id)
        {
            return await _subRepository.GetWithTitlesById(Id);
        }

        public Task RemoveByUserId(Guid UserId)
        {
            return _subRepository.RemoveByUserId(UserId);
        }
       

        public async Task<List<Subscription>> GetSubsByUserId()
        {
            var userId = await _userService.GetIdByToken();
            var allSubs = await _subRepository.GetSubsByUserId(userId);
            return allSubs;
        }

        public async Task<Subscription> GetSubByOrgId(Guid OrgId)
        {
            var subs = await GetSubsByUserId();
            return subs.Where(x => x.OrganizationId == OrgId).SingleOrDefault();
        }
    }
}
