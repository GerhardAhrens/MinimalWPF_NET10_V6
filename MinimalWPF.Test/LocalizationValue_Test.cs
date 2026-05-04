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
        }
    }
}
