# Serilog.Sinks.MFilesSysUtilsEventLog

A Serilog sink to write to the Windows EventLog in an M-Files Vault Application (via SysUtils).

*Please note that this library is provided "as-is" and with no warranty, explicit or otherwise. You should ensure that the functionality meets your requirements, and thoroughly test them, prior to using in any production scenarios.*

## Use case

An Vault Application is an addon for an M-Files Vault which has been created using the [M-Files Vault Application Framework](https://developer.m-files.com/Frameworks/Vault-Application-Framework/).
The currently only supported way of logging is using the [SysUtils Event Log reporting helper](https://developer.m-files.com/Frameworks/Vault-Application-Framework/Helpers/SysUtils/#event-log-reporting).

The **Serilog.Sinks.MFilesSysUtilsEventLog** sink makes it possible to use Serilog *structured logging* in a Vault Application. For more information about Serilog, see [Serilog.net](https://serilog.net/) and [https://github.com/serilog/serilog](https://github.com/serilog/serilog).


```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.MFilesSysUtilsEventLogSink(restrictedToMinimumLevel: LogEventLevel.Information)
    .CreateLogger();

Log.Information("Hello from the Vault Application");
```

## Dependencies

The Serilog.Sinks.MFilesSysUtilsEventLog sink uses MFiles.VAF version 2.2.0.11 and Serilog 2.10.0

## How to add Serilog.Sinks.MFilesSysUtilsEventLog to your vault application

1. Open your Vault Application solution in Visual Studio and choose `Manage NuGet packages...`. Browse for package 'Serilog.Sinks.MFilesSysUtilsEventLog' and add it to your solution.
1. Add `using Serilog;` at the top of your application.
1. In your Vault Application class, override InitializeApplication(Vault vault) and add a logging configuration bulder, where you write to the `MFilesSysUtilsEventLogSink` (see below)
1. Add Serilog Log.xxx() statements in your use case code.



## Example use in a Vault Application

```csharp
using Serilog;

namespace DemoVaultApplication
{
    /// <summary>
    /// The entry point for this Vault Application Framework application.
    /// </summary>
    /// <remarks>Examples and further information available on the developer portal: http://developer.m-files.com/. </remarks>
    public class VaultApplication : ConfigurableVaultApplicationBase<Configuration>
    {

        protected override void InitializeApplication(Vault vault)
        {
            base.InitializeApplication(vault);

            // Configure logging
            Log.Logger = new LoggerConfiguration()
                .WriteTo.MFilesSysUtilsEventLogSink(restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();
        }


        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, ObjectType = (int)MFBuiltInObjectType.MFBuiltInObjectTypeDocument)]
        public void BeforeCheckInChangesFinalizeUpdateLogDemo(EventHandlerEnvironment env)
        {
            Log.Information("User {UserID} has checked in document {DisplayID} at {TimeStamp}", env.CurrentUserID, env.DisplayID, DateTime.Now);
        }
    }
}
```

When a user checks in a document in the vault where the Vault Application is installed. the Log.Information statement will produce a Windows Event log entry similarly to below:

```text
DemoVault {D449E438-89EE-42BB-9769-B862E9B1B140}
DemoVaultApp 0.1 (Process ID: 35700)
----
User 1 has checked in document 1 at 05/29/2021 23:04:00
```


## Cloud vault

Your Vault Application can be installed on either a local M-Files server or on a cloud vault. With the local server, you'll have access to the Windows Event log and the logging from your application.

A cloud vault is operated by the M-Files CloudOps team and you do not have direct access to the Windows Event log on the (Azure) server where the vault runs. If you want your Vault Application's logging, you'll have to request it from M-Files support. It's hardly ideal, but at least you're using structured logging with Serilog in your application now!

