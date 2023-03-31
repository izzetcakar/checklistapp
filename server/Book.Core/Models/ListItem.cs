using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Core.Models
{
    public enum Risk
    {
        A, B, C, D, E, F
    }
    public class ListItem:BaseModel
    {
        public Guid CheckListId { get; set; }
        public Guid? ConseptId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? SegmentId { get; set; }
        public Guid? ContentId { get; set; }
        public Guid? ControlListId { get; set; }
        public Checklist Checklist { get; set; }
        public Consept? Consept { get; set; }
        public Category? Category { get; set; }
        public Segment? Segment { get; set; }
        public Content? Content { get; set; }
        public ControlList? ControlList { get; set; }
        public int? Standard { get; set; }
        public Risk Risk { get; set; }
        public double? ItemScore { get; set; }
        public double? Relevance { get; set; }
        public double? Result { get; set; }
        public string? Note { get; set; }
        public string Department { get; set; }
    }
}
