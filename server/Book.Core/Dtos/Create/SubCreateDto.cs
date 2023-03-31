using Book.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Core.Dtos.Create
{
    public class SubCreateDto
    {
        public Guid UserId { get; set; }
        public Guid OrganizationId { get; set; }
        public bool CanDelete { get; set; }
        public bool CanEdit { get; set; }
        public bool CanAdd { get; set; }
        public bool CanList { get; set; }
    }
}
