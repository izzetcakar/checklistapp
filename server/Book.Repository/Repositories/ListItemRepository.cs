using AutoMapper;
using Book.Core.Dtos.Create;
using Book.Core.Dtos.Generic;
using Book.Core.Dtos.Update;
using Book.Core.Models;
using Book.Core.Repositories;
using Book.Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Book.Repository.Repositories
{
    public class ListItemRepository : GenericRepository<ListItem>, IListItemRepository
    {
        private readonly IMapper _mapper;
        private readonly Dictionary<string, double> risks = new Dictionary<string, double>(){
            {"A",10}, {"B",8}, {"C",6}, {"D",4}, {"E",2}, {"F",0},
        };
        public ListItemRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }

        public Task<List<ListItem>> GetByChkId(Guid id)
        {
            return dbContext.ListItems.Where(x => x.CheckListId == id).AsNoTracking().ToListAsync();
        }

        public async Task RemoveRangeIds(List<string> Ids)
        {
            var listItems = await dbContext.ListItems.Where(x => Ids.Contains(x.Id.ToString())).AsNoTracking().ToListAsync();
            dbContext.RemoveRange(listItems);
        }

        public async Task CreateList(ListItemCreateDto dto, Guid userId)
        {
            var checklist = await dbContext.Checklists.AsNoTracking().SingleOrDefaultAsync(x => x.Id == dto.CheckListId);
            var sub = await dbContext.Subscriptions.AsNoTracking().SingleOrDefaultAsync(x => x.UserId == userId && x.OrganizationId == checklist.OrganizationId);
            if (sub != null && sub.CanAdd == true)
            {
                ListItem listItem = _mapper.Map<ListItem>(dto);
                listItem.Id = new Guid();
                listItem.ItemScore = risks[listItem.Risk.ToString()];
                listItem.Result = listItem.Relevance / listItem.ItemScore;
                await dbContext.ListItems.AddAsync(listItem);
                checklist.UpdatedDate = DateTime.UtcNow;
                dbContext.Checklists.Update(checklist);
            }
            else
            {
                throw new Exception("User can not add");
            }
        }
        public async Task UpdateList(ListItemUpdateDto dto, Guid userId)
        {
            var checklist = await dbContext.Checklists.AsNoTracking().SingleOrDefaultAsync(x => x.Id == dto.ChecklistId);
            var sub = await dbContext.Subscriptions.AsNoTracking().SingleOrDefaultAsync(x => x.UserId == userId && x.OrganizationId == checklist.OrganizationId);    
            if(sub != null && sub.CanEdit == true)
            {
                var listItem = await dbContext.ListItems.AsNoTracking().SingleOrDefaultAsync(x => x.Id == dto.Id);
                listItem = _mapper.Map<ListItem>(dto);
                listItem.UpdatedDate = DateTime.UtcNow;
                listItem.ItemScore = risks[dto.Risk.ToString()];
                listItem.Result = dto.Relevance / listItem.ItemScore;
                checklist.UpdatedDate = DateTime.UtcNow;
                dbContext.ListItems.Update(listItem);
                dbContext.Checklists.Update(checklist);
            }
            else
            {
                throw new Exception("User can not update");
            }
        }

        public async Task DeleteList(Guid listId, Guid userId)
        {
            var listItem = await dbContext.ListItems.AsNoTracking().SingleOrDefaultAsync(x => x.Id == listId);
            var checklist = await dbContext.Checklists.AsNoTracking().SingleOrDefaultAsync(x => x.Id == listItem.CheckListId);
            var sub = await dbContext.Subscriptions.AsNoTracking().SingleOrDefaultAsync(x => x.UserId == userId && x.OrganizationId == checklist.OrganizationId);
            if (sub != null && sub.CanDelete == true)
            {
                dbContext.ListItems.Remove(listItem);
                checklist.UpdatedDate = DateTime.UtcNow;
                dbContext.Checklists.Update(checklist);
            }
            else
            {
                throw new Exception("User can not delete");
            }
        }

        public async Task DeleteLists(List<string> ids, Guid userId)
        {
            Guid listId = new Guid(ids[0]);
            var listItem = await dbContext.ListItems.AsNoTracking().SingleOrDefaultAsync(x => x.Id == listId);
            var checklist = await dbContext.Checklists.AsNoTracking().SingleOrDefaultAsync(x => x.Id == listItem.CheckListId);
            var sub = await dbContext.Subscriptions.AsNoTracking().SingleOrDefaultAsync(x => x.UserId == userId && x.OrganizationId == checklist.OrganizationId);
            if (sub.CanDelete == true)
            {
                await RemoveRangeIds(ids);
                dbContext.Checklists.Update(checklist);
            }
            else
            {
                throw new Exception("User can not delete");
            }
        }
    }
}
