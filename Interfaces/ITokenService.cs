using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app_dotnet.Models;

namespace app_dotnet.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}