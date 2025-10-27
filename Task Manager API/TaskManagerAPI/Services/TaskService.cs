using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManagerAPI.Data;
using TaskManagerAPI.Models;
using TaskManagerAPI.Utilities;


namespace TaskManagerAPI.Services
{
    public class TaskService
    {

        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<IEnumerable<TaskItem>> GetAllTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // GET Item With ID
        public async Task<TaskItem?> GetTaskItem(int taskId)
        {
            return await _context.Tasks.FindAsync(taskId);
        }

        // Set Done
        public async Task<TaskItem?> setDone(int taskId)
        {
            
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null)
                throw new ArgumentException("Task not found");

            task.IsCompleted = true;
            await _context.SaveChangesAsync();

            return task;

        }

        // Update task
        public async Task<TaskItem?> updateTask(int taskId, TaskItem updatedTask)
        {

            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null)
                throw new ArgumentException("Task not found");


            task.Title = UpdateHelper.KeepOriginalIfEmpty(task.Title, updatedTask.Title);
            task.Description = UpdateHelper.KeepOriginalIfEmpty(task.Description, updatedTask.Description);

            await _context.SaveChangesAsync();

            return task;

        }

 
        // CREATE
        public async Task<TaskItem> CreateTask(TaskItem task)
        {
            if (string.IsNullOrWhiteSpace(task.Title))
                throw new ArgumentException("Title cannot be empty");

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return task;
        }

        // Delete
        public async Task<TaskItem> DeleteTask(int taskId)
        {
            var foundTask = await _context.Tasks.FindAsync(taskId);

            if (foundTask == null)
                throw new ArgumentException("Task nicht gefunden.");

            _context.Tasks.Remove(foundTask);
            await _context.SaveChangesAsync();

            return foundTask;
        }


    }
}
