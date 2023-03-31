using AutoMapper;
using Book.Core.Dtos.Create;
using Book.Core.Dtos.Update;
using Book.Core.Models;
using Book.Core.Repositories;
using Book.Core.Services;
using Book.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Service.Services
{
    public class ListItemService : Service<ListItem>, IListItemService
    {
        private readonly IListItemRepository _listItemRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ListItemService(IGenericRepository<ListItem> repository, IUnitOfWork unitOfWork,
            IListItemRepository listItemRepository,IMapper mapper) : base(repository, unitOfWork)
        {
            _listItemRepository = listItemRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateList(ListItemCreateDto dto, Guid userId)
        {
            await _listItemRepository.CreateList(dto, userId);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteList(Guid listId, Guid userId)
        {
            await _listItemRepository.DeleteList(listId, userId);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteLists(List<string> ids, Guid userId)
        {
            await _listItemRepository.DeleteLists(ids, userId);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ListItem>> GetByChkId(Guid id)
        {
            return await _listItemRepository.GetByChkId(id);
        }

        public async Task UpdateList(ListItemUpdateDto dto, Guid userId)
        {
            await _listItemRepository.UpdateList(dto, userId);
            await _unitOfWork.CommitAsync();
        }
    }
}
