using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazing.DotNet.Tweets.AppServer.Services
{
    public interface IBTwitterService<T> where T : Hub
    {
        void RemoveTrack(string track);

        void AddTracks(ISet<string> tracks);

        Task StartTweetStreamAsync();

        void PauseTweetStream();

        void StopTweetStream();
    }
}
