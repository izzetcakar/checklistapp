using AutoMapper;
using Book.Core.Dtos.Create;
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
    public class ChecklistController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<Checklist> _service;
        private readonly IChecklistService _checklistService;
        private readonly ISubService _subService;
        private readonly IUserService _userService;
        public ChecklistController(IMapper mapper, IService<Checklist> service, IChecklistService checklistService,
            ISubService subService, IUserService userService)
        {
            _mapper = mapper;
            _service = service;
            _checklistService = checklistService;
            _subService = subService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var checklists = _checklistService.GetAllAsync().Result.ToList();
            var checklistDtos = _mapper.Map<List<ChecklistShowDto>>(checklists).ToList();
            return CreateActionResult(CustomResponseDto<List<ChecklistShowDto>>.Success(200,checklistDtos));
        }
        [HttpGet("GetWithItems/{id}")]
        public async Task<IActionResult> GetWithItems(Guid id)
        {
            var checklists = _checklistService.GetWithtems(id).Result;
            var checklistDto = _mapper.Map<ChecklistWithItemsDto>(checklists);
            return CreateActionResult(CustomResponseDto<ChecklistWithItemsDto>.Success(200, checklistDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var checklist = await _checklistService.GetByIdAsync(id);
            var checklistDto = _mapper.Map<ChecklistShowDto>(checklist);
            return CreateActionResult(CustomResponseDto<ChecklistShowDto>.Success(200, checklistDto));
        }
        [HttpGet("GetByOrgId/{id}")]
        public async Task<IActionResult> GetAllByOrgId(Guid id)
        {
            var checklists = await _checklistService.GetByOrgId(id);
            var checklistsDto = _mapper.Map<List<ChecklistShowDto>>(checklists).ToList();
            return CreateActionResult(CustomResponseDto<List<ChecklistShowDto>>.Success(200, checklistsDto));
        }

        [HttpPost]
        public async Task<IActionResult> Add(ChecklistDto checklistDto)
        {
            var sub = await _subService.GetSubByOrgId(checklistDto.OrganizationId);
            if (sub.CanAdd == true)
            {
                Checklist checklist = _mapper.Map<Checklist>(checklistDto);
                checklist.Id = Guid.NewGuid();
                await _checklistService.AddAsync(checklist);
                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(201));
            }
            else
            {
                return CreateActionResult(CustomResponseDto<string>.Fail(404,"User can not add"));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(ChecklistUpdateDto checklistDto)
        {
            var sub = await _subService.GetSubByOrgId(checklistDto.OrganizationId);
            if (sub.CanEdit == true)
            {
                var checklistInDb = await _checklistService.GetByIdAsync(checklistDto.Id);
                checklistDto.UpdatedDate = DateTime.UtcNow;
                await _checklistService.UpdateAsync(checklistInDb, _mapper.Map<Checklist>(checklistDto));
                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
            }
            else
            {
                return CreateActionResult(CustomResponseDto<string>.Fail(404, "User can not update"));
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var checklist = await _checklistService.GetByIdAsync(id);
            var sub = await _subService.GetSubByOrgId(checklist.OrganizationId);
            if (sub.CanDelete == true)
            {
                await _checklistService.RemoveAsync(checklist);
                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
            }
            else
            {
                return CreateActionResult(CustomResponseDto<string>.Fail(404, "User can not delete"));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveRangeIds(List<string> ids)
        {
            await _checklistService.RemoveRangeIds(ids);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
        }
    }
}
