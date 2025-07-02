using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersApp.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [ForeignKey("Employees")]
        public int? EmployeeId { get; set; }
        public virtual Employee Employees { get; set; }

        [ForeignKey("ProjectStatus")]
        public int ProjectStatusId { get; set; }
        public virtual ProjectStatus ProjectStatus { get; set; }
    }
}
