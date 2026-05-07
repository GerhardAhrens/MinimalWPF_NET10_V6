namespace MinimalWPF.Test
{
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows;

    [TestClass]
    public sealed class SingletonBase_Test : BaseTest
    {
        [TestInitialize]
        public void Initialize()
        {
            CultureInfo culture = new CultureInfo("de-DE");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        [TestMethod]
        public void Singleton_Create()
        {
            var config = ConfigurationManager.Instance;
            Assert.IsNotNull(config);
            Assert.IsNotEmpty(config.ApplicationName);
        }

        [TestMethod]
        public void Singleton_AreEqual()
        {
            var config1 = ConfigurationManager.Instance;
            var config2 = ConfigurationManager.Instance;
            Assert.IsNotNull(config1);
            Assert.IsNotNull(config2);
            Assert.AreEqual(config1, config2);
        }

        [TestMethod]
        public void Singleton_Reload()
        {
            var config = ConfigurationManager.Instance;
            Assert.IsNotNull(config);
            Assert.IsNotEmpty(config.ApplicationName);

            // Event abonnieren
            config.Reloaded += () =>
            {
                Trace("Reload Event aufgerufen");
            };

            config.Modus = "Init";
            Assert.AreEqual("Init", config.Modus);

            Thread.Sleep(1000);
            ConfigurationManager.ReloadInstance();
            Assert.AreEqual(string.Empty, config.Modus);
        }
    }

    public class ConfigurationManager : SingletonBase<ConfigurationManager>,
                                    ISingletonInitializable,
                                    ISingletonReloadable
    {
        protected ConfigurationManager()
        {
        }

        public string ApplicationName { get; private set; } = string.Empty;
        public DateTime LastReload { get; private set; }
        public string Modus { get; set; } = string.Empty;

        public void Initialize()
        {
            Trace.WriteLine("Initialisierung läuft...");

            // Beispielwerte erstellen
            this.LoadConfiguration();

            Trace.WriteLine("Initialisierung abgeschlossen");
        }

        public void ReloadContent()
        {
            Trace.WriteLine("Reload gestartet...");

            this.LoadConfiguration();

            Trace.WriteLine("Reload abgeschlossen");
        }

        public void Print()
        {
            Trace.WriteLine($"App: {this.ApplicationName}");
            Trace.WriteLine($"Startup: {this.LastReload}");
        }

        private void LoadConfiguration()
        {
            // Simuliert Laden aus Datei/DB/API
            ApplicationName = $"App geladen: {DateTime.Now:T}";
            LastReload = DateTime.Now;
            Modus = string.Empty;
        }
    }

}
