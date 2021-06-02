using System;
using System.Reflection;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration.AdminConfigurations;
using MFiles.VAF.Core;
using MFilesAPI;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace SampleVaultApplication
{
    /// <summary>
    /// The entry point for this Vault Application Framework application.
    /// </summary>
    /// <remarks>Examples and further information available on the developer portal: http://developer.m-files.com/. </remarks>
    public class VaultApplication : ConfigurableVaultApplicationBase<Configuration>
    {
        private readonly LoggingLevelSwitch _loggingLevelSwitch = new LoggingLevelSwitch(LogEventLevel.Information);
        private readonly string _buildFileVersion               = ((AssemblyFileVersionAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyFileVersionAttribute), false)).Version;

        protected override void InitializeApplication(Vault vault)
        {
            base.InitializeApplication(vault);

            ConfigureApplication(Configuration);

            var thisVaultApp = vault.CustomApplicationManagementOperations.GetCustomApplication("45472745-9d80-4b42-b092-463f3c38b6f1"); // From appdef.xml

            // And log some information about this VaultApp
            Log.Information("Starting up VaultApp {VaultAppName} build version {VaultAppBuildVersion} in vault {VaultName}", thisVaultApp.Name, _buildFileVersion, vault.Name);
        }

        // ===========================================================================================================================================================
        // Logging configuration and settings

        /// <summary>
        /// Configure logging in the vault application, even create structure if necessary.
        /// </summary>
        /// <param name="vault"></param>
        public void ConfigureApplication(Configuration configuration)
        {
            // Initialize the _loggingLevelSwitch from configuration
            ConfigureLoggingLevelSwitch(configuration.LogLevel);

            // Configure logging
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(_loggingLevelSwitch)
                .WriteTo.MFilesSysUtilsEventLogSink()
                .CreateLogger();
        }

        private void ConfigureLoggingLevelSwitch(string logLevel)
        {
            switch(logLevel)
            {
                case "OFF":     _loggingLevelSwitch.MinimumLevel = ((LogEventLevel) 1 + (int) LogEventLevel.Fatal);     break;  // https://stackoverflow.com/questions/30849166/how-to-turn-off-serilog
                case "INFO":    _loggingLevelSwitch.MinimumLevel = LogEventLevel.Information;                           break;
                case "WARNING": _loggingLevelSwitch.MinimumLevel = LogEventLevel.Warning;                               break;
                case "ERROR":   _loggingLevelSwitch.MinimumLevel = LogEventLevel.Error;                                 break;
                default:        _loggingLevelSwitch.MinimumLevel = LogEventLevel.Information;                           break;
            }
        }


        /// <summary>
        /// Update the Serilog loggingLevelSwitch, when the LogLevel configuration for the Vault Application is changed in M-Files Admin.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="clientOps"></param>
        /// <param name="oldConfiguration"></param>
        protected override void OnConfigurationUpdated(IConfigurationRequestContext context, ClientOperations clientOps, Configuration oldConfiguration)
        {
            if (oldConfiguration.LogLevel != Configuration.LogLevel)
            {
                ConfigureLoggingLevelSwitch(Configuration.LogLevel);
            }

            Log.Information("Log level changed to {LogLevel}", Configuration.LogLevel);
        }



        /// <summary>
        /// Sample Event that fired upon checkin of a Builtin Document object type.
        /// It logs the event using Serilog.
        /// </summary>
        /// <param name="env"></param>
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, ObjectType = (int)MFBuiltInObjectType.MFBuiltInObjectTypeDocument)]
        public void BeforeCheckInChangesFinalizeUpdateLogDemo(EventHandlerEnvironment env)
        {
            Log.Information("User {UserID} has checked in document {DisplayID} at {TimeStamp}", env.CurrentUserID, env.DisplayID, DateTime.Now);
        }
    }
}