namespace MathZ.GatewayAPI
{
    using MathZ.Services.ServiceDefaults;
    using Ocelot.DependencyInjection;
    using Ocelot.Middleware;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.AddServiceDefaults();

            builder.Configuration.AddJsonFile("ocelot.json", false, true);

            builder.Services.AddOcelot(builder.Configuration);

            var app = builder.Build();

            app.MapDefaultEndpoints();

            app.UseOcelot().GetAwaiter().GetResult();

            app.Run();
        }
    }
}
