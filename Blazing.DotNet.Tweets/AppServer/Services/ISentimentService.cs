using Blazing.DotNet.Tweets.Shared;

namespace Blazing.DotNet.Tweets.AppServer.Services
{
    public interface ISentimentService
    {
        Prediction Predict(string text);
    }
}
