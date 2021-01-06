
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{
    public class User
    {
        public string name { get; set; }
        public int index { get; set; }
    }


    public class BenchmarkRequest
    {
        public List<User> users { get; set; }
    }

    public class BenchmarkResponse
    {
        public int myInt {get; set;}
        public List<User> users { get; set; }
    }

    public class ThreadSafeRandom
    {
        private static readonly Random _global = new Random();
        [ThreadStatic] private static Random _local;


        public int Next(int maxValue)
        {
            if (_local == null)
            {
                lock (_global)
                {
                    if (_local == null)
                    {
                        int seed = _global.Next();
                        _local = new Random(seed);
                    }
                }
            }

            return _local.Next(maxValue);
        }
    }
}
