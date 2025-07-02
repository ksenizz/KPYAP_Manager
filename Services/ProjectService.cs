using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersApp.Models;

namespace UsersApp.Services
{
    public class ProjectService
    {
        private readonly DB _context;

        public ProjectService(DB context)
        {
            _context = context;
        }

        public List<Project> GetAllProjects()
        {
            return _context.Projects.ToList();
        }

        public List<ProjectStatus> GetAllProjectStatuses()
        {
            return _context.ProjectStatus.ToList();
        }

        public Project GetProjectById(int id)
        {
            return _context.Projects.Find(id);
        }

        public void AddProject(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
        }

        public void UpdateProject(Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteProject(int id)
        {
            var project = _context.Projects.Find(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                _context.SaveChanges();
            }
        }
    }
}
