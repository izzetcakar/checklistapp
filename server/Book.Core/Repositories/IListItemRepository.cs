using Book.Core.Dtos.Create;
using Book.Core.Dtos.Update;
using Book.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Core.Repositories
{
    public interface IListItemRepository:IGenericRepository<ListItem>
    {
        Task RemoveRangeIds(List<string> Ids);
        Task<List<ListItem>> GetByChkId(Guid id);
        Task UpdateList(ListItemUpdateDto dto, Guid userId);
        Task CreateList(ListItemCreateDto dto, Guid userId);
        Task DeleteList(Guid listId, Guid userId);
        Task DeleteLists(List<string> ids, Guid userId);
    }
}
