#pragma checksum "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4efda9443fae2799b9b63c8b1a9de0a93aae043971c20357d37ffa5441766fa3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Device_UserCodeConfirmation), @"mvc.1.0.view", @"/Views/Device/UserCodeConfirmation.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"4efda9443fae2799b9b63c8b1a9de0a93aae043971c20357d37ffa5441766fa3", @"/Views/Device/UserCodeConfirmation.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"a37bb02fe3277db53ef664b0b2223d70455027585a6423f2ad507316524fd6fa", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Device_UserCodeConfirmation : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DeviceAuthorizationViewModel>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_ValidationSummary", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_ScopeListItem", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("placeholder", new global::Microsoft.AspNetCore.Html.HtmlString("Description or name of device"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-check-input"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-check-label"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Callback", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"page-device-confirmation\">\r\n    <div class=\"lead\">\r\n");
#nullable restore
#line 5 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
         if (Model.ClientLogoUrl != null)
        {

#line default
#line hidden
#nullable disable

            WriteLiteral("            <div class=\"client-logo\"><img");
            BeginWriteAttribute("src", " src=\"", 198, "\"", 224, 1);
            WriteAttributeValue("", 204, 
#nullable restore
#line 7 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
                                                Model.ClientLogoUrl

#line default
#line hidden
#nullable disable
            , 204, 20, false);
            EndWriteAttribute();
            WriteLiteral("></div>\r\n");
#nullable restore
#line 8 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
        }

