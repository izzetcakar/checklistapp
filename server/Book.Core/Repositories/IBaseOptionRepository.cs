using Book.Core.Dtos.List;
using Book.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Core.Repositories
{
    public interface IBaseOptionRepository:IGenericRepository<BaseOption>
    {
        Task<BaseOptionShowDto> GetAll();
    }
}
