﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Umbraco.Core.CodeAnnotations;

namespace Umbraco.Core.BackOffice
{
    [UmbracoVolatile]
    public class BackOfficeUserValidator<T> : UserValidator<T>
        where T : BackOfficeIdentityUser
    {
        public override async Task<IdentityResult> ValidateAsync(UserManager<T> manager, T user)
        {
            // Don't validate if the user's email or username hasn't changed otherwise it's just wasting SQL queries.
            if (user.IsPropertyDirty("Email") || user.IsPropertyDirty("UserName"))
            {
                return await base.ValidateAsync(manager, user);
            }
            return IdentityResult.Success;
        }
    }
}
