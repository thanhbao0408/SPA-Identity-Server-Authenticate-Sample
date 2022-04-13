using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static IdentityModel.OidcConstants;

namespace IdentityServerAspNetIdentity;

public class UserValidator : IResourceOwnerPasswordValidator
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    public UserValidator(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var result = await _signInManager.PasswordSignInAsync(context.UserName, context.Password, isPersistent: true, lockoutOnFailure: true);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(context.UserName);
            if (user != null)
            {
                var claims = await _userManager.GetClaimsAsync(user);
                // context set to success
                context.Result = new GrantValidationResult(
                    subject: user.Id.ToString(),
                    authenticationMethod: AuthenticationMethods.Password,
                    claims: claims
                );
                return;
            }
        }

        // context set to Failure        
        context.Result = new GrantValidationResult(
                TokenRequestErrors.UnauthorizedClient, "Invalid Crdentials");

    }
}