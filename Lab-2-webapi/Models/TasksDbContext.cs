using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_2_webapi.Models
{

    //DbContext = Unit of work
    public class TasksDbContext :DbContext
    {
        public TasksDbContext(DbContextOptions<TasksDbContext> optons) : base(optons)
        {
        }
        public DbSet<Task> Tasks { get; set; }
    }
}
