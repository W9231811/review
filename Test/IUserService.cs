using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test
{
    public interface IUserService
    {
        User GetUser(string UserName);
    }
}
