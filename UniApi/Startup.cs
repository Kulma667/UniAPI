
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
        }
    }
}