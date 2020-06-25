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
            where THub : Hub<ITwitterClient>
        {
            services.AddSignalR(options => options.KeepAliveInterval = TimeSpan.FromSeconds(5));

            Console.WriteLine($"config{configuration["witter:Tracks"]}");

            Auth.SetUserCredentials(
                configuration["Authentication:Twitter:ConsumerKey"],
                configuration["Authentication:Twitter:ConsumerSecret"],
                configuration["Authentication:Twitter:AccessToken"],
                configuration["Authentication:Twitter:AccessTokenSecret"]);

            return services.AddSingleton<ISentimentService, SentimentService>()
                           .AddSingleton<ITwitterService<THub>, TwitterService<THub>>()
                           .AddHostedService<TwitterService<THub>>()
                           .AddSingleton<IFilteredStream>(_ =>
                           {
                               var stream = Stream.CreateFilteredStream();
                               stream.StallWarnings = true;

                               return stream;
                           });
        }
    }
}
