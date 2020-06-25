using Blazing.DotNet.Tweets.Shared;
using Microsoft.ML;
using System.IO;

namespace Blazing.DotNet.Tweets.AppServer.Services
{
    public class SentimentService : ISentimentService
    {
        readonly MLContext _context = new MLContext();
        readonly PredictionEngine<Sentiment, Prediction> _predictionEngine;

        public SentimentService()
        {
            var predictionPipeline = _context.Model.Load(@"./Data/SentimentModel.zip", out _);
            _predictionEngine = _context.Model.CreatePredictionEngine<Sentiment, Prediction>(predictionPipeline);
        }

        public Prediction Predict(string text)
            => _predictionEngine?.Predict(new Sentiment { SentimentText = text });
    }
}
