using TaskManagerAPI.Models;
using TaskManagerAPI.Repositories;

namespace TaskManagerAPI.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasks()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TaskItem?> GetTaskItem(int taskId)
        {
            return await _repository.GetByIdAsync(taskId);
        }

        public async Task<TaskItem> CreateTask(TaskItem task)
        {
            if (string.IsNullOrWhiteSpace(task.Title))
                throw new ArgumentException("Title cannot be empty");

            await _repository.AddAsync(task);
            await _repository.SaveAsync();
            return task;
        }

        public async Task<TaskItem?> UpdateTask(int taskId, TaskItem updatedTask)
        {
            var task = await _repository.GetByIdAsync(taskId);
            if (task == null)
                throw new ArgumentException("Task not found");

            task.Title = string.IsNullOrWhiteSpace(updatedTask.Title)
                ? task.Title : updatedTask.Title;

            task.Description = string.IsNullOrWhiteSpace(updatedTask.Description)
                ? task.Description : updatedTask.Description;

            _repository.Update(task);
            await _repository.SaveAsync();
            return task;
        }

        public async Task<TaskItem?> SetDone(int taskId)
        {
            var task = await _repository.GetByIdAsync(taskId);
            if (task == null)
                throw new ArgumentException("Task not found");

            task.IsCompleted = true;
            _repository.Update(task);
            await _repository.SaveAsync();
            return task;
        }

        public async Task<TaskItem> DeleteTask(int taskId)
        {
            var task = await _repository.GetByIdAsync(taskId);
            if (task == null)
                throw new ArgumentException("Task not found");

            _repository.Delete(task);
            await _repository.SaveAsync();
            return task;
        }
    }
}
