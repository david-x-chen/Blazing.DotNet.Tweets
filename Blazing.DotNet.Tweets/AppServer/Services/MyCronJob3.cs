
using System;
using System.Threading;
using System.Threading.Tasks;
using Blazing.DotNet.Tweets.AppServer.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Blazing.DotNet.Tweets.AppServer.Services
{
    public class MyCronJob3 : CronJobService
    {
        private readonly ILogger<MyCronJob3> _logger;

        private readonly IHubContext<StreamHub, ITwitterClient> _streamHub;
        
        public MyCronJob3(IScheduleConfig<MyCronJob3> config, ILogger<MyCronJob3> logger, IHubContext<StreamHub, ITwitterClient> streamHub)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _streamHub  = streamHub;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CronJob 3 starts.");
 
            return base.StartAsync(cancellationToken);
        }

        public override Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} CronJob 3 is working.");
            
            _logger.LogInformation("Worker running at: {Time}", DateTime.Now);
            _streamHub.Clients.All.TweetReceived(null);
            Task.Delay(1000);

            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CronJob 3 is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}