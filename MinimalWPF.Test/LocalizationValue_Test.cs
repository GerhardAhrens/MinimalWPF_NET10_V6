namespace MinimalWPF.Test
{
    using System.Globalization;
    using System.Windows;

    [TestClass]
    public sealed class LocalizationValue_Test
    {
        private const string DICTIONARYNAME = "Resources\\Localization\\Localization.xaml";
        private static ResourceDictionary resourceDict;

        [TestInitialize]
        public void Initialize()
        {
            CultureInfo culture = new CultureInfo("de-DE");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        [TestMethod]
        public void TestResourceDictionaryLoading()
        {
            // 1. Pack-URI erstellen
            // Format: pack://application:,,,/AssemblyName;component/PathTo/File.xaml
            Uri resourceUri = new Uri($"/MinimalWPF.Test;component/{DICTIONARYNAME}", UriKind.Relative);

            // 2. Dictionary laden
            resourceDict = (ResourceDictionary)Application.LoadComponent(resourceUri);

            // 3. Auf eine Ressource zugreifen
            string dicValue = resourceDict["WindowsTitelZeile"] as string;

            // 4. Assertion
            Assert.IsNotNull(dicValue, "Resource konnte nicht gefunden werden.");
        }

        [TestMethod]
        public void GetStringWithoutParameter()
        {
            string stringValue = LocalizationValue.Get("WindowsTitelZeile");
            
            Assert.IsNotNull(stringValue, "Minimal WPF Template NET 10; V6");
        }

        [TestMethod]
        public void GetStringWithParameter()
        {
            string stringValue = LocalizationValue.Get("MessageExit_Text_DE", "Parameter");

            Assert.IsNotNull(stringValue, "Wollen Sie die Anwendung beenden? (Parameter)");
        }

        [TestMethod]
        public void GetInteger()
        {
            int intValue = LocalizationValue.Get<int>("EineZahl");

            Assert.AreEqual(42, intValue);
        }

        [TestMethod]
        public void GetDateTime()
        {
            DateTime dateTimeValue = LocalizationValue.Get<DateTime>("EinDatumMitZeit");

            Assert.AreEqual(new DateTime(2026,5,4,14,45,00), dateTimeValue);
        }
    }
}
