
namespace UserManagement
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.PlatformAbstractions;
    using Microsoft.IdentityModel.Tokens;
    
    using UserManagement.Models;

    using Serilog;
    using Serilog.Sinks.MSSqlServer;

    using Swashbuckle.AspNetCore.Swagger;

    using UserManagement.Data;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.AddAuthentication(options =>{
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    }).AddJwtBearer(options =>{
                        options.SaveToken = true;
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters{
                                                                    ValidateIssuer = true,
                                                                    ValidateAudience = true,
                                                                    ValidIssuer = Configuration["Jwt:Iss"],
                                                                    ValidAudience = Configuration["Jwt:Aud"],
                                                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                                                                };
                    });
            services.AddMvc();
            services.AddSwaggerGen(c =>
                {
                    var security = new Dictionary<string, IEnumerable<string>>
                                       {
                                           {"Bearer", new string[] { }},
                                       };
                    c.SwaggerDoc("v1.0", new Info { Title = "Job API", Version = "Entertainment job search API" });
                    c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                                                          {
                                                              Description = "JWT Authorization header using the Bearer scheme.",
                                                              Name = "Authorization",
                                                              In = "header",
                                                              Type = "apiKey"
                                                          });
                    c.AddSecurityRequirement(security);
                    c.DescribeAllEnumsAsStrings();
                    c.OperationFilter<FileUploadOperation>();
                });
            services.ConfigureSwaggerGen(
                options =>
                    {
                        options.DescribeAllEnumsAsStrings();
                        options.OperationFilter<FileUploadOperation>();
                    });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            SeedDatabase.Initialize(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);
            loggerFactory.AddSerilog();
            ColumnOptions columnOptions = new ColumnOptions();
            columnOptions.Store.Add(StandardColumn.LogEvent);
            Log.Logger = new LoggerConfiguration()
                .WriteTo.MSSqlServer(connectionString:Configuration.GetConnectionString("DefaultConnection"),tableName: "Logs", columnOptions: columnOptions)
                .WriteTo.RollingFile(Path.Combine(env.ContentRootPath + @"\wwwroot\Logs", "log-{Date}.txt"))
                .CreateLogger();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Job API");
                    c.DocExpansion(DocExpansion.None);
                });
        }
    }
}
