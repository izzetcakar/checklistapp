using Book.Core.Dtos.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Core.Models
{
    public enum Status
    {
        Rejected,
        Allowed,
        Waiting
    }
    public class Permission:BaseModel
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public Guid OrganizationId { get; set; }
        public bool? CanList { get; set; }
        public bool? CanAdd { get; set; }
        public bool? CanDelete { get; set; }
        public bool? CanEdit { get; set; }
        public Status Status { get; set; }
    }
}
