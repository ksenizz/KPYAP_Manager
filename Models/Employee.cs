using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsersApp.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FIO { get; set; }
        public string Password { get; set; }

        [ForeignKey("Role")]
        public int Role_Id { get; set; }

        public virtual Roles Role { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
