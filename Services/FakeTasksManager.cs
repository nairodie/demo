using TasksTracker.TaskManager.Backend.Api.Models;

namespace TasksTracker.TaskManager.Backend.Api.Services
{
    public class FakeTasksManager : ITaskManager
    {
        private List<TaskModel> _tasksList = new List<TaskModel>();
        Random rnd = new Random();

        private void GenerateRandomTasks()
        {
            for (int i = 0; i < 10; i++)
            {
                var task = new TaskModel()
                {
                    TaskId = Guid.NewGuid(),
                    TaskName = $"Task number: {i}",
                    TaskCreatedBy = "nairodie@protonmail.com",
                    TaskCreatedOn = DateTime.UtcNow.AddMinutes(i),
                    TaskDueDate = DateTime.UtcNow.AddDays(i),
                    TaskAssignedTo = $"assignee{rnd.Next(50)}@mail.com",
                };
                _tasksList.Add(task);
            }
        }


        public Task<Guid> CreateNewTask(string taskName, string createdBy, string assignedTo, DateTime dueDate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTask(Guid taskId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TaskModel>> GetTaskByCreator(string createdBy)
        {
            throw new NotImplementedException();
        }

        public Task<TaskModel?> GetTaskById(Guid taskId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MarkTaskCompleted(Guid taskId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTask(Guid taskId, string taskName, string assignedTo, DateTime dueDate)
        {
            throw new NotImplementedException();
        }
    }

}