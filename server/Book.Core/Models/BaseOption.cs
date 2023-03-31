using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Core.Models
{
    public abstract class BaseOption:BaseModel
    {
        public ICollection<ListItem> ListItems { get; set; }
        public string Title { get; set; }
    }
}
