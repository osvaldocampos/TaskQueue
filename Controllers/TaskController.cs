using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskQueue.Dtos;
using TaskQueue.Services;

namespace TaskQueue.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly IBackgroundTaskQueue _backgroundTaskQueue;
        private readonly IEngineService _engineService;

        private readonly ILogger<TaskController> _logger;

        public TaskController(IBackgroundTaskQueue backgroundTaskQueue, IEngineService engineService, ILogger<TaskController> logger)
        {
            _logger = logger;
            _backgroundTaskQueue = backgroundTaskQueue;
            _engineService = engineService;
        }

        [HttpPost("queue")]
        public ActionResult CreateNewTask([FromBody] TaskDto task)
        {
            _backgroundTaskQueue.QueueBackgroundWorkItem(x => _engineService.Process(task.TaskId));

            return Ok();
        }
    }
}
