using System.Xml.Linq;
using Microsoft.AspNetCore.SignalR;
using OptimaJet.Workflow.Core;
using OptimaJet.Workflow.Core.Builder;
using OptimaJet.Workflow.Core.Parser;
using OptimaJet.Workflow.Core.Runtime;
using OptimaJet.Workflow.DbPersistence;
using OptimaJet.Workflow.Plugins;
using WorkflowApi.Hubs;

namespace WorkflowLib;

public static class WorkflowInit
{
    public static WorkflowRuntime GetRuntime(MSSQLProvider provider)
    {
        //WorkflowRuntime.RegisterLicense(Secrets.LicenseKey);

        var builder = new WorkflowBuilder<XElement>(
            provider,
            new XmlWorkflowParser(),
            provider
        ).WithDefaultCache();

        // we need BasicPlugin to send email
        var basicPlugin = new BasicPlugin
        {
            Setting_MailserverFrom = "mail@gmail.com",
            Setting_Mailserver = "smtp.gmail.com",
            Setting_MailserverSsl = true,
            Setting_MailserverPort = 587,
            Setting_MailserverLogin = "mail@gmail.com",
            Setting_MailserverPassword = Secrets.MailPassword
        };
        var runtime = new WorkflowRuntime()
            .WithPlugin(basicPlugin)
            .WithBuilder(builder)
            .WithPersistenceProvider(provider)
            .EnableCodeActions()
            .SwitchAutoUpdateSchemeBeforeGetAvailableCommandsOn()
            .WithCustomActivities(new List<ActivityBase> { new WeatherActivity() })
            .WithRuleProvider(new SimpleRuleProvider())
            .WithDesignerParameterFormatProvider(new DesignerParameterFormatProvider())
            .AsSingleServer();

        // events subscription
        runtime.OnProcessActivityChangedAsync += (sender, args, token) => Task.CompletedTask;
        runtime.OnProcessStatusChangedAsync += (sender, args, token) => Task.CompletedTask;

        return runtime;
    }

    public static MSSQLProvider GetProvider(string connectionString)
    {
        var provider = new MSSQLProvider(connectionString);
        return provider;
    }
}
