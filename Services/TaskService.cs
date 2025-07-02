using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersApp.Models;

namespace UsersApp.Services
{
    public class TaskService
    {
        private readonly DB _context;

        public TaskService(DB context)
        {
            _context = context;
        }

        public List<Tasks> GetAllTasks()
        {
            return _context.Tasks
                .Include(t => t.Status)
                .Include(t => t.Employee)
                .Include(t => t.Project)
                .AsNoTracking()
                .ToList();
        }

        public List<UsersApp.Models.TaskStatus> GetAllTaskStatuses()
        {
            return _context.TaskStatus.AsNoTracking().ToList();
        }

        public Tasks GetTaskById(int id)
        {
            return _context.Tasks.Find(id);
        }

        public void AddTask(Tasks task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(Tasks task)
        {
            using (var context = new DB())
            {
                var existingTask = context.Tasks.Find(task.Id);
                if (existingTask != null)
                {
                    context.Entry(existingTask).CurrentValues.SetValues(task);
                    context.SaveChanges();
                }
            }
        }

        public void DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }
    }
}

