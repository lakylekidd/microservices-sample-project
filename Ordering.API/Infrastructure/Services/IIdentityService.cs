using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Infrastructure.Services
{
    /// <summary>
    /// The identity service contract
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// Returns the user identity
        /// </summary>
        /// <returns></returns>
        string GetUserIdentity();

        /// <summary>
        /// Returns the user name
        /// </summary>
        /// <returns></returns>
        string GetUserName();
    }
}
