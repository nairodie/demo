using TasksTracker.TaskManager.Backend.Api.Models;

namespace TasksTracker.TaskManager.Backend.Api.Services
{
    public interface ITaskManager
    {
        Task<List<TaskModel>> GetTaskByCreator(string createdBy);
        Task<TaskModel?> GetTaskById(Guid taskId);
        Task<Guid> CreateNewTask(string taskName, string createdBy, string assignedTo, DateTime dueDate);
        Task<bool> UpdateTask(Guid taskId, string taskName, string assignedTo, DateTime dueDate);
        Task<bool> MarkTaskCompleted(Guid taskId);
        Task<bool> DeleteTask(Guid taskId);
    }
}