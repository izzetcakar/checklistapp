using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book.Core.Dtos.Generic;

namespace Book.Core.Dtos.List
{
    public class OrganizationShowDto:BaseDtoModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}
