using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManagerAPI.Data;
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
        public async Task<IActionResult> GetTaskById(int id)
        {
            var taskItem = await _taskService.GetTaskItem(id);

            if (taskItem == null)
                return NotFound();

            return Ok(taskItem);
            //var tasks = await _taskService.GetAllTasks();
            //return Ok(tasks);
        }


        // Delete: api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskByID(int id)
        {
            try
            {
                var deletedTask = await _taskService.DeleteTask(id);
                return Ok($"Task '{deletedTask.Title}' gelöscht."); // falls du z.B. einen Namen hast
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Fehler beim Löschen: {ex.Message}");
            }
        }

        // Post: api/tasks/{id}
        [HttpPost("done/{id}")]
        public async Task<IActionResult> SetDoneTask(int id)
        {
            try
            {
                var Task = await _taskService.setDone(id);
                return Ok($"Task '{Task}' ist erledigt."); // falls du z.B. einen Namen hast
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Fehler beim Setzen auf Ferti: {ex.Message}");
            }

           
        }


        // Post: api/tasks/{id}
        [HttpPost("{id}")]
        public async Task<IActionResult> updateTask(int id, [FromBody] TaskItem updatedTask)
        {
            try
            {
                var Task = await _taskService.updateTask(id, updatedTask);
                return Ok($"Task '{Task?.Title}' wurde angepasst."); // falls du z.B. einen Namen hast
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Fehler beim Setzen auf Ferti: {ex.Message}");
            }
        }


        // POST: api/tasks
        [HttpPost]
        public async Task<IActionResult> Create(TaskItem task)
        {
            try
            {
                var createdTask = await _taskService.CreateTask(task);
                return CreatedAtAction(nameof(GetAll), new { id = createdTask.Id }, createdTask);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
