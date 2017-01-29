using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MiddlewareExample.Services
{
    public class PersonService : BaseService, IPersonService
    {
        public virtual string Method1(int x)
        {
            return $"input is {x}";
        }
    }
}