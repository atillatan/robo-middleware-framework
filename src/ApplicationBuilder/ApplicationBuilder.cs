using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareExample.ApplicationBuilder
{
    public class ApplicationBuilder : IApplicationBuilder
    {
        public readonly IList<Func<InvokeDelegate, InvokeDelegate>> _middlewares = new List<Func<InvokeDelegate, InvokeDelegate>>();

        public IDictionary<string, object> Properties { get; }

        private ApplicationBuilder(ApplicationBuilder builder)
        {
            Properties = builder.Properties;
        }

        public ApplicationBuilder()
        {
            Properties = new Dictionary<string, object>();
        }

        public IApplicationBuilder New()
        {
            return new ApplicationBuilder(this);
        }

        public InvokeDelegate Build()
        {
            InvokeDelegate next = context => Task.Run(() => { });

            foreach (var current in _middlewares.Reverse())
            {
                next = current(next);
            }

            return next;
        }

        public virtual IApplicationBuilder Use(Func<InvokeDelegate, InvokeDelegate> middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }

        private T GetProperty<T>(string key)
        {
            object value;
            return Properties.TryGetValue(key, out value) ? (T)value : default(T);
        }

        private void SetProperty<T>(string key, T value)
        {
            Properties[key] = value;
        }
    }
}