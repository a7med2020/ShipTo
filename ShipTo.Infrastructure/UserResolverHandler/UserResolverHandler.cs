using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Infrastructure.UserResolverHandler
{
    public interface IUserResolverHandler
    {
        string GetUserId();
        string GetUserName();
        //string GetUserEmail();
    }

    public class UserResolverHandler : IUserResolverHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserResolverHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserName()
        {
            return _httpContextAccessor.HttpContext.User?.Identity?.Name;
        }

        public string GetUserId()
        {
            return _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
