using Book.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Core.Dtos.Generic
{
    public class ChecklistDto
    {
        public Guid OrganizationId { get; set; }
        public string Title { get; set; }
    }
}
