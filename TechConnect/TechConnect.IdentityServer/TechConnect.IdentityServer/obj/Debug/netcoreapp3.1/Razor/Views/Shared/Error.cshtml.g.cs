#pragma checksum "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Shared\Error.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "dada8f4299f02a05a610bc5ca13161199859a6be14c14b7cfad51540174a4bc9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Error), @"mvc.1.0.view", @"/Views/Shared/Error.cshtml")]
namespace AspNetCore
{
    #line default
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\_ViewImports.cshtml"
using IdentityServerHost.Quickstart.UI

#line default
#line hidden
#nullable disable
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"dada8f4299f02a05a610bc5ca13161199859a6be14c14b7cfad51540174a4bc9", @"/Views/Shared/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"a37bb02fe3277db53ef664b0b2223d70455027585a6423f2ad507316524fd6fa", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared_Error : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ErrorViewModel>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Shared\Error.cshtml"
  
    var error = Model?.Error?.Error;
    var errorDescription = Model?.Error?.ErrorDescription;
    var request_id = Model?.Error?.RequestId;

#line default
#line hidden
#nullable disable

            WriteLiteral("\r\n<div class=\"error-page\">\r\n    <div class=\"lead\">\r\n        <h1>Error</h1>\r\n    </div>\r\n\r\n    <div class=\"row\">\r\n        <div class=\"col-sm-6\">\r\n            <div class=\"alert alert-danger\">\r\n                Sorry, there was an error\r\n\r\n");
#nullable restore
#line 19 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Shared\Error.cshtml"
                 if (error != null)
                {

#line default
#line hidden
#nullable disable

            WriteLiteral("                    <strong>\r\n                        <em>\r\n                            : ");
            Write(
#nullable restore
#line 23 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Shared\Error.cshtml"
                               error

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n                        </em>\r\n                    </strong>\r\n");
#nullable restore
#line 26 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Shared\Error.cshtml"

                    if (errorDescription != null)
                    {

#line default
#line hidden
#nullable disable

            WriteLiteral("                        <div>");
            Write(
#nullable restore
#line 29 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Shared\Error.cshtml"
                              errorDescription

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</div>\r\n");
#nullable restore
#line 30 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Shared\Error.cshtml"
                    }
                }

#line default
#line hidden
#nullable disable

            WriteLiteral("            </div>\r\n\r\n");
#nullable restore
#line 34 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Shared\Error.cshtml"
             if (request_id != null)
            {

#line default
#line hidden
#nullable disable

            WriteLiteral("                <div class=\"request-id\">Request Id: ");
            Write(
#nullable restore
#line 36 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Shared\Error.cshtml"
                                                     request_id

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</div>\r\n");
#nullable restore
#line 37 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Shared\Error.cshtml"
            }

#line default
#line hidden
#nullable disable

            WriteLiteral("        </div>\r\n    </div>\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ErrorViewModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
