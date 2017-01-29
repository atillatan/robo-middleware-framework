using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareExample.ApplicationBuilder
{
    public interface IApplicationBuilder
    {
        InvokeDelegate Build();

        IApplicationBuilder Use(Func<InvokeDelegate, InvokeDelegate> middleware);

        IDictionary<string, object> Properties { get; }

        IApplicationBuilder New();
    }
}