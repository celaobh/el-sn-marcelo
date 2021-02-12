using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace el_sn_marcelo_web
{
    public class Startup
    {
        SqliteConnection conn;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //if (conn == null)
            //{
            //    conn = new SqliteConnection("Data Source=:memory:");
            //    conn.Open();
            //}
            //var db = new DBInitializer();
            //db.CriarTabelas();
            //db.InserirDados();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddRazorPages();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
            services.AddHttpClient<APIClient>()
                           .SetHandlerLifetime(TimeSpan.FromMinutes(5));
            //.AddPolicyHandler(GetRetryPolicy());

            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    options =>
                    {
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(60),
                        options.LoginPath = "/Login";
                    }
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                            name: "default",
                            pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            Random jitterer = new Random();
            var retryWithJitterPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(c => c.StatusCode == System.Net.HttpStatusCode.NotFound)
                .OrResult(c => c.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                .WaitAndRetryAsync(6,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(1, retryAttempt))
                                  + TimeSpan.FromMilliseconds(jitterer.Next(0, 100)),
                    onRetry: (outcome, timespan, retryAttempt, context) =>
                    {
                        //Log.Information($"Delay de {timespan.TotalMilliseconds}ms, e fazendo retry número {retryAttempt}.");
                    }
                );

            return retryWithJitterPolicy;
        }
    }
}
