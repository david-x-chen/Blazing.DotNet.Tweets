
using System;
using System.Threading;
using System.Threading.Tasks;
using Blazing.DotNet.Tweets.AppServer.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Blazing.DotNet.Tweets.AppServer.Services
{
    public class TimeService : CronJobService
    {
        private readonly ILogger<TimeService> _logger;

        private readonly IHubContext<StreamHub, IBTwitterClient> _streamHub;
        
        public TimeService(IScheduleConfig<TimeService> config, ILogger<TimeService> logger, IHubContext<StreamHub, IBTwitterClient> streamHub)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _streamHub  = streamHub;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Time service starts.");
 
            return base.StartAsync(cancellationToken);
        }

        public override Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} Time service is working.");
            
            _logger.LogInformation("Worker running at: {Time}", DateTime.Now);
            _streamHub.Clients.All.TweetReceived(null);
            Task.Delay(1000);

            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Time service is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}