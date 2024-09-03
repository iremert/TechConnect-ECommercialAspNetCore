// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace TechConnect.IdentityServer
{
    public static class Config
    {

        //korumak istediğin apileri temsil eder
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            //her mikroservis için o mikrosevise erişim için bir key belirlicez
           new ApiResource("Resource"){Scopes={ "AboutFullPermission", "ContactFullPermission" } },
           new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };



        //kullanıcının kimlik bilgileri
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };


        //apilere erişim için gerekli izinler
        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("AboutFullPermission","Full authority for about operations"),
            new ApiScope("ContactFullPermission","Full authority for contact operations"),
             new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
        };


        public static IEnumerable<Client> Clients => new Client[]
        {
            //VİSİTOR
            new Client
            {
                ClientId="TechConnectVisitorId",
                ClientName="TechConnect Visitor User",
                AllowedGrantTypes=GrantTypes.ClientCredentials,
                ClientSecrets={new Secret("techconnectsecret".Sha256())},
                AllowedScopes={"AboutFullPermission", "ContactFullPermission",
                IdentityServerConstants.LocalApi.ScopeName},
                AllowAccessTokensViaBrowser=true,

            },
            

            //USER
            new Client
            {
                ClientId="TechConnectUserId",
                ClientName="TechConnect  User",
                AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                ClientSecrets={new Secret("techconnectsecret".Sha256())},
                AllowedScopes={"AboutFullPermission", "ContactFullPermission",
                
                IdentityServerConstants.LocalApi.ScopeName, //*** 
                IdentityServerConstants.StandardScopes.Email,
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile
                }
            },


            //Admin
            new Client
            {
                ClientId="TechConnectAdminId",
                ClientName="TechConnect Admin User",
                AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                ClientSecrets={new Secret("techconnectsecret".Sha256())},
                 AllowedScopes={"AboutFullPermission", "ContactFullPermission",
                IdentityServerConstants.LocalApi.ScopeName, //*** 
                IdentityServerConstants.StandardScopes.Email,
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile
                },
                AccessTokenLifetime=600 //6 dk sonra token silinir.
            }
        };


    }
}