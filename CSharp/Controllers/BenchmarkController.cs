using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;

namespace CSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BenchmarkController : ControllerBase
    {
        static ThreadSafeRandom rng = new ThreadSafeRandom();
        private readonly ILogger<BenchmarkController> _logger;

        public BenchmarkController(ILogger<BenchmarkController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public BenchmarkResponse Post(BenchmarkRequest request)
        {
            var resultMap = new HashSet<int>();
            var count = 0;
            for (int i = 0; i < 100000; i++)
            {
                var firstIndex = rng.Next(request.users.Count);
                var secondIndex = rng.Next(request.users.Count);
                var first = request.users[firstIndex];
                var second = request.users[secondIndex];
                if (first.name.CompareTo(second.name) > 0)
                {
                    count += 1;
                    resultMap.Add(firstIndex);
                }
                else
                {
                    count -= 1;
                    resultMap.Remove(firstIndex);
                }
            }

            var result = new BenchmarkResponse();
            result.myInt = count;
            result.users = resultMap.Select(id => request.users[id]).ToList();
            return result;
        }
    }
}
