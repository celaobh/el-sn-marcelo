using el_sn_marcelo_api_cadastro.Filters;
using el_sn_marcelo_api_cadastro_application.Ports.Database;
using el_sn_marcelo_api_cadastro_infrastructure.Adapter.Database;
using el_sn_marcelo_api_cadastro_infrastructure.Adapter.Database.BuscaCliente;
using el_sn_marcelo_api_cadastro_infrastructure.Adapter.Database.BuscaOperador;
using el_sn_marcelo_api_cadastro_infrastructure.Adapter.Database.CadastroCliente;
using el_sn_marcelo_api_cadastro_infrastructure.Adapter.Database.CadastroOperador;
using el_sn_marcelo_api_cadastro_infrastructure.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Text;

namespace el_sn_marcelo_api_cadastro
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
            services.AddControllers();
            services.AddScoped<IDatabasePort, DapperConnection>();
            services.AddScoped<ICadastroClientePort, CadastroCliente>();
            services.AddScoped<ICadastroOperadorPort, CadastroOperador>();
            services.AddScoped<ICadastroMarcaPort, CadastroMarca>();
            services.AddScoped<IBuscaClientePort, BuscaCliente>();
            services.AddScoped<IBuscaOperadorPort, BuscaOperador>();
            services.AddScoped<IBuscaMarcaPort, BuscaMarca>();
            services.AddMvc(options => options.Filters.Add(new DefaultExceptionFilter()));

            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "el_sn_marcelo_api_cadastro",
                    Description = "API de cadastros",
                    Version = "v1"
                });

                var apiPath = Path.Combine(AppContext.BaseDirectory, "el_sn_marcelo_api_cadastro.xml");
                c.IncludeXmlComments(apiPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "el_sn_marcelo_api_cadastro");
            });
        }
    }
}
