#pragma checksum "C:\Users\tlokh\OneDrive\Desktop\Project\WOW_proj\Prosares.Wow.Login\Views\ResetPassword\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c199a26ebe2c7db58319fef83177a7356554204f"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c199a26ebe2c7db58319fef83177a7356554204f", @"/Views/ResetPassword/Index.cshtml")]
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
  
    ViewData["Title"] = "Reset Password";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral(@"

<style>
    #notif, #notif_err {
        position: absolute;
        display: flex;
        gap: 20px;
        justify-content: center;
        align-items: center;
        top: 100px;
    }

    .email_form{
        margin-top: 100px;
    }
</style>

    <div class=""d-flex justify-content-center align-items-center my-4 position-relative"">
        <div class=""alert alert-success alert-dismissible fade show"" role=""alert"" id=""notif"">
            <i class=""fa fa-check-circle"" style=""font-size:22px;color:green""></i>
            <span> Mail sent succesfully</span>
            <button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close"">
                <span aria-hidden=""true"">&times;</span>
            </button>
        </div>

    <div class=""alert alert-danger alert-dismissible fade show"" role=""alert"" id=""notif_err"">
            <i class=""fa fa-times-circle-o"" style=""font-size:22px;color:red""></i>
            <span> Something went wrong!</span>
            <button t");
            WriteLiteral(@"ype=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close"">
                <span aria-hidden=""true"">&times;</span>
            </button>
        </div>
    </div>

    <div class=""text-center"">
  
    <div class=""container mt-5 mb-5 d-flex justify-content-center"">
        <div class=""card px-1 py-4 form_container"">
            <div class=""card-body"">
                <div class=""row"">
                    <div class=""col-sm-12"">
                        <div class=""form-group email_form"">
                            <div class=""input-group"">
                                <input class=""form-control"" id=""email"" type=""text"" placeholder=""Email ID"">
                            </div>
                        </div>
                        <div class=""form-group"">
                            <div id=""valid"" class=""col-sm-12 invalid-feedback"">
                                Email Doesnot Exists!
                            </div>
                            <div class=""invalid-feedback ");
            WriteLiteral("col-sm-12\" id=\"empty_email\">Email Cannot be empty!</div>\r\n                        </div>\r\n                    </div>\r\n                    \r\n\r\n                </div>\r\n\r\n");
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
        $(""#notif"").hide();
        $(""#notif_err"").hide();
       
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
        console.log(data);



        if (email.length == 0 || email == null) {
            $(""#empty_email"").show();
        } else {

            $.ajax({
                type:");
            WriteLiteral(@" 'POST',
                url: ""/ResetPassword/Index"",
                data: data,
                dataType: ""text"",
                success: function (data) {
                    console.log(data, typeof data);

                    if (data == 0 || data == '0') {
                        $(""#notif"").show();
                        setTimeout(function () {
                            $(""#notif"").hide();
                        }, 5000);
                        $(""#email"").val("""");


                    }
                    else if (data == 1 || data == '1'){
                      $(""#valid"").show();
                    }
                    else {
                        console.log('Something went wrong')
                        $(""#notif_err"").show();
                        setTimeout(function () {
                            $(""#notif_err"").hide();
                        }, 5000);
                    }
                   
                }

            })

        }

    }");
            WriteLiteral(")\r\n</script>");
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