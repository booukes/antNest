using System;
using System.Windows;
using Serilog;

namespace antNest
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Initialize Serilog for console only
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug() // logs everything debug and above
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Application starting...");

            // Catch unhandled exceptions
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                Log.Fatal(args.ExceptionObject as Exception, "Unhandled exception");
            };

            this.DispatcherUnhandledException += (sender, args) =>
            {
                Log.Fatal(args.Exception, "Dispatcher unhandled exception");
                args.Handled = true; // prevent crash
            };

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Log.Information("Application shutting down...");
            Log.CloseAndFlush(); // flush logs
            base.OnExit(e);
        }
    }
}
