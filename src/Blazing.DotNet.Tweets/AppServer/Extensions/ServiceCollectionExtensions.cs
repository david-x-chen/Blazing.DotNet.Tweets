using Blazing.DotNet.Tweets.AppServer.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Tweetinvi;
using Tweetinvi.Streaming;

namespace Blazing.DotNet.Tweets.AppServer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorTwitterServices<THub>(this IServiceCollection services, IConfiguration configuration)
            where THub : Hub<IBTwitterClient>
        {
            services.AddSignalR(options => options.KeepAliveInterval = TimeSpan.FromSeconds(5));

            Console.WriteLine($"config{configuration["Twitter:Tracks"]}");

            var userClient = new TwitterClient(
                configuration["Authentication:Twitter:ConsumerKey"],
                configuration["Authentication:Twitter:ConsumerSecret"],
                configuration["Authentication:Twitter:AccessToken"],
                configuration["Authentication:Twitter:AccessTokenSecret"]);

            return services.AddSingleton<ISentimentService, SentimentService>()
                           .AddSingleton<IBTwitterService<THub>, TwitterService<THub>>()
                           .AddHostedService<TwitterService<THub>>()
                           .AddSingleton<IFilteredStream>(_ =>
                           {
                               var stream = userClient.Streams.CreateFilteredStream();
                               stream.StallWarnings = true;

                               return stream;
                           });
        }

        public static IServiceCollection AddCronJob<T>(this IServiceCollection services, Action<IScheduleConfig<T>> options) where T : CronJobService
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), @"Please provide Schedule Configurations.");
            }
            var config = new ScheduleConfig<T>();
            options.Invoke(config);
            if (string.IsNullOrWhiteSpace(config.CronExpression))
            {
                throw new ArgumentNullException(nameof(ScheduleConfig<T>.CronExpression), @"Empty Cron Expression is not allowed.");
            }

            services.AddSingleton<IScheduleConfig<T>>(config);
            services.AddHostedService<T>();
            return services;
        }
    }
}
