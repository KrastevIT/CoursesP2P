#pragma checksum "C:\Users\Кристиян Кръстев\Desktop\CoursesP2P\CoursesP2P.App\Views\Courses\MyCourses.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0ded12b4082beb624311d4599279e9e6d5e18de1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Courses_MyCourses), @"mvc.1.0.view", @"/Views/Courses/MyCourses.cshtml")]
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
#line 1 "C:\Users\Кристиян Кръстев\Desktop\CoursesP2P\CoursesP2P.App\Views\_ViewImports.cshtml"
using CoursesP2P.App;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Кристиян Кръстев\Desktop\CoursesP2P\CoursesP2P.App\Views\_ViewImports.cshtml"
using CoursesP2P.App.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Кристиян Кръстев\Desktop\CoursesP2P\CoursesP2P.App\Views\_ViewImports.cshtml"
using CoursesP2P.App.Models.BindingModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Кристиян Кръстев\Desktop\CoursesP2P\CoursesP2P.App\Views\_ViewImports.cshtml"
using CoursesP2P.App.Models.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Кристиян Кръстев\Desktop\CoursesP2P\CoursesP2P.App\Views\_ViewImports.cshtml"
using CoursesP2P.Models.Enum;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0ded12b4082beb624311d4599279e9e6d5e18de1", @"/Views/Courses/MyCourses.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"84d0f07253eb3333f78abcfc958d256902d5efb7", @"/Views/_ViewImports.cshtml")]
    public class Views_Courses_MyCourses : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CourseViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Lectures", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-sm btn-outline-info"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Кристиян Кръстев\Desktop\CoursesP2P\CoursesP2P.App\Views\Courses\MyCourses.cshtml"
  
    ViewData["Title"] = "MyCourses";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1 class=\"text-center\">My Enrolled Courses</h1>\r\n\r\n\r\n<br />\r\n\r\n<div class=\"container\">\r\n    <div class=\"row\">\r\n");
#nullable restore
#line 13 "C:\Users\Кристиян Кръстев\Desktop\CoursesP2P\CoursesP2P.App\Views\Courses\MyCourses.cshtml"
         foreach (var course in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"col-md-4\">\r\n                <figure class=\"card card-product\">\r\n                    <div class=\"img-wrap\"><img");
            BeginWriteAttribute("src", " src=\"", 381, "\"", 400, 1);
#nullable restore
#line 17 "C:\Users\Кристиян Кръстев\Desktop\CoursesP2P\CoursesP2P.App\Views\Courses\MyCourses.cshtml"
WriteAttributeValue("", 387, course.Image, 387, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"width:348px;height:220px;\"></div>\r\n                    <figcaption class=\"info-wrap\">\r\n                        <h4 class=\"title\">");
#nullable restore
#line 19 "C:\Users\Кристиян Кръстев\Desktop\CoursesP2P\CoursesP2P.App\Views\Courses\MyCourses.cshtml"
                                     Write(course.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                        <ul class=\"list-group list-group-flush\">\r\n                            <li class=\"list-group-item\">by ");
#nullable restore
#line 21 "C:\Users\Кристиян Кръстев\Desktop\CoursesP2P\CoursesP2P.App\Views\Courses\MyCourses.cshtml"
                                                      Write(course.InstructorFullName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n                            <li class=\"list-group-item\">");
#nullable restore
#line 22 "C:\Users\Кристиян Кръстев\Desktop\CoursesP2P\CoursesP2P.App\Views\Courses\MyCourses.cshtml"
                                                   Write(course.Category);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n                            <li class=\"list-group-item\">Lectures 0</li>\r\n                        </ul>\r\n                    </figcaption>\r\n                    <div class=\"text-center\">\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0ded12b4082beb624311d4599279e9e6d5e18de17260", async() => {
                WriteLiteral("View Lectures");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 27 "C:\Users\Кристиян Кръстев\Desktop\CoursesP2P\CoursesP2P.App\Views\Courses\MyCourses.cshtml"
                                                                          WriteLiteral(course.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    </div>\r\n                </figure>\r\n            </div>\r\n");
#nullable restore
#line 31 "C:\Users\Кристиян Кръстев\Desktop\CoursesP2P\CoursesP2P.App\Views\Courses\MyCourses.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CourseViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
