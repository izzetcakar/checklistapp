using Book.Core.Dtos.Generic;
using Book.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Core.Dtos.Update
{
    public class ListItemUpdateDto:BaseDtoModel
    {
        public Guid Id { get; set; }
        public Guid ChecklistId { get; set; }
        public Guid ConseptId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SegmentId { get; set; }
        public Guid ContentId { get; set; }
        public Guid ControlListId { get; set; }
        public int Standard { get; set; }
        public Risk Risk { get; set; }
        public double Relevance { get; set; }
        public string? Note { get; set; }
        public string Department { get; set; }
    }
}
