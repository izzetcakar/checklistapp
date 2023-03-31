using AutoMapper;
using Book.Core.Dtos.Create;
using Book.Core.Dtos.Generic;
using Book.Core.Dtos.List;
using Book.Core.Dtos.Update;
using Book.Core.Models;
using Book.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PageApp.API.Controllers;

namespace Book.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : CustomBaseController
    {
        private readonly IOrganizationService _organizationService;
        private readonly ISubService _subService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IService<Organization> _service;

        public OrganizationController(IOrganizationService organizationService, IMapper mapper, IService<Organization> service,
            ISubService subService, IUserService userService)
        {
            _organizationService = organizationService;
            _mapper = mapper;
            _service = service;
            _subService = subService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var organizations = await _organizationService.GetAllAsync();
            var organizationDtos = _mapper.Map<List<OrganizationShowDto>>(organizations).ToList();
            return CreateActionResult(CustomResponseDto<List<OrganizationShowDto>>.Success(200, organizationDtos));
        }
        [HttpGet("getByUser")]
        public async Task<IActionResult> GetOrgsByUser()
        {
            var isAdmin = await _userService.VerifyAdmin();
            if (isAdmin == true)
            {
                var organizations = await _organizationService.GetAllAsync();
                var orgDtos = _mapper.Map<List<OrganizationShowDto>>(organizations).ToList();
                return CreateActionResult(CustomResponseDto<List<OrganizationShowDto>>.Success(200, orgDtos));
            }
           
            var subs = await _subService.GetSubsByUserId();
            List<Organization> orgs = new List<Organization>();
            foreach (var sub in subs)
            {
                if (sub.CanList == true)
                {
                    var org = await _organizationService.GetByIdAsync(sub.OrganizationId);
                    orgs.Add(org);
                }
            }
            var organizationDtos = _mapper.Map<List<OrganizationShowDto>>(orgs).ToList();
            return CreateActionResult(CustomResponseDto<List<OrganizationShowDto>>.Success(200, organizationDtos));  
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var sub = await _subService.GetSubByOrgId(id);
            if (sub.CanList == true)
            {
                var organization = await _organizationService.GetByIdAsync(id);
                var organizationDto = _mapper.Map<OrganizationShowDto>(organization);
                return CreateActionResult(CustomResponseDto<OrganizationShowDto>.Success(200, organizationDto));
            }
            return CreateActionResult(CustomResponseDto<string>.Fail(409, "User can't list this organization"));
        }

        [HttpPost]
        public async Task<IActionResult> Add(OrganizationDto organizationDto)
        {
            Organization organization = _mapper.Map<Organization>(organizationDto);
            organization.Id = new Guid();
            var isAdmin = await _userService.VerifyAdmin();
            try
            {
                if(isAdmin == true)
                {
                    await _organizationService.Create(organization);
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Success(201));
                }
                return CreateActionResult(CustomResponseDto<string>.Fail(404, "User is not Admin"));
            }
            catch(Exception ex)
            {
                return CreateActionResult(CustomResponseDto<string>.Fail(409,ex.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(OrganizationUpdateDto organization)
        {
            var isAdmin = await _userService.VerifyAdmin();
            if (isAdmin == true)
            {
                var organizationInDb = await _organizationService.GetByIdAsync(organization.Id);
                organization.UpdatedDate = DateTime.UtcNow;
                await _service.UpdateAsync(organizationInDb, _mapper.Map<Organization>(organization));
                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
            }
            else
            {
                return CreateActionResult(CustomResponseDto<string>.Fail(404, "User is not Admin"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isAdmin = await _userService.VerifyAdmin();
            if(isAdmin == true)
            {
                var organization = await _organizationService.GetByIdAsync(id);
                await _organizationService.RemoveAsync(organization);
                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
            }
            else
            {
                return CreateActionResult(CustomResponseDto<string>.Fail(404, "User is not Admin"));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveRangeIds(List<string> ids)
        {
            await _organizationService.RemoveRangeIds(ids);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
        }
    }
}
