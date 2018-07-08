
namespace UserManagement
{
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
    using Microsoft.IdentityModel.Tokens;

    using NSwag.AspNetCore;

    using UserManagement.Models;

    using Serilog;
    using Serilog.Sinks.MSSqlServer;
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();
            ColumnOptions columnOptions = new ColumnOptions();
            columnOptions.Store.Add(StandardColumn.LogEvent);
            Log.Logger = new LoggerConfiguration()
                .WriteTo.MSSqlServer(Configuration["ConnectionStrings:DefaultConnection"], "Logs", columnOptions: columnOptions)
                .WriteTo.RollingFile(Path.Combine(env.ContentRootPath + @"\wwwroot\Logs", "log-{Date}.txt"))
                .CreateLogger();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            SeedDatabase.Initialize(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);
            app.UseMvc();
            app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, new SwaggerUiOwinSettings());
        }
    }
}
