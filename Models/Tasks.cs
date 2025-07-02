using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersApp.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public DateTime Deadline { get; set; }

        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public virtual TaskStatus Status { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }


        [NotMapped]
        public int DaysRemaining
        {
            get
            {
                TimeSpan remainingTime = Deadline - DateTime.Now;
                return remainingTime.Days;
            }
        }

    }

}
