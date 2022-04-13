using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServerAspNetIdentity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("SampleAPI"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // Swagger client
            new Client
            {
                ClientId = "api_swagger",
                ClientName = "Swagger UI for Sample API",
                ClientSecrets = {new Secret("secret".Sha256())},

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = {"https://localhost:7101/swagger/oauth2-redirect.html"},
                AllowedCorsOrigins = {"https://localhost:7101"},
                AllowedScopes = new List<string>
                {
                    "SampleAPI"
                }
            },
            // NextJs client
            new Client
            {
                ClientId = "nextjs_web_app",
                ClientName = "NextJs Web App",
                ClientSecrets = {new Secret("secret".Sha256())}, // change me!
                RequireClientSecret = false,
                AllowedGrantTypes =  new[] { GrantType.AuthorizationCode, GrantType.ResourceOwnerPassword },
                    
                AllowOfflineAccess = true,

                //// where to redirect to after login
                RedirectUris = { "http://localhost:3000/api/auth/callback/sample-identity-server" },
                //// where to redirect to after logout
                PostLogoutRedirectUris = { "http://localhost:3000" },
                AllowedCorsOrigins= { "http://localhost:3000" },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "SampleAPI"
                },
            }
        };
}
