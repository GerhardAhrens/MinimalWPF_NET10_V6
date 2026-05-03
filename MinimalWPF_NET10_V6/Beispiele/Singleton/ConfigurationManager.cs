namespace MinimalWPF.Beispiele
{
    using System;
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
            Console.WriteLine("Initialisierung läuft...");

            // Beispielwerte laden
            ApplicationName = "Meine NET 10 Anwendung";
            StartupTime = DateTime.Now;

            Console.WriteLine("Initialisierung abgeschlossen");
        }

        public void Print()
        {
            Console.WriteLine($"App: {ApplicationName}");
            Console.WriteLine($"Startup: {StartupTime}");
        }
    }
}
