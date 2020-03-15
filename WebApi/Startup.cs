using AutoMapper;
using EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Repos.Abstract;
using Repos.Concrete;

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
            services.AddCors();
            
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    // тут конфигурация поведения веб-апи. В частоности, влияния аттрибута ApiController
                }).AddNewtonsoftJson(options =>
                {
                    // тут я указываю статегию нейминга для входящего json. 
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                        {
                            ProcessDictionaryKeys = true,
                            OverrideSpecifiedNames = true,
                            ProcessExtensionDataNames = true
                        }
                    };
                });

            // db context
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<ApplicationDbContext>();

            services.AddAutoMapper(typeof(Startup));

            // repos
            services.AddScoped<IJobRepository, JobRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => builder.AllowAnyOrigin());

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}