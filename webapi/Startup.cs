using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace webapi
{
    public class Startup
    {
        private readonly string API_GUID = string.Empty;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            API_GUID = Guid.NewGuid().ToString();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "API  No 1" });
                //c.IncludeXmlComments(System.AppDomain.CurrentDomain.BaseDirectory + @"webapi.xml");
            });

            var key = "A_VERY_SECRET_KEy";
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "web_api");
            });

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Response-Time-Milliseconds", API_GUID);
                await next.Invoke();
                //context.Response.Headers.Add("X-Response-Time-PostInvoke", "post-value");
                // NOTE: System.InvalidOperationException: Headers are read-only, response has already started.
                await context.Response.WriteAsync("\n\r------- After ------");
            });


            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
