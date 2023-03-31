using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Core.Dtos.Create
{
    public enum BaseOptionType
    {
        Category,
        Segment,
        ControlList,
        Consept,
        Content
    }
    public class BaseOptionCreateDto
    {
        public string Title { get; set; }
        public BaseOptionType OptionType { get; set; }
    }
}
