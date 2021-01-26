﻿using System;

namespace Blazing.DotNet.Tweets.Shared
{
    public class TweetResult
    {
        public bool IsOffTopic { get; set; }
        public string AuthorName { get; set; }
        public string AuthorURL { get; set; }
        public string HTML { get; set; }
        public string URL { get; set; }
        public string ProviderURL { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Version { get; set; }
        public string Type { get; set; }
        public string CacheAge { get; set; }
        public DateTimeOffset CreateAt { get; set; }
    }
}