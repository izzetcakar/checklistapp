using AutoMapper;
using Book.Core.Dtos.Create;
using Book.Core.Dtos.Generic;
using Book.Core.Dtos.List;
using Book.Core.Dtos.Update;
using Book.Core.Models;
using Book.Core.Services;
using Microsoft.AspNetCore.Mvc;
using PageApp.API.Controllers;

namespace Book.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseOptionController : CustomBaseController
    {
        private readonly IService<Category> _categoryService;
        private readonly IService<Segment> _segmentService;
        private readonly IService<ControlList> _controlListService;
        private readonly IService<Consept> _conseptService;
        private readonly IService<Content> _contentService;
        private readonly IBaseOptionService _baseOptionService;
        private readonly IMapper _mapper;

        public BaseOptionController(IService<Category> categoryService, IService<Segment> segmentService,
            IService<ControlList> controlListService, IService<Consept> conseptService, IBaseOptionService baseOptionService,
            IMapper mapper, IService<Content> contentService)
        {
            _categoryService = categoryService;
            _segmentService = segmentService;
            _controlListService = controlListService;
            _conseptService = conseptService;
            _baseOptionService = baseOptionService;
            _mapper = mapper;
            _contentService = contentService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var baseOptions = await _baseOptionService.GetAll();
            return CreateActionResult(CustomResponseDto<BaseOptionShowDto>.Success(200, baseOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Add(BaseOptionCreateDto baseOptionDto)
        {
            switch (baseOptionDto.OptionType)
            {
                case BaseOptionType.Category:
                    Category category = _mapper.Map<Category>(baseOptionDto);
                    category.Id = Guid.NewGuid();
                    await _categoryService.AddAsync(category);
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Success(201));

                case BaseOptionType.Segment:
                    Segment segment = _mapper.Map<Segment>(baseOptionDto);
                    segment.Id = Guid.NewGuid();
                    await _segmentService.AddAsync(segment);
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Success(201));

                case BaseOptionType.ControlList:
                    ControlList controlList = _mapper.Map<ControlList>(baseOptionDto);
                    controlList.Id = Guid.NewGuid();
                    await _controlListService.AddAsync(controlList);
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Success(201));

                case BaseOptionType.Consept:
                    Consept consept = _mapper.Map<Consept>(baseOptionDto);
                    consept.Id = Guid.NewGuid();
                    await _conseptService.AddAsync(consept);
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Success(201));

                case BaseOptionType.Content:
                    Content content = _mapper.Map<Content>(baseOptionDto);
                    content.Id = Guid.NewGuid();
                    await _contentService.AddAsync(content);
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Success(201));

                default:
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Create Failed"));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(BaseOptionUpdateDto baseOptionDto)
        {
            switch (baseOptionDto.OptionType)
            {
                case BaseOptionType.Category:
                    var categoryInDb = await _categoryService.GetByIdAsync(baseOptionDto.Id);
                    await _categoryService.UpdateAsync(categoryInDb, _mapper.Map<Category>(baseOptionDto));
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));

                case BaseOptionType.Segment:
                    var segmentInDb = await _segmentService.GetByIdAsync(baseOptionDto.Id);
                    await _segmentService.UpdateAsync(segmentInDb, _mapper.Map<Segment>(baseOptionDto));
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));

                case BaseOptionType.ControlList:
                    var controlListInDb = await _controlListService.GetByIdAsync(baseOptionDto.Id);
                    await _controlListService.UpdateAsync(controlListInDb, _mapper.Map<ControlList>(baseOptionDto));
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));

                case BaseOptionType.Consept:
                    var conseptInDb = await _conseptService.GetByIdAsync(baseOptionDto.Id);
                    await _conseptService.UpdateAsync(conseptInDb, _mapper.Map<Consept>(baseOptionDto));
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));

                case BaseOptionType.Content:
                    var contentInDb = await _contentService.GetByIdAsync(baseOptionDto.Id);
                    await _contentService.UpdateAsync(contentInDb, _mapper.Map<Content>(baseOptionDto));
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));

                default:
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Update Failed"));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(BaseOptionGetDto baseOptionDto)
        {
            switch (baseOptionDto.OptionType)
            {
                case BaseOptionType.Category:
                    var category = await _categoryService.GetByIdAsync(baseOptionDto.Id);
                    await _categoryService.RemoveAsync(category);
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));

                case BaseOptionType.Segment:
                    var segment = await _segmentService.GetByIdAsync(baseOptionDto.Id);
                    await _segmentService.RemoveAsync(segment);
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));

                case BaseOptionType.ControlList:
                    var controlList = await _controlListService.GetByIdAsync(baseOptionDto.Id);
                    await _controlListService.RemoveAsync(controlList);
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));

                case BaseOptionType.Consept:
                    var consept = await _conseptService.GetByIdAsync(baseOptionDto.Id);
                    await _conseptService.RemoveAsync(consept);
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));

                case BaseOptionType.Content:
                    var content = await _contentService.GetByIdAsync(baseOptionDto.Id);
                    await _contentService.RemoveAsync(content);
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));

                default:
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Delete Failed"));
            }
        }
        [HttpPost("AddRangeContent")]
        public async Task<IActionResult> AddRange(ContentCreateDto inputTitles)
        {
            string[] titles = inputTitles.Title.Split("# ");
            List<Content> contents = new List<Content>();
            foreach (var title in titles)
            {
                Content content = new Content();
                content.Id = Guid.NewGuid();
                content.Title = title;
                contents.Add(content);
            }
            await _contentService.AddRangeAsync(contents);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(201));
        }
    }
}
