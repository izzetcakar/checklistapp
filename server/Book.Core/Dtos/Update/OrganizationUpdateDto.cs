using Book.Core.Dtos.Generic;
using Book.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Core.Dtos.Update
{
    public class OrganizationUpdateDto:BaseDtoModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}
