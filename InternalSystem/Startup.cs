using InternalSystem.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternalSystem
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
            //�ϥ�cookie������
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
            {
                //���n�J�ɷ|�۰ʾɨ�n�J����
                option.LoginPath = new PathString("/api/LoginTest/NoLogin");

                /*
                //�v���Q�ڵ��˨�n�J����
                option.AccessDeniedPath= new PathString("/api/LoginTest/NoAccess");
                */
            });

            /*
            //������API��������(�n�J)�~��ϥΥ\��
            services.AddMvc(options =>
            {
                options.Filters.Add(new AuthorizeFilter());
            });
            */

            //�`�J���o�ϥΪ̸�T
            //services.AddHttpContextAccessor();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "InternalSystem", Version = "v1" });
            });
            services.AddDbContext<MSIT44Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MSIT44DbContext")));
            services.AddDbContext<MSIT44Context2>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MSIT44DbContext")));
            //services.AddControllers().AddNewtonsoftJson(options =>
            //    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InternalSystem v1"));
            }

            app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            //Cookie�]�w�A���ǥ����n�@��
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
