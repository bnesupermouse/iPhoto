﻿using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using Nancy;

namespace Host
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Adding to the pipeline with our own middleware
            app.Use(async (context, next) =>
            {
                // Add Header
                context.Response.Headers["Product"] = "Web Api Self Host";

                // Call next middleware
                await next.Invoke();
            });

            // Custom Middleare
            app.Use(typeof(CustomMiddleware));

            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}/{id2}/{id3}",
                defaults: new { id = RouteParameter.Optional, id2 = RouteParameter.Optional, id3 = RouteParameter.Optional }
            );

            // Web Api
            app.UseWebApi(config);

            // File Server
            var options = new FileServerOptions
            {
                EnableDirectoryBrowsing = true,
                EnableDefaultFiles = true,
                DefaultFilesOptions = { DefaultFileNames = { "index.html" } },
                FileSystem = new PhysicalFileSystem(@"../../WebSrc/app"),
                StaticFileOptions = { ContentTypeProvider = new CustomContentTypeProvider() }
            };

            app.UseFileServer(options);

            // Nancy
            app.UseNancy();
        }
    }
}
