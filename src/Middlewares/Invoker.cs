using MiddlewareExample.ApplicationBuilder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MiddlewareExample.Middlewares
{
    public class Invoker
    {
        private readonly InvokeDelegate _next;

        public Invoker(InvokeDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(InvokeContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Expression<Func<object>> _function = context.Request.Function;
            object _instance = context.Request.Instance;

            var timer = Stopwatch.StartNew();
            if (_instance == null) throw new Exception("Service is null!");
            var fbody = _function.Body as MethodCallExpression;
            if (fbody == null || fbody.Method == null) throw new Exception("Expression must be a method call.");
            string methodName = fbody.Method.Name;
            dynamic result = null;

            List<object> args = new List<object>();

            foreach (var argument in fbody.Arguments)
            {
                var constant = argument as ConstantExpression;
                if (constant != null)
                {
                    args.Add(constant.Value);
                }
            }

            try
            {
                result = fbody.Method.Invoke(_instance, args.ToArray<object>());
                Type t = context.Result.ResultType;
                context.Result.Value = result;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                timer.Stop();
                Console.WriteLine($"{methodName}() method invoked in :{Convert.ToDouble(timer.ElapsedMilliseconds)}ms ");
            }

            await _next.Invoke(context);
        }
    }
}