using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TaskQueue.Services
{
    public class EngineService : IEngineService
    {
        public EngineService() { }

        public async Task Process(int taskId)
        {
            Debug.WriteLine($"Starting task id: { taskId }");
            await Task.Delay(5000);
            Debug.WriteLine($"Completed task id: { taskId }");
        }
    }
}