#line default
#line hidden
#nullable disable

            WriteLiteral("        <h1>\r\n            ");
            Write(
#nullable restore
#line 10 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
             Model.ClientName

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n            <small class=\"text-muted\">is requesting your permission</small>\r\n        </h1>\r\n");
#nullable restore
#line 13 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
         if (Model.ConfirmUserCode)
        {

#line default
#line hidden
#nullable disable

            WriteLiteral("            <p>Please confirm that the authorization request quotes the code: <strong>");
            Write(
#nullable restore
#line 15 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
                                                                                       Model.UserCode

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</strong>.</p>\r\n");
#nullable restore
#line 16 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
        }

#line default
#line hidden
#nullable disable

            WriteLiteral("        <p>Uncheck the permissions you do not wish to grant.</p>\r\n    </div>\r\n\r\n    <div class=\"row\">\r\n        <div class=\"col-sm-8\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "4efda9443fae2799b9b63c8b1a9de0a93aae043971c20357d37ffa5441766fa39419", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4efda9443fae2799b9b63c8b1a9de0a93aae043971c20357d37ffa5441766fa310599", async() => {
                WriteLiteral("\r\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "4efda9443fae2799b9b63c8b1a9de0a93aae043971c20357d37ffa5441766fa310890", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.
#nullable restore
#line 27 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
                        UserCode

#line default
#line hidden
#nullable disable
                );
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                BeginWriteTagHelperAttribute();
                WriteLiteral(
#nullable restore
#line 27 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
                                                        Model.UserCode

#line default
#line hidden
#nullable disable
                );
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n        <div class=\"row\">\r\n            <div class=\"col-sm-8\">\r\n");
#nullable restore
#line 30 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
                 if (Model.IdentityScopes.Any())
                {

#line default
#line hidden
#nullable disable

                WriteLiteral(@"                    <div class=""form-group"">
                        <div class=""card"">
                            <div class=""card-header"">
                                <span class=""glyphicon glyphicon-user""></span>
                                Personal Information
                            </div>
                            <ul class=""list-group list-group-flush"">
");
#nullable restore
#line 39 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
                                 foreach (var scope in Model.IdentityScopes)
                                {

#line default
#line hidden
#nullable disable

                WriteLiteral("                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "4efda9443fae2799b9b63c8b1a9de0a93aae043971c20357d37ffa5441766fa314775", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = 
#nullable restore
#line 41 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
                                                                           scope

#line default
#line hidden
#nullable disable
                ;
                __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
#nullable restore
#line 42 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
                                }

#line default
#line hidden
#nullable disable

                WriteLiteral("                            </ul>\r\n                        </div>\r\n                    </div>\r\n");
#nullable restore
#line 46 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
                }

#line default
#line hidden
#nullable disable

                WriteLiteral("\r\n");
#nullable restore
#line 48 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
                 if (Model.ApiScopes.Any())
                {

#line default
#line hidden
#nullable disable

                WriteLiteral(@"                    <div class=""form-group"">
                        <div class=""card"">
                            <div class=""card-header"">
                                <span class=""glyphicon glyphicon-tasks""></span>
                                Application Access
                            </div>
                            <ul class=""list-group list-group-flush"">
");
#nullable restore
#line 57 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
                                 foreach (var scope in Model.ApiScopes)
                                {

#line default
#line hidden
#nullable disable

                WriteLiteral("                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "4efda9443fae2799b9b63c8b1a9de0a93aae043971c20357d37ffa5441766fa318441", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = 
#nullable restore
#line 59 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
                                                                          scope

#line default
#line hidden
#nullable disable
                ;
                __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
#nullable restore
#line 60 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
                                }

#line default
#line hidden
#nullable disable

                WriteLiteral("                            </ul>\r\n                        </div>\r\n                    </div>\r\n");
#nullable restore
#line 64 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
                }

#line default
#line hidden
#nullable disable

                WriteLiteral(@"
                <div class=""form-group"">
                    <div class=""card"">
                        <div class=""card-header"">
                            <span class=""glyphicon glyphicon-tasks""></span>
                            Description
                        </div>
                        <div class=""card-body"">
                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "4efda9443fae2799b9b63c8b1a9de0a93aae043971c20357d37ffa5441766fa321338", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.
#nullable restore
#line 73 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
                                                                                                             Description

#line default
#line hidden
#nullable disable
                );
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                BeginWriteTagHelperAttribute();
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __tagHelperExecutionContext.AddHtmlAttribute("autofocus", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n\r\n");
#nullable restore
#line 78 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
                 if (Model.AllowRememberConsent)
                {

#line default
#line hidden
#nullable disable

                WriteLiteral("                    <div class=\"form-group\">\r\n                        <div class=\"form-check\">\r\n                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "4efda9443fae2799b9b63c8b1a9de0a93aae043971c20357d37ffa5441766fa324128", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.
#nullable restore
#line 82 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
                                                                     RememberConsent

#line default
#line hidden
#nullable disable
                );
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4efda9443fae2799b9b63c8b1a9de0a93aae043971c20357d37ffa5441766fa325937", async() => {
                    WriteLiteral("\r\n                                <strong>Remember My Decision</strong>\r\n                            ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.
#nullable restore
#line 83 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
                                                                     RememberConsent

#line default
#line hidden
#nullable disable
                );
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n");
#nullable restore
#line 88 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
                }

#line default
#line hidden
#nullable disable

                WriteLiteral(@"            </div>
        </div>

        <div class=""row"">
            <div class=""col-sm-4"">
                <button name=""button"" value=""yes"" class=""btn btn-primary"" autofocus>Yes, Allow</button>
                <button name=""button"" value=""no"" class=""btn btn-secondary"">No, Do Not Allow</button>
            </div>
            <div class=""col-sm-4 col-lg-auto"">
");
#nullable restore
#line 98 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
                 if (Model.ClientUrl != null)
                {

#line default
#line hidden
#nullable disable

                WriteLiteral("                    <a class=\"btn btn-outline-info\"");
                BeginWriteAttribute("href", " href=\"", 4117, "\"", 4140, 1);
                WriteAttributeValue("", 4124, 
#nullable restore
#line 100 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
                                                           Model.ClientUrl

#line default
#line hidden
#nullable disable
                , 4124, 16, false);
                EndWriteAttribute();
                WriteLiteral(">\r\n                        <span class=\"glyphicon glyphicon-info-sign\"></span>\r\n                        <strong>");
                Write(
#nullable restore
#line 102 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
                                 Model.ClientName

#line default
#line hidden
#nullable disable
                );
                WriteLiteral("</strong>\r\n                    </a>\r\n");
#nullable restore
#line 104 "C:\Users\Lenovo\OneDrive\Desktop\Yazılım3\TechConnect\TechConnect.IdentityServer\TechConnect.IdentityServer\Views\Device\UserCodeConfirmation.cshtml"
                }

#line default
#line hidden
#nullable disable

                WriteLiteral("            </div>\r\n        </div>\r\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DeviceAuthorizationViewModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
