using System;

namespace Blazing.DotNet.Tweets.AppServer.Services
{
    public class ScheduleConfig<T> : IScheduleConfig<T> where T: CronJobService
    {
        public string CronExpression { get; set; }
        public TimeZoneInfo TimeZoneInfo { get; set; }
    }
}