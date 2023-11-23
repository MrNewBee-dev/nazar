using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Nazar1988.Areas.Identity.Data;

namespace Nazar1988.Class
{
    public class RoleFindMangeHandler : AuthorizationHandler<RoleFindManage>
    {
        private readonly Nazar1988Context _Wallet;
        private readonly UserManager<Nazar1988User> _user;
        public RoleFindMangeHandler(Nazar1988Context _Wallet, UserManager<Nazar1988User> _user)
        {
            
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleFindManage requirement)
        {

            var roles = ((ClaimsIdentity)context.User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);
            if (roles == null)
            {
                return Task.CompletedTask;
            }
            if (roles.Contains("توزیع کننده"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
