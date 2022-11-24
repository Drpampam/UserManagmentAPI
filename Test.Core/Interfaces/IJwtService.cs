using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data;

namespace Test.Core.Interfaces
{
    public interface IJwtService
    {
        Tokens Authenticate(AppUser user);
    }
}
