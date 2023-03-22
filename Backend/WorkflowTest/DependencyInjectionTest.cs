using OptimaJet.Workflow.Core.Runtime;
using OptimaJet.Workflow.DbPersistence;

namespace WorkflowTest;

public class DependencyInjectionTest
{
    private WorkflowRuntime _runtime;
    private MSSQLProvider _provider;
    public DependencyInjectionTest(WorkflowRuntime runtime, MSSQLProvider provider)
    {
        _runtime = runtime;
        _provider = provider;
    }
    [Fact]
    public void RuntimeExistTest()
    {
        Assert.NotNull(_runtime);
    }

    [Fact]
    public void ProviderExistTest()
    {
        Assert.NotNull(_provider);
    }
}