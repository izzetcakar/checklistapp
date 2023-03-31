using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Core.Models
{
    public class Subscription:BaseModel
    {
        public Guid UserId { get; set; }
        public Guid OrganizationId { get; set; }
        public User User { get; set; }
        public Organization Organization { get; set; }
        public bool CanDelete { get; set; }
        public bool CanEdit { get; set; }
        public bool CanAdd { get; set; }
        public bool CanList { get; set; }
    }
}
