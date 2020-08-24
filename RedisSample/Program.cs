using System;
using StackExchange.Redis;
namespace RedisSample
{
    class Program
    {
        private static IDatabase database;
        public Program()
        {

        }

        //public ConnectionMultiplexer ConnectRedis
        //{
        //    get
        //    {
        //        return 
        //    }
        //}

        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");
                ConnectionMultiplexer ConnectRedis = ConnectionMultiplexer.Connect("192.168.0.94");
                database = ConnectRedis.GetDatabase();
                var retValue = database.StringGet("first");
                Console.WriteLine("Redis Retvalue: " + retValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
