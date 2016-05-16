using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisWithStackExchange
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect( "localhost,ssl=false,password=foobared");
            //var options = new ConfigurationOptions();
            //options.EndPoints.Add("localhost");
            //options.Ssl = true;
            //options.Password = "foobared";
            //var connection = ConnectionMultiplexer.Connect(options);

            IDatabase cache = connection.GetDatabase();
 
            // Perform cache operations using the cache object...
            // Simple put of integral data types into the cache
            cache.StringSet("key1", "dsyavdadsahda");
            cache.StringSet("key2", 25);
 
            Foo foo1 = new Foo() {Age = 1, Name = "Foo1"};
            var serializedFoo = JsonConvert.SerializeObject(foo1);
            cache.StringSet("serializedFoo", serializedFoo);
 
            Foo foo3 = new Foo() { Age = 1, Name = "Foo3" };
            var serializedFoo3 = JsonConvert.SerializeObject(foo1);
            cache.StringSet("serializedFoo3", serializedFoo3); 
 
            // Simple get of data types from the cache
            string key1 = cache.StringGet("key1");
            int key2 = (int)cache.StringGet("key2"); 
 
            var foo2 = JsonConvert.DeserializeObject<Foo>(cache.StringGet("serializedFoo"));
            bool areEqual = foo1 == foo2; 
 
            var foo4 = JsonConvert.DeserializeObject<Foo>(cache.StringGet("serializedFoo3"));
            bool areEqual2 = foo3 == foo4; 
 
            Console.ReadLine();
        }
    }

     public class Foo : IEquatable<Foo>
    {
        public string Name { get; set; }
        public int Age { get; set; }
 
        public bool Equals(Foo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && Age == other.Age;
        }
 
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Foo) obj);
        }
 
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0)*397) ^ Age;
            }
        }
 
        public static bool operator ==(Foo left, Foo right)
        {
            return Equals(left, right);
        }
 
        public static bool operator !=(Foo left, Foo right)
        {
            return !Equals(left, right);
        }
    }
}
