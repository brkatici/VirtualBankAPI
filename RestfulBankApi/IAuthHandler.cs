using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
namespace RestfulBankApi
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public string Role { get; }

        public RoleRequirement(string role)
        {
            Role = role;
        }
    }

    public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == requirement.Role))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

}
