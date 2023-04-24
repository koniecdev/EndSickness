// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer;
public static class Config
{
	public const string APIURL = "https://localhost:44329";
	public const string FRONTENDURL = "https://localhost:7284";

    public static IEnumerable<IdentityResource> IdentityResources =>
	new List<IdentityResource>
	{
		new IdentityResources.OpenId(),
		new IdentityResources.Profile(),
		new IdentityResource(name: "user", userClaims: new[] { JwtClaimTypes.Id, JwtClaimTypes.Email, JwtClaimTypes.Name})
	};

	public static IEnumerable<ApiScope> ApiScopes =>
		new List<ApiScope>
		{
			new ApiScope("scope1"),
			new ApiScope("scope2"),
			new ApiScope("api1")
		};

	public static IEnumerable<Client> Clients =>
		new Client[]
		{
		   new Client
		   {
				ClientId = "client",
				ClientName = "Client for Postman user",
				AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
				ClientSecrets = { new Secret("secret".Sha256()) },
				AllowedScopes = { "api1", "user"},
				AlwaysSendClientClaims = true,
				AlwaysIncludeUserClaimsInIdToken = true,
				AllowAccessTokensViaBrowser = true
		   },
		   new Client
		   {
				ClientId = "swagger",
				ClientName = "Client for API Swagger user",
				AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
				ClientSecrets = {new Secret("secret".Sha256())},
				AllowedScopes = {"api1", "user", "openid"},
				AlwaysSendClientClaims = true,
				AlwaysIncludeUserClaimsInIdToken = true,
				AllowAccessTokensViaBrowser=true,
				RedirectUris = { $"{APIURL}/swagger/oauth2-redirect.html" },
				AllowedCorsOrigins = { APIURL }
		   },
			new Client
			{
				ClientId = "mvc",
				ClientName = "MVC Client app",
				AllowedGrantTypes = GrantTypes.Code,
				ClientSecrets = { new Secret("secret".Sha256()) },
				PostLogoutRedirectUris = { $"{FRONTENDURL}/signout-callback-oidc" },
                AllowedCorsOrigins = { FRONTENDURL },
				AllowOfflineAccess = true,
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "api1",
                },
                RedirectUris = { $"{FRONTENDURL}/signin-oidc" },
            }
        };
}