#pragma checksum "C:\Users\tlokh\OneDrive\Desktop\Project\WOW_proj\Prosares.Wow.Login\Views\ResetPassword\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3730889b6fe4b633c0bf84be49e312d3724b21d4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ResetPassword_Index), @"mvc.1.0.view", @"/Views/ResetPassword/Index.cshtml")]
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
#line 1 "C:\Users\tlokh\OneDrive\Desktop\Project\WOW_proj\Prosares.Wow.Login\Views\_ViewImports.cshtml"
using WowLoginModule;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\tlokh\OneDrive\Desktop\Project\WOW_proj\Prosares.Wow.Login\Views\_ViewImports.cshtml"
using WowLoginModule.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3730889b6fe4b633c0bf84be49e312d3724b21d4", @"/Views/ResetPassword/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f1861ed5d2387b7b63988446d32212d6ac0ffff3", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_ResetPassword_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\tlokh\OneDrive\Desktop\Project\WOW_proj\Prosares.Wow.Login\Views\ResetPassword\Index.cshtml"
  
    Layout = "_Layout";
    ViewData["Title"] = "Reset Password";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<script src=""/lib/jquery/dist/jquery.min.js""></script>




<div>
    Hello World
</div>
<div class=""text-center"">
  
    <div class=""container mt-5 mb-5 d-flex justify-content-center"">
        <div class=""card px-1 py-4 form_container"">
            <div class=""card-body"">
                <div class=""row"">
                    <div class=""col-sm-12"">
                        <div class=""form-group"">
                            <div class=""input-group"">
                                <input class=""form-control"" id=""email"" type=""text"" placeholder=""Email ID"">
                            </div>
                        </div>
                        <div class=""form-group"">
                            <div id=""valid"" class=""col-sm-12 invalid-feedback"">
                                Email Doesnot Exists!
                            </div>
                            <div class=""invalid-feedback col-sm-12"" id=""empty_email"">Email Cannot be empty!</div>
                        </div>
      ");
            WriteLiteral("              </div>\r\n                    \r\n\r\n                </div>\r\n\r\n");
            WriteLiteral(@"
                <div class="" d-flex flex-column text-center px-5 mt-3 mb-3"">
                    <button class=""btn btn-primary btn-block confirm-button"" id=""btn"">Reset Password</button>
                </div>
            </div>
        </div>
    </div>
</div>




<script>
    $(document).ready(() => {
        $(""#valid"").hide();
        $(""#mailSent"").hide();
        $(""#empty_email"").hide();
        let validation = [];
    })

    $("".close"").click(() => {
        $(""#mailSent"").hide();
    })

    $(""#email"").keyup(() => {
        $(""#valid"").hide();
        $(""#empty_email"").hide();
    })

    $(""#btn"").click(() => {
        let email = $(""#email"").val();
        let url = window.location.origin;
        let data = { Email: email, Url: url };
        console.log(data)
        if (email.length == 0 || email == null) {
            $(""#empty_email"").show();
        } else {

            $.ajax({
                type: 'POST',
                url: ""/ResetPassword/Ind");
            WriteLiteral(@"ex"",
                data: data,
                dataType: ""text"",
                success: function (data) {
                    console.log(data);
                    if (data == true || data == 'true') {
                       
                    }
                    else {
                        $(""#email"").val("""");
                        $(""#mailSent"").show();
                        setTimeout(function () {
                            $(""#mailSent"").hide();
                        }, 5000);
                    }
                }

            })

        }

    })
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
