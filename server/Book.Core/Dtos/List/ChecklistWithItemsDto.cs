using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Core.Dtos.List
{
    public class ChecklistWithItemsDto
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public List<ListItemShowDto> ListItems { get; set; }
        public string Title { get; set; }
    }
}
