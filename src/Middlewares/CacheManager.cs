using MiddlewareExample.ApplicationBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareExample.Middlewares
{
    public class CacheManager
    {
        private readonly InvokeDelegate _next;

        public CacheManager(InvokeDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(InvokeContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Console.WriteLine("Begin CacheManager.");

            await _next.Invoke(context);

            Console.WriteLine("End CacheManager.");
        }
    }
}