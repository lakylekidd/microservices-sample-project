using System;
using Microsoft.AspNetCore.Http;

namespace Ordering.API.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        // The http context accessor
        private readonly IHttpContextAccessor _context;

        // The constructor
        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Gets the user id from the context
        public string GetUserIdentity()
        {
            return _context.HttpContext.User.FindFirst("sub").Value;
        }

        // Gets the username from the context
        public string GetUserName()
        {
            return _context.HttpContext.User.Identity.Name;
        }
    }
}
