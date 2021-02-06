using System;
using System.Runtime.Serialization;

namespace Contest.Wallet.Common.Cache.Distributed.RedisCache
{
    public class RedisCacheException : Exception
    {
        public RedisCacheException()
        {
        }

        public RedisCacheException(string message) : base(message)
        {
        }

        public RedisCacheException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RedisCacheException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
