
namespace UniApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static string ConString;

        public void ConfigureServices(IServiceCollection services)
        {
            ConString = Configuration.GetConnectionString("ConString");
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:5173")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("AllowSpecificOrigin");
        }
    }
}