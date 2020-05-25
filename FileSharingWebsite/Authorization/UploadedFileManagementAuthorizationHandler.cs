using System.Threading.Tasks;
using FileSharingWebsite.Models;
using Microsoft.AspNetCore.Authorization;

namespace FileSharingWebsite.Authorization
{
    public class SameAuthorRequirement : IAuthorizationRequirement { }
    public class UploadedFileManagementAuthorizationHandler : AuthorizationHandler<SameAuthorRequirement, UploadedFile>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameAuthorRequirement requirement,
            UploadedFile resource)
        {
            if (context.User.Identity?.Name == resource.Author)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
