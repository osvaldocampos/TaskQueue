using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TaskQueue.Services
{
    public class QueueHostedService : BackgroundService
    {
        public IBackgroundTaskQueue TaskQueue { get; }
        public QueueHostedService(IBackgroundTaskQueue taskQueue) 
        {
            TaskQueue = taskQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<Task> taskList = new List<Task>();
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem =
                    await TaskQueue.DequeueAsync(stoppingToken);

                try
                {
                    await workItem(stoppingToken);
                }
                catch
                {
                    Debug.WriteLine($"Error occurred executing {nameof(workItem)}.");
                }
            }
        }
    }
}
