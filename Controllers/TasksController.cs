using Microsoft.AspNetCore.Mvc;
using TasksTracker.TaskManager.Backend.Api.Models;
using TasksTracker.TaskManager.Backend.Api.Services;

namespace TasksTracker.TaskManager.Backend.Api.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ITaskManager _taskManager;

        public TasksController(ILogger<TasksController> logger, ITaskManager taskManager)
        {
            _logger = logger;
            _taskManager = taskManager;
        }

        [HttpGet]
        public async Task<IEnumerable<TaskModel>> Get(string createdBy)
        {
            return await _taskManager.GetTaskByCreator(createdBy);
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetTask(Guid taskId)
        {
            var task = await _taskManager.GetTaskById(taskId);
            if (task != null)
            {
                return Ok(task);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TaskAddModel taskAddModel)
        {
            var taskId = await _taskManager.CreateNewTask(taskAddModel.TaskName, taskAddModel.TaskCreatedBy, taskAddModel.TaskAssignedTo, taskAddModel.TaskDueDate);
            return Created($"/api/tasks/{taskId}", null);
        }

        [HttpPut("{taskId}")]
        public async Task<IActionResult> Put(Guid taskId, [FromBody]TaskUpdateModel taskUpdateModel)
        {
            var updated = await _taskManager.UpdateTask(taskId, taskUpdateModel.TaskName, taskUpdateModel.TaskAssignedTo, taskUpdateModel.TaskDueDate);

            if (updated)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("{taskId}/markcomplete")]
        public async Task<IActionResult> MarkComplete(Guid taskId)
        {
             var updated = await _taskManager.MarkTaskCompleted(taskId);

            if (updated)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid taskId)
        {
            var deleted = await _taskManager.DeleteTask(taskId);
            if (deleted)
            {
                return Ok();
            }
            return NotFound();
        } 
    }
}