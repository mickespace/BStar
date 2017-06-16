// ***********************************************************************
// Assembly         : WallE.Platform.dll
// Author           : 
// Created          : 2016-06-13 9:22
// 
// Last Modified By : 郭华华
// Last Modified On : 2016-06-13 9:25
// ***********************************************************************
// <copyright file="SwaggerConfig.cs" company="深圳筑星科技有限公司">
//      Copyright (c) BStar All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Web.Http;
using WebActivatorEx;
using WallE.Platform;
using Swashbuckle.Application;
using WallE.Platform.Support;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WallE.Platform
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;
            GlobalConfiguration.Configuration
                               .EnableSwagger(c =>
                               {
                                   c.SingleApiVersion("v1", "服务器端api");
                                   c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                                   var xmlFilePath = string.Format(@"{0}\bin\helpApi.xml",AppDomain.CurrentDomain.BaseDirectory);
                                   c.IncludeXmlComments(xmlFilePath);
                                   c.CustomProvider((defaultProvider) => new CachingSwaggerProvider(defaultProvider, xmlFilePath));
                               }).EnableSwaggerUi(c =>
                               {
                                   c.InjectJavaScript(thisAssembly, "WallE.Platform.Scripts.swagger_lang.js");
                                   c.SetValidatorUrl("");
                                   c.DisableValidator();
                               });
        }
    }
}