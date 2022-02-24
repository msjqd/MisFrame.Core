using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisFrame.Core.WebApi.Cache
{
    public interface ICaching
    {
        void Set(String cacheKey, Object cacheValue);

        Object Get(String cacheKey);
    }
}
