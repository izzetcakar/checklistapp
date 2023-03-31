using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Core.Models
{
    public class Organization:BaseModel
    {
        public string Title { get; set; }
        public ICollection<Checklist>? CheckLists { get; set; }
        public ICollection<Subscription>? Subs { get; set; }
    }
}
