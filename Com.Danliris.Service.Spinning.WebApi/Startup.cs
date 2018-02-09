using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.AccessTokenValidation;
using IdentityModel;
using Newtonsoft.Json.Serialization;
using Com.Danliris.Service.Spinning.Lib;
using Com.Danliris.Service.Spinning.Lib.Services;
using Microsoft.AspNetCore.Mvc;

namespace Com.Danliris.Service.Spinning.WebApi
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
            //string connectionString = Configuration.GetConnectionString("DefaultConnection") ?? Configuration["DefaultConnection"];
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=com.danliris.db.spinning;Trusted_Connection=True;";

            services
                .AddDbContext<SpinningDbContext>(options => options.UseSqlServer(connectionString))
                .AddApiVersioning(options =>
                {
                    options.ReportApiVersions = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                });

            services
                .AddTransient<YarnService>()
                .AddTransient<LotYarnService>()
                .AddTransient<SpinningInputProductionService>()
                .AddTransient<SpinningInputProduction_InputDetailsService>()
                .AddTransient<YarnOutputProductionService>();


            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.ApiName = "com.danliris.service.spinning";
                    options.ApiSecret = "secret";
                    options.Authority = "https://localhost:44350";
                    options.RequireHttpsMetadata = false;
                    options.NameClaimType = JwtClaimTypes.Name;
                    options.RoleClaimType = JwtClaimTypes.Role;
                });

            services
                .AddMvcCore()
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                .AddAuthorization(options =>
                {
                    options.AddPolicy("service.core.read", (policyBuilder) =>
                    {
                        policyBuilder.RequireClaim("scope", "service.core.read");
                    });
                })
                .AddJsonFormatters();

            services.AddCors(options => options.AddPolicy("SpinningPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SpinningDbContext>();
                context.Database.Migrate();
            }
            app.UseAuthentication();
            app.UseCors("SpinningPolicy");
            app.UseMvc();
        }
    }
}
