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
                    ClientSecrets = {new Secret("secret".Sha256())}, // change me!

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = {"https://localhost:7101/swagger/oauth2-redirect.html"},
                    AllowedCorsOrigins = {"https://localhost:7101"},
                    AllowedScopes = new List<string>
                    {
                        "SampleAPI"
                    }
                },
        };
}
