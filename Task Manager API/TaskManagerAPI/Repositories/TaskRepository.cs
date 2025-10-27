using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using TaskManagerAPI.Models;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;
    public TaskRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<TaskItem>> GetAllAsync() => await _context.Tasks.ToListAsync();
    public async Task<TaskItem?> GetByIdAsync(int id) => await _context.Tasks.FindAsync(id);
    public async Task AddAsync(TaskItem task) => await _context.Tasks.AddAsync(task);
    public async Task DeleteAsync(TaskItem task) => _context.Tasks.Remove(task);
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}
