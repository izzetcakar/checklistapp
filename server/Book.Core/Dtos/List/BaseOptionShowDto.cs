using Book.Core.Dtos.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Core.Dtos.List
{
    public class BaseOptionShowDto
    {
        public List<BaseOptionDto>? Categories { get; set; }
        public List<BaseOptionDto>? Segments { get; set; }
        public List<BaseOptionDto>? ControlLists { get; set; }
        public List<BaseOptionDto>? Consepts { get; set; }
        public List<BaseOptionDto> Contents { get; set; }
    }
}
