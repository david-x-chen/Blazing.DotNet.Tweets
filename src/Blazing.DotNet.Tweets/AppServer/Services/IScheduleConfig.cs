using System;

namespace Blazing.DotNet.Tweets.AppServer.Services
{
    public interface IScheduleConfig<T> where T: CronJobService
    {
        string CronExpression { get; set; }
        TimeZoneInfo TimeZoneInfo { get; set; }
    }
}