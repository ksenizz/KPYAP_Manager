using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersApp.Models;

namespace UsersApp.Services
{
    public class EmployeeService
    {
        private readonly DB _context;

        public EmployeeService(DB context)
        {
            _context = context;
        }

        public List<Employee> GetAllEmployees()
        {
            return _context.Employes.ToList();
        }

        public List<Employee> GetAllManagers()
        {
            return _context.Employes
                .Where(e => e.Role.Name == "Менеджер") 
                .ToList();
        }

        public List<Employee> GetAllDevelopersAndDesigners()
        {
            return _context.Employes
                .Where(e => e.Role.Name == "Разработчик" || e.Role.Name == "Дизайнер")
                .ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return _context.Employes.Find(id);
        }

        public void AddEmployee(Employee employee)
        {
            _context.Employes.Add(employee);
            _context.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            var employee = _context.Employes.Find(id);
            if (employee != null)
            {
                _context.Employes.Remove(employee);
                _context.SaveChanges();
            }
        }

        public List<Employee> GetAllDesigners()
        {
            return _context.Employes.Where(e => e.Role.Name == "Дизайнер").ToList();
        }

        public List<Employee> GetAllDevelopers()
        {
            return _context.Employes.Where(e => e.Role.Name == "Разработчик").ToList();
        }
        public void UpdateProject(Project project)
        {
            var existingProject = _context.Projects.Find(project.Id);
            if (existingProject != null)
            {
                existingProject.Name = project.Name;
                existingProject.Description = project.Description;
                existingProject.CreatedDate = project.CreatedDate;
                existingProject.UpdatedDate = project.UpdatedDate;
                existingProject.ProjectStatusId = project.ProjectStatusId;
                existingProject.EmployeeId = project.EmployeeId; 
                _context.SaveChanges();
            }
        }
    }
}
