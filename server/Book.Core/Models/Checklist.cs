using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Core.Models
{
    public class Checklist :BaseModel
    {
        public Organization Organization { get; set; }
        public Guid OrganizationId { get; set; }
        public ICollection<ListItem>? ListItems { get; set; }
        public string Title { get; set; }
    }
}
