using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmApp
{
    public interface IMyHostedService
    {
        //Empty!
    }

    public class JobTrackingService : IHostedService, IMyHostedService
    {
        ILogger<IHostedService> _logger;

        public JobTrackingService(ILogger<IHostedService> logger)
        {
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Job tracking service is starting");

            //while (true)
            //{
            await Task.Delay(500);

                //_logger.LogInformation("My hosted service task executed {counter}", counter);
            //}
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("My hosted service is stopping");
        }
    }
}
