using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace UsersApp.Models
{
    public class DB : DbContext
    {
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Employee> Employes { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectStatus> ProjectStatus { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<TaskStatus> TaskStatus { get; set; }
        public DbSet<Reports> Reports { get; set; }

        public DB() : base("Data Source=DESKTOP-VMEPNO2\\MYSQLEXPRESS; Initial Catalog=Vector; Integrated Security=True;")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasRequired(p => p.Employees)
                .WithMany(e => e.Projects)
                .HasForeignKey(p => p.EmployeeId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}

