#pragma checksum "C:\Users\tlokh\OneDrive\Desktop\Project\WOW_proj\Prosares.Wow.Login\Views\ResetPassword\ForgetPassword.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "aa9f9e764e518e49750f8a691bc772f9c5adb933"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ResetPassword_ForgetPassword), @"mvc.1.0.view", @"/Views/ResetPassword/ForgetPassword.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"aa9f9e764e518e49750f8a691bc772f9c5adb933", @"/Views/ResetPassword/ForgetPassword.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f1861ed5d2387b7b63988446d32212d6ac0ffff3", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_ResetPassword_ForgetPassword : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Prosares.Wow.Data.Entities.EmployeeMasterEntity>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("control-label"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("email"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"

<style>
    form span{
        margin-left: -30px;
        cursor:pointer;
    }


    .pass_show{position: relative} 

    .pass_show .ptxt { 

        position: absolute; 
        top: 50%; 
        right: 20px; 
        z-index: 1; 
        margin-top: -10px; 
        cursor: pointer; 
        transition: .3s ease all; 
    } 

    .ptxt-2{
        position: absolute; 
        top: 7%; 
        right: 20px; 
        z-index: 1; 
        margin-top: -10px; 
        cursor: pointer; 
        transition: .3s ease all; 
    }


    .pass_show .ptxt:hover{color: #333333;} 

    label {
        color: white;
    }

    .main{
        display: flex;
        justify-content: center;
        align-items: center;
        height: 90vh;
    }

    .row{
        display: flex;
        justify-content: center;
        align-items: center;
    }

    #notif, #notif_err {
        position: absolute;
        display: flex;
        gap: 20px;
        justify-content:");
            WriteLiteral(@" center;
        align-items: center;
        top: 100px;
    }

    
</style>



<div class=""d-flex justify-content-center align-items-center my-4 position-relative"">
    <div class=""alert alert-success alert-dismissible fade show"" role=""alert"" id=""notif"">
        <i class=""fa fa-check-circle"" style=""font-size:22px;color:green""></i>
        <span> Password reset succesfully</span>
        <button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close"">
            <span aria-hidden=""true"">&times;</span>
        </button>
    </div>

    <div class=""alert alert-danger alert-dismissible fade show"" role=""alert"" id=""notif_err"">
        <i class=""fa fa-times-circle-o"" style=""font-size:22px;color:red""></i>
        <span> Something went wrong!</span>
        <button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close"">
            <span aria-hidden=""true"">&times;</span>
        </button>
    </div>
</div>


<div class=""main"">
    <div class=""container"">
	   ");
            WriteLiteral(" <div class=\"row\">\r\n\t\t    <div class=\"col-sm-4\">\r\n\r\n                <div class=\"form-group\">\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "aa9f9e764e518e49750f8a691bc772f9c5adb9336746", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 98 "C:\Users\tlokh\OneDrive\Desktop\Project\WOW_proj\Prosares.Wow.Login\Views\ResetPassword\ForgetPassword.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.LoginId);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("hidden", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "aa9f9e764e518e49750f8a691bc772f9c5adb9338624", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#nullable restore
#line 99 "C:\Users\tlokh\OneDrive\Desktop\Project\WOW_proj\Prosares.Wow.Login\Views\ResetPassword\ForgetPassword.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.LoginId);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("hidden", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                </div>

		        <label>New Password</label>
                <div class=""form-group pass_show""> 
                      <span> <i class=""fa fa-eye-slash ptxt"" id=""showPass_2""   aria-hidden=""true""></i></span>
                      <input type=""password"" id=""Password"" class=""form-control"" placeholder=""Password""> 
                </div> 

		        <label>Confirm Password</label>
                <div class=""form-group pass_show""> 
                    <span> <i class=""fa fa-eye-slash ptxt"" id=""showPass_3""   aria-hidden=""true""></i></span>
                    <input type=""password"" id=""ConfirmPassword"" class=""form-control"" placeholder=""Confirm Password""> 
                </div>
                

                <div class=""form-group "">
                    <button id=""submit"" class=""btn btn-primary"" type=""button"">Reset Password</button>
                </div>

                <div class=""form-group"">
                     <span id=""duplicate_pass""  class=""text-danger"" style=""dis");
            WriteLiteral(@"play:none"">Password and confirm doesnot match.</span>
                     <span id=""empty_pass""  class=""text-danger"" style=""display:none"">Password or confirm password cannot be empty.</span>
                    <span id=""min_pass"" class=""text-danger"" style=""display:none"">Minimum 5 charachters and 1 numeric degit required.</span>
                    <span id=""alpha_numeric"" class=""text-danger"" style=""display:none""> Password must contain at least 1 numeric digits and 5 charachters</span>
                </div>
		    </div>  
	    </div>
    </div>

</div>



<script>

    $(document).ready(() => {
      
        $(""#min_pass"").hide();
        $(""#empty_pass"").hide();
        $(""#duplicate_pass"").hide();
        $(""#notif"").hide();
        $(""#notif_err "").hide();
       

        $(""#showPass_2"").click(() => {
            let type = $(""#Password"").attr('type');
            if (type == 'password') {
                $('#Password').attr('type', 'text');
                $('#showPass_2')");
            WriteLiteral(@".removeClass('fa fa-eye-slash ptxt');
                $(""#showPass_2"").addClass('fa fa-eye  ptxt-2')
            } else {
                $('#Password').attr('type', 'password');
                $(""#showPass_2"").removeClass('fa fa-eye  ptxt-2')
                $('#showPass_2').addClass('fa fa-eye-slash ptxt');
            }
        });
    
    
    
        $(""#showPass_3"").click(() => {
            let type = $(""#ConfirmPassword"").attr('type');
            if (type == 'password') {
                $('#ConfirmPassword').attr('type', 'text');
                $('#showPass_3').removeClass('fa fa-eye-slash ptxt');
                $(""#showPass_3"").addClass('fa fa-eye  ptxt-2')
            } else {
                $('#ConfirmPassword').attr('type', 'password');
                $(""#showPass_3"").removeClass('fa fa-eye  ptxt-2')
                $('#showPass_3').addClass('fa fa-eye-slash ptxt');
            }
        });

    });




     $(document).on('keyup','#ConfirmPassword , #Passwor");
            WriteLiteral(@"d', ()=>{
            $(""#empty_pass"").hide();
            $(""#duplicate_pass"").hide();
            $(""#min_pass"").hide();
            $(""#alpha_numeric"").hide();
     })


    $("".close"").click(() => {
        $(""#passreset"").hide();
    })

    $(""#submit"").click(() => {
      
        let LoginId = $(""#email"").val();
        let Password = $(""#Password"").val();
        let ConfirmPassword = $(""#ConfirmPassword"").val();

         
        
        

        console.log(Password, Password.trim(), Password.replace(/\s/g, ''))


        var letter = /[a-zA-Z]/;
        var number = /[0-9]/;
        var valid = number.test(Password) && letter.test(Password);
       

        let data = { LoginId: LoginId, Password: Password };

        if (Password.length == 0 || Password == null) {
            $(""#empty_pass"").show();
            return;
        }

        if (Password.length < 7) {
            $(""#min_pass"").show();
            return;
        }

        if (!valid) {");
            WriteLiteral(@"
            $(""#alpha_numeric"").show();
            return;
        }

        if (Password != ConfirmPassword) {
           $(""#duplicate_pass"").show();
            return;
        }

        $.ajax({
            type: 'POST',
            url: ""/ResetPassword/ForgetPassword"",
            data: data,
            dataType: ""text"",
            success: function (data) {
                console.log(data);
                if (data == true || data == 'true') {
                    $(""#notif"").show();
                    setTimeout(function () {
                        $(""#notif"").hide();
                    }, 5000);
                    window.location.href = window.location.origin;
                }
                else {
                    $(""#notif_err"").show();
                    setTimeout(function () {
                        $(""#notif_err"").hide();
                    }, 5000);

                }
            }

        })
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Prosares.Wow.Data.Entities.EmployeeMasterEntity> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
