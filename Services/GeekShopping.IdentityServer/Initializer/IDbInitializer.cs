using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekShopping.IdentityServer.Initializer
{
    public interface IDbInitializer
    {
        public void Initialize();
    }
}