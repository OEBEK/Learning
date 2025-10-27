using TaskManagerAPI.Models;

public interface ITaskRepository
{
    Task<IEnumerable<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(int id);
    Task AddAsync(TaskItem task);
    Task DeleteAsync(TaskItem task);
    Task SaveChangesAsync();
}
