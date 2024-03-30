using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Grpc.Net.Client;

internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Replace the gRPC client registration with custom configuration
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(GrpcChannelOptions));

            services.Remove(descriptor);

            services.AddSingleton(sp =>
            {
                var channel = GrpcChannel.ForAddress("http://localhost:5000"); // Set your host and port here
                var options = new GrpcChannelOptions { HttpClient = new HttpClient() };
                channel = GrpcChannel.ForAddress("http://localhost:5000", options);
                return channel;
            });
        });
        
        // configure command line arguments
        builder.UseSetting("https_port", "5001");
        builder.UseSetting("http_port", "5000");
        
        
        base.ConfigureWebHost(builder);
    }
}