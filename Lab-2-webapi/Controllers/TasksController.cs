using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab_2_webapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab_2_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private TasksDbContext context;
        public TasksController(TasksDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets all the tasks.
        /// </summary>
        
        /// <param name="from">Optional, filter by minimum Deadline</param>
        /// <param name="to">Optional, filter by maximum Deadline</param>
        /// <returns>A list of Task objects</returns>
        [HttpGet]
        public IEnumerable<Models.Task> Get([FromQuery]DateTime? from, [FromQuery]DateTime? to)
        {
            IQueryable<Models.Task> result = context.Tasks.Include(t =>t.Comments);
            if (from == null & to == null)
            {
                return result;
            }
            if(from != null)
            {
                result = result.Where(t => t.Deadline >= from);
            }
            if (to != null)
            {
                result = result.Where(t => t.Deadline <= to);
            }
            return result;
        }

        // GET: api/Tasks/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var existing = context.Tasks
                .Include(t => t.Comments)
                .FirstOrDefault(task => task.Id == id);
            if (existing == null)
            {
                return NotFound();
            }
            return Ok(existing);
        }

        /// <summary>
        /// Add a task
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     POST /Tasks
        ///     {
        ///         "title": "Todo with comment",
        ///         "description": "description4",
        ///         "dateAdded": "2019-05-06T00:00:00",
        ///         "deadline": "2019-05-15T00:00:00",
        ///         "imp": 1,
        ///         "closedAt": "2019-05-17T00:00:00",
        ///         "comments": [
        ///         	{
        ///         		"text": "morning task",
        ///         		"important": true
        ///
        ///             },
        ///         	{
        ///		
        ///         		"text": "first task",
        ///         		"important": false
        ///         	}
        ///	        ]
        ///     }
        ///
        /// </remarks>
        /// <param name="task">The task to add.</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public void Post([FromBody] Models.Task task)
        {

            //if(!ModelState.IsValid)
            //{
            //}
            context.Tasks.Add(task);
            context.SaveChanges();
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Models.Task task)
        {
            var existing = context.Tasks.AsNoTracking().FirstOrDefault(f => f.Id == id);
            if (existing == null)
            {
                context.Tasks.Add(task);
                context.SaveChanges();
                return Ok(task);
            }
            task.Id = id;
            context.Tasks.Update(task);
            context.SaveChanges();
            return Ok(task);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = context.Tasks.FirstOrDefault(task => task.Id == id);
            if (existing == null)
            {
                return NotFound();
            }
            context.Tasks.Remove(existing);
            context.SaveChanges();
            return Ok();
        }
    }
}
