// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace TechConnect.IdentityServer
{
    public static class Config
    {

        ////korumak istediğin apileri temsil eder
        //public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        //{
        //    //her mikroservis için o mikrosevise erişim için bir key belirlicez
        //   new ApiResource("Resource"){Scopes={ "AboutFullPermission", "TestimonialFullPermission", "İyzicoPaymentFullPermission", "TagFullPermission", "OrderingFullPermission", "ProductFullPermission", "DiscountFullPermission", "AddressFullPermission", "BasketTotalFullPermission", "CategoryFullPermission", "ColorFullPermission", "CompareFullPermission", "ContactFullPermission", "Contact2FullPermission", "FavouriteFullPermission" } },
        //   new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        //};



        ////kullanıcının kimlik bilgileri
        //public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        //{
        //    new IdentityResources.OpenId(),
        //    new IdentityResources.Email(),
        //    new IdentityResources.Profile()
        //};


        ////apilere erişim için gerekli izinler
        //public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        //{
        //    new ApiScope("AboutFullPermission","Full authority for about operations"),
        //    new ApiScope("AddressFullPermission","Full authority for address operations"),
        //    new ApiScope("BasketTotalFullPermission","Full authority for baskettotal operations"),
        //    new ApiScope("CategoryFullPermission","Full authority for category operations"),
        //    new ApiScope("ColorFullPermission","Full authority for color operations"),
        //    new ApiScope("CompareFullPermission","Full authority for compare operations"),
        //    new ApiScope("ContactFullPermission","Full authority for contact operations"),
        //    new ApiScope("Contact2FullPermission","Full authority for contact2 operations"),
        //    new ApiScope("DiscountFullPermission","Full authority for discount operations"),
        //    new ApiScope("FavouriteFullPermission","Full authority for favourite operations"),
        //    new ApiScope("İyzicoPaymentFullPermission","Full authority for iyzico payment operations"),
        //    new ApiScope("OrderingFullPermission","Full authority for ordering operations"),
        //    new ApiScope("ProductFullPermission","Full authority for product operations"),
        //    new ApiScope("TagFullPermission","Full authority for tag operations"),
        //    new ApiScope("TestimonialFullPermission","Full authority for testimonial operations"),
        //     new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
        //};


        //public static IEnumerable<Client> Clients => new Client[]
        //{
        //    //VİSİTOR
        //    new Client
        //    {
        //        ClientId="TechConnectVisitorId",
        //        ClientName="TechConnect Visitor User",
        //        AllowedGrantTypes=GrantTypes.ClientCredentials,
        //        ClientSecrets={new Secret("techconnectsecret".Sha256())},
        //        AllowedScopes={"AboutFullPermission","TestimonialFullPermission","TagFullPermission","ProductFullPermission","OrderingFullPermission","İyzicoPaymentFullPermission","DiscountFullPermission","Contact2FullPermission","CompareFullPermission","ColorFullPermission","CategoryFullPermission","BasketTotalFullPermission","AddressFullPermission", "ContactFullPermission","FavouriteFullPermission",
        //        IdentityServerConstants.LocalApi.ScopeName},
        //        AllowAccessTokensViaBrowser=true,

        //    },


        //    //USER
        //    new Client
        //    {
        //        ClientId="TechConnectUserId",
        //        ClientName="TechConnect  User",
        //        AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
        //        ClientSecrets={new Secret("techconnectsecret".Sha256())},
        //        AllowedScopes={"AboutFullPermission","TestimonialFullPermission","TagFullPermission","ProductFullPermission","OrderingFullPermission","İyzicoPaymentFullPermission","DiscountFullPermission","Contact2FullPermission","CompareFullPermission","ColorFullPermission","CategoryFullPermission","BasketTotalFullPermission","AddressFullPermission", "ContactFullPermission","FavouriteFullPermission",

        //        IdentityServerConstants.LocalApi.ScopeName, //*** 
        //        IdentityServerConstants.StandardScopes.Email,
        //        IdentityServerConstants.StandardScopes.OpenId,
        //        IdentityServerConstants.StandardScopes.Profile
        //        }
        //    },


        //    //Admin
        //    new Client
        //    {
        //        ClientId="TechConnectAdminId",
        //        ClientName="TechConnect Admin User",
        //        AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
        //        ClientSecrets={new Secret("techconnectsecret".Sha256())},
        //        AllowedScopes={"AboutFullPermission","TestimonialFullPermission","TagFullPermission","ProductFullPermission","OrderingFullPermission","İyzicoPaymentFullPermission","DiscountFullPermission","Contact2FullPermission","CompareFullPermission","ColorFullPermission","CategoryFullPermission","BasketTotalFullPermission","AddressFullPermission", "ContactFullPermission","FavouriteFullPermission",
        //        IdentityServerConstants.LocalApi.ScopeName, //*** 
        //        IdentityServerConstants.StandardScopes.Email,
        //        IdentityServerConstants.StandardScopes.OpenId,
        //        IdentityServerConstants.StandardScopes.Profile
        //        },
        //        AccessTokenLifetime=600 //6 dk sonra token silinir.
        //    }
        //};


        //korumak istediğin apileri temsil eder
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            //her mikroservis için o mikrosevise erişim için bir key belirlicez
           new ApiResource("Resource"){Scopes={ "AboutFullPermission", "ContactFullPermission", "UserPermission", "AdminPermission", "VisitorPermission" } },
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
            new ApiScope("AboutFullPermission","Full authority for about operations"),  //kullanım şekillerinden biride api kısmında authorize policy ekleyip, sonrada program.cs te bunun iznini ekleyince kısıltanıyor fakat ui kısmında bunu yapınca sıkıntı oluyor :(
            new ApiScope("ContactFullPermission","Full authority for contact operations"),
            new ApiScope("UserPermission","Full authority for user operations"),
            new ApiScope("AdminPermission","Full authority for admin operations"),
            new ApiScope("VisitorPermission","Full authority for visitor operations"),
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
                AllowedScopes={"AboutFullPermission", "ContactFullPermission","VisitorPermission",
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
                AllowedScopes={"AboutFullPermission", "ContactFullPermission","UserPermission",

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
                 AllowedScopes={"AboutFullPermission", "ContactFullPermission","AdminPermission","UserPermission",
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