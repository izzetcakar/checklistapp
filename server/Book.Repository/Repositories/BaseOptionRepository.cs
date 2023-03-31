using AutoMapper;
using Book.Core.Dtos.Generic;
using Book.Core.Dtos.List;
using Book.Core.Models;
using Book.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Repository.Repositories
{
    public class BaseOptionRepository : GenericRepository<BaseOption>, IBaseOptionRepository
    {
        private readonly IMapper _mapper;
        public BaseOptionRepository(AppDbContext dbContext,IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }

        public async Task<BaseOptionShowDto> GetAll()
        {
            BaseOptionShowDto baseOption = new BaseOptionShowDto();
            var categories = await dbContext.Categories.AsNoTracking().ToListAsync();
            var segments = await dbContext.Segments.AsNoTracking().ToListAsync();
            var controlLists = await dbContext.ControlLists.AsNoTracking().ToListAsync();
            var consepts = await dbContext.Consepts.AsNoTracking().ToListAsync();
            var contents = await dbContext.Contents.AsNoTracking().ToListAsync();
            baseOption.Categories = _mapper.Map<List<BaseOptionDto>>(categories).ToList();
            baseOption.Segments = _mapper.Map<List<BaseOptionDto>>(segments).ToList();
            baseOption.ControlLists = _mapper.Map<List<BaseOptionDto>>(controlLists).ToList();
            baseOption.Consepts = _mapper.Map<List<BaseOptionDto>>(consepts).ToList();
            baseOption.Contents = _mapper.Map<List<BaseOptionDto>>(contents).ToList();
            return baseOption;
        }
    }
}
