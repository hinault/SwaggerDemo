using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;

namespace SwaggerDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

         services.AddSwaggerGen(c =>
          {
             c.SwaggerDoc("v1", new Info { Title = "SwaggerDemo", Version = "v1" });

              c.SwaggerDoc("v2", new Info
             {
                Version = "v2",
                Title = "SwaggerDemo API",
                Description = "Customers API to demo Swagger",
                TermsOfService = "None",
                Contact = new Contact 
                { 
                    Name = "Hinault Romaric", 
                    Email = "hinault@monsite.com", 
                    Url = "http://rdonfack.developpez.com/"
                },
                License = new License
                 { 
                     Name = "Apache 2.0", 
                     Url = "http://www.apache.org"
                 }
            });

              var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "SwaggerDemo.xml");

              c.IncludeXmlComments(filePath);
        
          });

          

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();

             app.UseSwagger();

             app.UseSwaggerUI(c =>
             {
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "SwaggerDemo v1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "SwaggerDemo v2");

                 c.InjectStylesheet("/swagger-ui/css/custom.css");
             });

            app.UseMvc();
           
        }
    }
}
