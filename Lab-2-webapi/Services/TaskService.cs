using Lab_2_webapi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Lab_2_webapi.Services
{
    public interface ITaskService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        IEnumerable<Task> GetAll(DateTime? from=null, DateTime? to=null);
        Task GetById(int id);
        Task Create(Task task);
        Task Upsert(int id, Task task);
        Task Delete(int id);
    }
    public class TaskService : ITaskService
    {
        private Models.TasksDbContext context;
        public TaskService(Models.TasksDbContext context)
        {
            this.context = context;
        }

        public Task Create(Task task)
        {
            context.Tasks.Add(task);
            context.SaveChanges();
            return task;
        }

        public Task Delete(int id)
        {
            var existing = context.Tasks
                .Include(t => t.Comments)
                .FirstOrDefault(task => task.Id == id);
            if (existing == null)
            {
                return null;
            }
            context.Tasks.Remove(existing);
            context.SaveChanges();
            return existing;
        }

        public IEnumerable<Task> GetAll(DateTime? from=null, DateTime? to=null)
        {
            IQueryable<Models.Task> result = context.Tasks.Include(t => t.Comments);
            if (from == null & to == null)
            {
                return result;
            }
            if (from != null)
            {
                result = result.Where(t => t.Deadline >= from);
            }
            if (to != null)
            {
                result = result.Where(t => t.Deadline <= to);
            }
            return result;
        }

        public Task GetById(int id)
        {
           //sau  context.Tasks.Find()
            return context.Tasks
                .Include(t => t.Comments)
                .FirstOrDefault(t => t.Id == id);
        }

        public Task Upsert(int id, Task task)
        {
            var existing = context.Tasks.AsNoTracking().FirstOrDefault(f => f.Id == id);
            if (existing == null)
            {
                context.Tasks.Add(task);
                context.SaveChanges();
                return task;
            }
            task.Id = id;
            context.Tasks.Update(task);
            context.SaveChanges();
            return task;
        }
    }
}
