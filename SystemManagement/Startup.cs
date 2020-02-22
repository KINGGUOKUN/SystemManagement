using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Autofac;
using Autofac.Configuration;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using SystemManagement.Common;
using SystemManagement.Dto;
using SystemManagement.Repository;
using SystemManagement.Repository.Contract;
using SystemManagement.Service;

namespace SystemManagement
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
            services.AddControllers(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                 .AddJsonOptions(options =>
                 {
                     options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                     options.JsonSerializerOptions.Converters.Add(new DateTimeNullableConverter());
                 });

            services.AddDbContextPool<SystemManageDbContext>(options => 
                options.UseMySql(Configuration.GetConnectionString("Default"), mySqlOptions => 
                    mySqlOptions.ServerVersion(new ServerVersion(new Version(8, 0, 18), ServerType.MySql))
            ));

            services.Configure<JWTConfig>(Configuration.GetSection("JWT"));
            var jwtConfig = Configuration.GetSection("JWT").Get<JWTConfig>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtConfig.Issuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SymmetricSecurityKey)),
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(5)
                    };
                    options.Events.OnTokenValidated = context =>
                    {
                        var currentUser = context.HttpContext.RequestServices.GetService<SysUserDto>();
                        var claims = context.Principal.Claims;
                        currentUser.ID = long.Parse(claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value);
                        currentUser.Name = claims.First(x => x.Type == ClaimTypes.Name).Value;
                        currentUser.Email = claims.First(x => x.Type == JwtRegisteredClaimNames.Email).Value;
                        currentUser.RoleId = claims.First(x => x.Type == ClaimTypes.Role).Value;

                        return Task.CompletedTask;
                    };
                });
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins(Configuration.GetValue<string>("CorsHosts"))
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddAutoMapper(typeof(SystemManagementProfile));

            services.AddScoped<SysUserDto>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var module = new ConfigurationModule(Configuration);
            builder.RegisterModule(module);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("default");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
