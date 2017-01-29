using MiddlewareExample.ApplicationBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareExample.Middlewares
{
    public class LoggerManager
    {
        private readonly InvokeDelegate _next;

        public LoggerManager(InvokeDelegate next)
        {
            _next = next;
        }

        public LoggerManager(InvokeDelegate next, string param1, string param2)
        {
            _next = next;
        }

        public async Task Invoke(InvokeContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Console.WriteLine("Begin LoggerManager.");

            await _next.Invoke(context);

            Console.WriteLine("End LoggerManager.");
        }
    }
}