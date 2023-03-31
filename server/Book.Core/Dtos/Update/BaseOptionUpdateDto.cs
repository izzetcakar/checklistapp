using Book.Core.Dtos.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Core.Dtos.Update
{
    public class BaseOptionUpdateDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public BaseOptionType OptionType { get; set; }
    }
}
