namespace UnitTestCartAPI;

public class Startup
{
    public void ConfigureHost(IHostBuilder hostBuilder)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddEnvironmentVariables()
            .Build();
        hostBuilder.ConfigureHostConfiguration(builder => { builder.AddConfiguration(config);});
    }
}