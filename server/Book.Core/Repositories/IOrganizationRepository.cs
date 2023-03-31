﻿using Book.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Core.Repositories
{
    public interface IOrganizationRepository : IGenericRepository<Organization>
    {
        Task RemoveRangeIds(List<string> Ids);
        Task Create(Organization organization);
    }
}
