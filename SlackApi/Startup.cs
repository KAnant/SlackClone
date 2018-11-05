using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SlackApi.Data;
using SlackApi.Data.Entities;
using SlackApi.Data.Interfaces;
using SlackApi.Services;

namespace SlackApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
       //     var builder = new ConfigurationBuilder()
       //.AddJsonFile("appsettings.json")
       //.AddEnvironmentVariables();
       //     Configuration = builder.Build();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SlackApiDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SlackApiDb")));
            services.AddCors(options =>
            {
                options.AddPolicy("SlackCorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            //services.Configure<SmtpConfig>(Configuration.GetSection("MailSettings"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddTransient<MailService>();
            services.AddTransient<MailSender>();
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            //services.AddSingleton<IEmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            //services.AddTransient<IEmailService, EmailService>();

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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors("SlackCorsPolicy");
            app.UseMvc();
        }
    }
}
