using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.WebUI.Infrastructure.Abstract
{
    /// <summary>
    /// This was created to decouple controllers from static auth providers, making it testable in unittests
    /// </summary>
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);
    }
}
