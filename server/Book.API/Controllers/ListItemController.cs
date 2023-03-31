using AutoMapper;
using Book.Core.Dtos.Create;
using Book.Core.Dtos.Generic;
using Book.Core.Dtos.List;
using Book.Core.Dtos.Update;
using Book.Core.Models;
using Book.Core.Services;
using Book.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PageApp.API.Controllers;
using System.Collections.Generic;

namespace Book.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListItemController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<ListItem> _service;
        private readonly IListItemService _listItemService;
        private readonly IUserService _userService;
        public ListItemController(IMapper mapper, IService<ListItem> service, IListItemService listItemService, IUserService userService)
        {
            _mapper = mapper;
            _service = service;
            _listItemService = listItemService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var listItems = _listItemService.GetAllAsync().Result.ToList();
            var listItemDtos = _mapper.Map<List<ListItemShowDto>>(listItems).ToList();
            return CreateActionResult(CustomResponseDto<List<ListItemShowDto>>.Success(200, listItemDtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var listItem = await _listItemService.GetByIdAsync(id);
            var listItemDto = _mapper.Map<ListItemShowDto>(listItem);
            return CreateActionResult(CustomResponseDto<ListItemShowDto>.Success(200, listItemDto));
        }
        [HttpGet("GetByChkId/{id}")]
        public async Task<IActionResult> GetAllByOrgId(Guid id)
        {
            var listItems = await _listItemService.GetByChkId(id);
            var listItemsDto = _mapper.Map<List<ListItemShowDto>>(listItems).ToList();
            return CreateActionResult(CustomResponseDto<List<ListItemShowDto>>.Success(200, listItemsDto));
        }

        [HttpPost]
        public async Task<IActionResult> Add(ListItemCreateDto dto)
        {
            var userId = _userService.GetId();
            var id = new Guid(userId);
            try
            {
                await _listItemService.CreateList(dto, id);
                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<string>.Fail(404, ex.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(ListItemUpdateDto dto)
        {
            var userId = _userService.GetId();
            var id = new Guid(userId);
            try
            {
                await _listItemService.UpdateList(dto, id);
                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<string>.Fail(404, ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid listId)
        {
            var userId = _userService.GetId();
            var id = new Guid(userId);
            try
            {
                await _listItemService.DeleteList(listId, id);
                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<string>.Fail(404, ex.Message));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveRangeIds(List<string> ids)
        {
            var userId = _userService.GetId();
            var id = new Guid(userId);
            try
            {
                await _listItemService.DeleteLists(ids, id);
                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<string>.Fail(404, ex.Message));
            }
        }
    }
}
