namespace MinimalWPF.Beispiele
{
    using System;
    using System.Diagnostics;
    using System.Windows;

    public class ConfigurationManager : SingletonBase<ConfigurationManager>, ISingletonInitializable
    {
        protected ConfigurationManager()
        {
        }

        public string ApplicationName { get; private set; } = string.Empty;
        public DateTime StartupTime { get; private set; }

        public void Initialize()
        {
            Debug.WriteLine("Initialisierung läuft...");

            // Beispielwerte erstellen
            this.ApplicationName = "Meine NET 10 Anwendung";
            this.StartupTime = DateTime.Now;

            Debug.WriteLine("Initialisierung abgeschlossen");
        }

        public void Print()
        {
            Debug.WriteLine($"App: {this.ApplicationName}");
            Debug.WriteLine($"Startup: {this.StartupTime}");
        }
    }
}
