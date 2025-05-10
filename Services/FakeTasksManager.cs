using TasksTracker.TaskManager.Backend.Api.Models;

namespace TasksTracker.TaskManager.Backend.Api.Services
{
    public class FakeTasksManager : ITaskManager
    {
        private List<TaskModel> _tasksList = new List<TaskModel>();
        readonly Random rnd = new();

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

        public FakeTasksManager()
        {
            GenerateRandomTasks();
        }


        public Task<Guid> CreateNewTask(string taskName, string createdBy, string assignedTo, DateTime dueDate)
        {
            var task = new TaskModel()
            {
                TaskId = Guid.NewGuid(),
                TaskName = taskName,
                TaskCreatedBy = createdBy,
                TaskCreatedOn = DateTime.UtcNow,
                TaskDueDate = dueDate,
                TaskAssignedTo = assignedTo
            };
            _tasksList.Add(task);
            return Task.FromResult(task.TaskId);
        }

        public Task<bool> DeleteTask(Guid taskId)
        {
            var task = _tasksList.FirstOrDefault(t => t.TaskId.Equals(taskId));

            if (task != null)
            {
                _tasksList.Remove(task);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<List<TaskModel>> GetTaskByCreator(string createdBy)
        {
            var taskList = _tasksList.Where(t => t.TaskCreatedBy.Equals(createdBy)).OrderByDescending(x => x.TaskCreatedOn).ToList();
            return Task.FromResult(taskList);
        }

        public Task<TaskModel?> GetTaskById(Guid taskId)
        {
            var taskModel = _tasksList.FirstOrDefault(t => t.TaskId.Equals(taskId));
            return Task.FromResult(taskModel);
        }

        public Task<bool> MarkTaskCompleted(Guid taskId)
        {
             var task = _tasksList.FirstOrDefault(t => t.TaskId.Equals(taskId));
 
            if (task != null)
            {
                task.IsCompleted = true;
                 return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> UpdateTask(Guid taskId, string taskName, string assignedTo, DateTime dueDate)
        {
            var task = _tasksList.FirstOrDefault(t => t.TaskId.Equals(taskId));

            if (task != null)
            {
                task.TaskName = taskName;
                task.TaskAssignedTo = assignedTo;
                task.TaskDueDate = dueDate;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }

}