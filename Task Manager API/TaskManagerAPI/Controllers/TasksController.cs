using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Models;
using TaskManagerAPI.Services;

namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskService.GetAllTasks();
            return Ok(tasks);
        }

        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _taskService.GetTaskItem(id);
            if (task == null)
                return NotFound($"Task mit ID {id} nicht gefunden.");

            return Ok(task);
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskItem task)
        {
            var createdTask = await _taskService.CreateTask(task);
            return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
        }

        // PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskItem updatedTask)
        {
            var task = await _taskService.UpdateTask(id, updatedTask);
            return Ok(task);
        }

        // POST: api/tasks/{id}/done
        [HttpPost("{id}/done")]
        public async Task<IActionResult> SetDone(int id)
        {
            var task = await _taskService.SetDone(id);
            return Ok(task);
        }

        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedTask = await _taskService.DeleteTask(id);
            return Ok(deletedTask);
        }
    }
}
