
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Models;
using WebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Linq;
using System.Reflection;
using System.IO;
using System;
using System.Xml;

namespace WebApi
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
            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Secret").Value);

            services.AddControllers();
            services.AddCors();
            services.AddDbContext<CoreDbContext>();
               


            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                                .AddJwtBearer(options =>
                                {
                                    options.RequireHttpsMetadata = false;

                                    options.TokenValidationParameters = new TokenValidationParameters
                                    {
                                        ValidateIssuerSigningKey = true,
                                        IssuerSigningKey = new SymmetricSecurityKey(key),
                                        ValidateIssuer = false,
                                        ValidateAudience = false,
                                    };
                                });

            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                    var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name }.xml";
                    var xmlfilefullpath = Path.Combine(AppContext.BaseDirectory, xmlfile);
                    c.IncludeXmlComments(xmlfilefullpath);
                });
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;

            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}





