using AutoMapper;
using Book.Core.Dtos.Generic;
using Book.Core.Dtos.List;
using Book.Core.Dtos.Update;
using Book.Core.Models;
using Book.Core.Services;
using Book.Service.Services;
using Microsoft.AspNetCore.Mvc;
using PageApp.API.Controllers;

namespace Book.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IPermissionService _permissionService;
        private readonly IOrganizationService _organizationService;
        private readonly IUserService _userService;

        public PermissionController(IMapper mapper, IPermissionService permissionService,
            IUserService userService, IOrganizationService organizationService)
        {
            _mapper = mapper;
            _permissionService = permissionService;
            _userService = userService;
            _organizationService = organizationService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var allPermissions = await _permissionService.GetAllAsync();
            var allPermissionDtos = _mapper.Map<List<PermissionShowDto>>(allPermissions).ToList();
            foreach (var permission in allPermissionDtos)
            {
                permission.Title = _organizationService.GetByIdAsync(permission.OrganizationId).Result.Title;
                permission.Username = _userService.GetByIdAsync(permission.UserId).Result.UserName;
            }
            return CreateActionResult(CustomResponseDto<List<PermissionShowDto>>.Success(200, allPermissionDtos));
        }
        
        [HttpGet("getByUser")]
        public async Task<IActionResult> GetAllbyUser()
        {
            var userId = await _userService.GetIdByToken();
            var allPermissions = await _permissionService.GetAllByUserId(userId);
            var allPermissionDtos = _mapper.Map<List<PermissionShowDto>>(allPermissions).ToList();
            foreach (var permission in allPermissionDtos)
            {
                permission.Title = _organizationService.GetByIdAsync(permission.OrganizationId).Result.Title;
                permission.Username = _userService.GetByIdAsync(permission.UserId).Result.UserName;
            }
            return CreateActionResult(CustomResponseDto<List<PermissionShowDto>>.Success(200, allPermissionDtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var permission = await _permissionService.GetByIdAsync(id);
            var permissionDto = _mapper.Map<PermissionShowDto>(permission);

            permissionDto.Title = _organizationService.GetByIdAsync(permissionDto.OrganizationId).Result.Title;
            permissionDto.Username = _userService.GetByIdAsync(permissionDto.UserId).Result.UserName;
            
            return CreateActionResult(CustomResponseDto<PermissionShowDto>.Success(200, permissionDto));
        }

        [HttpPost]
        public async Task<IActionResult> Add(PermissionDto permissionDto)
        {
            var userId = await _userService.GetIdByToken();
            permissionDto.UserId = userId;
            permissionDto.Status = Status.Waiting;
            Permission permissionRequest = _mapper.Map<Permission>(permissionDto);
            permissionRequest.Id = Guid.NewGuid();
            await _permissionService.AddAsync(permissionRequest);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(201));
        }

        [HttpPut]
        public async Task<IActionResult> Update(PermissionUpdateDto permissionUpdateDto)
        {
            var permissionRequestInDb = await _permissionService.GetByIdAsync(permissionUpdateDto.Id);
            permissionUpdateDto.UpdatedDate = DateTime.UtcNow;
            await _permissionService.UpdateAsync(permissionRequestInDb, _mapper.Map<Permission>(permissionUpdateDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
        }

        [HttpPut("reply")]
        public async Task<IActionResult> ReplyRequest(PermissionUpdateDto permissionReqUpdateDto)
        {
            var isAdmin = await _userService.VerifyAdmin();
            if (isAdmin == true)
            {
                await _permissionService.ReplyRequest(permissionReqUpdateDto);
                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
            }
            return CreateActionResult(CustomResponseDto<string>.Fail(404,"User is Not Admin"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var permissinReq = await _permissionService.GetByIdAsync(id);
            await _permissionService.RemoveAsync(permissinReq);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
        }
    }
}
