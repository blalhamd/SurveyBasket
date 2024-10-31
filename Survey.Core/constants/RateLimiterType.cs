namespace Survey.Core.constants
{
    public static class RateLimiterType
    {
        public const string Concurrency = "concurrency";
        public const string TokenBucket = "tokenBucket";
        public const string FixedWindow = "fixedWindow";
        public const string SlidingWindow = "slidingWindow";
        public const string IpLimiting = "ipLimiting";
        public const string UserLimiting = "userLimiting";
    }
}
