using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileSharingWebsite.Models;
using Microsoft.AspNetCore.Authorization;

namespace FileSharingWebsite.Authorization
{
    public class CanDownloadRequirement : IAuthorizationRequirement { }
    public class UploadedFileSharingAuthorizationHandler : AuthorizationHandler<CanDownloadRequirement, UploadedFile>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CanDownloadRequirement requirement,
            UploadedFile resource)
        {
            if (resource.Published || context.User.Identity?.Name == resource.Author)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
