using Microsoft.Extensions.DependencyInjection;
using WorkflowLib;

namespace WorkflowTest;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        string connectionString = "Data Source=(local);Initial Catalog=pre_execution_sample;User ID=sa;Password=1";
        var provider = WorkflowInit.GetProvider(connectionString);
        var runtime = WorkflowInit.GetRuntime(provider);
        services.AddSingleton(provider);
        services.AddSingleton(runtime);
    }
}