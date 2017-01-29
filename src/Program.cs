using MiddlewareExample.ApplicationBuilder;
using MiddlewareExample.Middlewares;
using MiddlewareExample.Services;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace MiddlewareExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IApplicationBuilder app = Application.Current.appBuilder;

            app.UseMiddleware<LoggerManager>("param1", "param2");

            app.UseMiddleware<CacheManager>();

            app.Use(next =>
            {
                return async invokeContext =>
                {
                    Console.WriteLine("Begin Inline middleware");

                    await next(invokeContext);

                    Console.WriteLine("End Inline middleware");
                };
            });

            app.UseMiddleware<Invoker>();

            Application.Current.Build();

            //// Example Method Call ////

            PersonService personService = new PersonService();

            var result = personService.Invoke<string>(() => personService.Method1(3));

            Console.ReadKey();
        }
    }
}