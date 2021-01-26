using Blazing.DotNet.Tweets.Shared;
using System.Threading.Tasks;

namespace Blazing.DotNet.Tweets.AppServer.Services
{
    public interface IBTwitterClient
    {
        Task TweetReceived(TweetResult tweet);

        Task StatusUpdated(Status status);
    }
}
