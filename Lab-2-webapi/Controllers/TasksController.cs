using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab_2_webapi.Models;
using Lab_2_webapi.Services;
using Lab_2_webapi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab_2_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private ITaskService taskService;
        public TasksController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        /// <summary>
        /// Gets all the tasks.
        /// </summary>
        
        /// <param name="from">Optional, filter by minimum Deadline</param>
        /// <param name="to">Optional, filter by maximum Deadline</param>
        /// <returns>A list of Task objects</returns>
        [HttpGet]
        public IEnumerable<TaskGetModel> Get([FromQuery]DateTime? from, [FromQuery]DateTime? to)
        {
            return taskService.GetAll(from, to);
        }

        // GET: api/Tasks/5
        [HttpGet("{id}", Name = "Get")]

        public IActionResult Get(int id)
        {
            var found = taskService.GetById(id);
            if (found == null)
            {
                return NotFound();
            }
            return Ok(found);
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
            taskService.Create(task);
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Models.Task task)
        {
            var result = taskService.Upsert(id, task);
            return Ok(result);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = taskService.Delete(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
