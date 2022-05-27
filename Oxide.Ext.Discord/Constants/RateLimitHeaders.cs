namespace Oxide.Ext.Discord.Constants
{
    public static class RateLimitHeaders
    {
        public const string RetryAfter = "Retry-After";
        public const string IsGlobal = "X-RateLimit-Global";
        public const string BucketId = "X-RateLimit-Bucket";
        public const string BucketLimit = "X-RateLimit-Limit";
        public const string BucketRemaining = "X-RateLimit-Remaining";
        public const string BucketResetAfter = "X-RateLimit-Reset-After";
        public const string BucketReset = "X-RateLimit-Reset";
    }
}