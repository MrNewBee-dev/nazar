#pragma checksum "C:\Users\milad\Desktop\Nazar1988\Nazar1988\Views\Home\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "150d5c45532495c0aec622c5b559d72077d90cf2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Details), @"mvc.1.0.view", @"/Views/Home/Details.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\milad\Desktop\Nazar1988\Nazar1988\Views\_ViewImports.cshtml"
using Nazar1988;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\milad\Desktop\Nazar1988\Nazar1988\Views\_ViewImports.cshtml"
using Nazar1988.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"150d5c45532495c0aec622c5b559d72077d90cf2", @"/Views/Home/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c574e794144fdefb135c02c7521de91fd45e6757", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ProductsDiscountModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddToCart", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Orders", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\milad\Desktop\Nazar1988\Nazar1988\Views\Home\Details.cshtml"
  
    ViewData["Title"] = "Details";
    

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"row\" style=\"margin-top: 100px \">\r\n    <!-- Blog Post Content Column -->\r\n    <div class=\"col-lg-8\">\r\n        <!-- Title -->\r\n        <h1 class=\"mb-3\"> ");
#nullable restore
#line 12 "C:\Users\milad\Desktop\Nazar1988\Nazar1988\Views\Home\Details.cshtml"
                     Write(Model.ProductName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </h1>\r\n\r\n\r\n        <hr>\r\n        <!-- Post Content -->\r\n        <div id=\"content\">\r\n            ");
#nullable restore
#line 18 "C:\Users\milad\Desktop\Nazar1988\Nazar1988\Views\Home\Details.cshtml"
       Write(Model.ProductDescription);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <hr>\r\n");
#nullable restore
#line 21 "C:\Users\milad\Desktop\Nazar1988\Nazar1988\Views\Home\Details.cshtml"
         foreach (var item in System.Text.Json.JsonSerializer.Deserialize<List<string>>(Model.ImagePath).ToList())
        
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div>\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "150d5c45532495c0aec622c5b559d72077d90cf25591", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 652, "~/GalleryFiles/", 652, 15, true);
#nullable restore
#line 25 "C:\Users\milad\Desktop\Nazar1988\Nazar1988\Views\Home\Details.cshtml"
AddHtmlAttributeValue("", 667, System.IO.Path.GetFileName(item), 667, 33, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                \r\n            </div>\r\n");
#nullable restore
#line 28 "C:\Users\milad\Desktop\Nazar1988\Nazar1988\Views\Home\Details.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"


        <!-- Blog Comments -->
        <hr>
        <!-- Comments Form -->



    </div>

    <!-- Blog Sidebar Widgets Column -->

    <div class=""col-md-4"">


        
        
        <div class=""card my-4"">
            <div class=""card-body"">
");
#nullable restore
#line 49 "C:\Users\milad\Desktop\Nazar1988\Nazar1988\Views\Home\Details.cshtml"
                 if (Model.StartDate < DateTime.Now && Model.EndtDate > DateTime.Now)
                {
                    if (ViewBag.role == true)
                    {


#line default
#line hidden
#nullable disable
            WriteLiteral("                        <p>برای استفاده از این محصول نیاز است این دوره را با مبلغ  ");
#nullable restore
#line 54 "C:\Users\milad\Desktop\Nazar1988\Nazar1988\Views\Home\Details.cshtml"
                                                                               Write(Model.PriceToziKonande - (Model.PriceToziKonande * Model.DiscountPercent) / 100);

#line default
#line hidden
#nullable disable
            WriteLiteral(" تومان خریداری کنید</p>\r\n");
#nullable restore
#line 55 "C:\Users\milad\Desktop\Nazar1988\Nazar1988\Views\Home\Details.cshtml"
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <p>برای استفاده از این محصول نیاز است این دوره را با مبلغ  ");
#nullable restore
#line 58 "C:\Users\milad\Desktop\Nazar1988\Nazar1988\Views\Home\Details.cshtml"
                                                                               Write(Model.Price - (Model.Price * Model.DiscountPercent) / 100);

#line default
#line hidden
#nullable disable
            WriteLiteral(" تومان خریداری کنید</p>\r\n");
#nullable restore
#line 59 "C:\Users\milad\Desktop\Nazar1988\Nazar1988\Views\Home\Details.cshtml"
                    }


                }
                else
                {
                    if (ViewBag.role == true)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <p>برای استفاده از این محصول نیاز است این دوره را با مبلغ  ");
#nullable restore
#line 67 "C:\Users\milad\Desktop\Nazar1988\Nazar1988\Views\Home\Details.cshtml"
                                                                               Write(Model.PriceToziKonande);

#line default
#line hidden
#nullable disable
            WriteLiteral(" تومان خریداری کنید</p>\r\n");
#nullable restore
#line 68 "C:\Users\milad\Desktop\Nazar1988\Nazar1988\Views\Home\Details.cshtml"
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <p>برای استفاده از این محصول نیاز است این دوره را با مبلغ  ");
#nullable restore
#line 71 "C:\Users\milad\Desktop\Nazar1988\Nazar1988\Views\Home\Details.cshtml"
                                                                               Write(Model.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral(" تومان خریداری کنید</p>\r\n");
#nullable restore
#line 72 "C:\Users\milad\Desktop\Nazar1988\Nazar1988\Views\Home\Details.cshtml"
                    }

                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "150d5c45532495c0aec622c5b559d72077d90cf210732", async() => {
                WriteLiteral("\r\n                    <input type=\"hidden\" name=\"course\">\r\n                    <button type=\"submit\" class=\"btn btn-success\">خرید محصول</button>\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 76 "C:\Users\milad\Desktop\Nazar1988\Nazar1988\Views\Home\Details.cshtml"
                                                                       WriteLiteral(Model.ProductID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n        \r\n        \r\n\r\n\r\n\r\n        <!-- Search Widget -->\r\n        \r\n\r\n        <!-- Categories Widget -->\r\n        \r\n    </div>\r\n</div>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ProductsDiscountModel> Html { get; private set; }
    }
}
#pragma warning restore 1591