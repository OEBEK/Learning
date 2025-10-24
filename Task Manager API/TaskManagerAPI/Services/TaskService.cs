using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using TaskManagerAPI.Models;


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
            return await _context.Tasks.FindAsync(taskId);
        }

        //// GET ALL
        //public async Task<IEnumerable<TaskItem>> GetTaskItem()
        //{
        //    return await _context.Tasks.ToListAsync();
        //}



        // CREATE
        public async Task<TaskItem> CreateTask(TaskItem task)
        {
            if (string.IsNullOrWhiteSpace(task.Title))
                throw new ArgumentException("Title cannot be empty");

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return task;
        }


    }
}
