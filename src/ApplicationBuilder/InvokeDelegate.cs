using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareExample.ApplicationBuilder
{
    public delegate Task InvokeDelegate(InvokeContext context);
}