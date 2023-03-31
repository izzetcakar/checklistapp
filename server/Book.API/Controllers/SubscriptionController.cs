using AutoMapper;
using Book.Core.Dtos.Create;
using Book.Core.Dtos.Generic;
using Book.Core.Dtos.List;
using Book.Core.Models;
using Book.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PageApp.API.Controllers;

namespace Book.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : CustomBaseController
    {
        private readonly ISubService _subService;
        private readonly IService<Subscription> _service;
        private readonly IOrganizationService _organizationService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public SubscriptionController(ISubService subService, IService<Subscription> service, IMapper mapper, IOrganizationService organizationService, IUserService userService)
        {
            _subService = subService;
            _service = service;
            _mapper = mapper;
            _organizationService = organizationService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var allSubs = await _subService.GetAllAsync();
            var allSubsDto = _mapper.Map<List<SubShowDto>>(allSubs).ToList();
            foreach (var sub in allSubsDto)
            {
                sub.Title = _organizationService.GetByIdAsync(sub.OrganizationId).Result.Title;
                sub.Username = _userService.GetByIdAsync(sub.UserId).Result.UserName;
            }
            return CreateActionResult(CustomResponseDto<List<SubShowDto>>.Success(200, allSubsDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var sub = await _subService.GetByIdAsync(id);
            var subDto = _mapper.Map<SubShowDto>(sub);

            subDto.Title = _organizationService.GetByIdAsync(subDto.OrganizationId).Result.Title;
            subDto.Username = _userService.GetByIdAsync(subDto.UserId).Result.UserName;

            return CreateActionResult(CustomResponseDto<SubShowDto>.Success(200, subDto));
        }

        [HttpGet("getByUser")]
        public async Task<IActionResult> GetByUser()
        {
            var allSubs = await _subService.GetSubsByUserId();
            var allSubDtos = _mapper.Map<List<SubShowDto>>(allSubs).ToList();
            foreach (var sub in allSubDtos)
            {
                sub.Title = _organizationService.GetByIdAsync(sub.OrganizationId).Result.Title;
                sub.Username = _userService.GetByIdAsync(sub.UserId).Result.UserName;
            }
            return CreateActionResult(CustomResponseDto<List<SubShowDto>>.Success(200, allSubDtos));
        }

        [HttpGet("getOrgsByUserId/{id}")]
        public async Task<IActionResult> GetOrgsByUserId(Guid id)
        {
            var orgs = await _subService.GetOrgsByUserId(id);
            var orgDtos = _mapper.Map<List<OrganizationShowDto>>(orgs).ToList();
            return CreateActionResult(CustomResponseDto<List<OrganizationShowDto>>.Success(200, orgDtos));
        }

        [HttpPost]
        public async Task<IActionResult> Add(SubCreateDto subCreateDto)
        {
            var user = await _userService.GetByIdAsync(subCreateDto.UserId);
            var newSub = _mapper.Map<Subscription>(subCreateDto);
            newSub.Id = Guid.NewGuid();
            if (user.IsAdmin == true)
            {
                newSub.CanList= true;
                newSub.CanEdit= true;
                newSub.CanAdd= true;
                newSub.CanDelete= true;
            }
            await _subService.AddAsync(newSub);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
        }

        [HttpPut]
        public async Task<IActionResult> Update(SubDto subDto)
        {
            var subInDb = await _subService.GetByIdAsync(subDto.Id);
            await _subService.UpdateAsync(subInDb, _mapper.Map<Subscription>(subDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var sub = await _subService.GetByIdAsync(id);
            await _subService.RemoveAsync(sub);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
        }

        [HttpDelete("removeByUserId/{id}")]
        public async Task<IActionResult> RemoveByUserId(Guid id)
        {
            await _subService.RemoveByUserId(id);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
        }
    }
}
