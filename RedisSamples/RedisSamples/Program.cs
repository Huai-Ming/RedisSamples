using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedisSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            string host = "localhost";
            string elementKey = "testKeyRedis";

            using (RedisClient redisClient = new RedisClient(host, 6379, "foobared"))
            {
                if (redisClient.Get<string>(elementKey) == null)
                {
                    // adding delay to see the difference
                    Thread.Sleep(5000);
                    // save value in cache
                    redisClient.Set(elementKey, "some cached value");
                }
                // get value from the cache by key
                Console.WriteLine("Item value is: " + redisClient.Get<string>(elementKey));
            }
            Console.Read();
        }
    }
}
