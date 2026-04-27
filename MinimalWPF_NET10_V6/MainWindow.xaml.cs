//-----------------------------------------------------------------------
// <copyright file="MainWindow.cs" company="Lifeprojects.de">
//     Class: MainWindow
//     Copyright © Lifeprojects.de 2026
// </copyright>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>05.03.2026 18:21:36</date>
//
// <summary>
// WPF Template mit Minimalfunktionen
// </summary>
//-----------------------------------------------------------------------

namespace MinimalWPF
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowBase
    {
        public MainWindow()
        {
            this.InitializeComponent();
            WeakEventManager<WindowBase, RoutedEventArgs>.AddHandler(this, "Loaded", this.OnLoaded);
            WeakEventManager<WindowBase, CancelEventArgs>.AddHandler(this, "Closing", this.OnWindowClosing);

            this.SetVectorIcon("AppIcon2", 64);

            this.QuitCommand = new CommandBase(this.OnQuit, () => true);
            this.QuitParamCommand = new CommandBase(() => this.OnQuit("Argument"));
            this.StartCommand = new CommandBase(OnStart);
            this.InformationCommand = new CommandBase(OnInformationPopup);
            this.SettingsCommand = new CommandBase(OnSettingsPopup);
            this.CloseInformationPopupCommand = new CommandBase(OnCloseInformation);
            this.CloseSettingsPopupCommand = new CommandBase(OnCloseSettingsPopup);

            this.WindowTitel = LocalizationString.Get("WindowsTitelZeile");
            this.ApplikationVersion = base.ApplicationVersion.ToString();
            this.LaufzeitVersion = base.RuntimeVersion;
            this.WinVersion = base.WindowsVersion;
            this.DataContext = this;
        }

        public CommandBase QuitCommand { get; private set; }
        public CommandBase QuitParamCommand { get; private set; }
        public CommandBase StartCommand { get; private set; }
        public CommandBase InformationCommand { get; private set; }
        public CommandBase SettingsCommand { get; private set; }
        public CommandBase CloseInformationPopupCommand { get; private set; }
        public CommandBase CloseSettingsPopupCommand { get; private set; }

        public string WindowTitel
        {
            get => base.GetValue<string>();
            set => base.SetValue(value);
        }

        public string ApplikationVersion
        {
            get => base.GetValue<string>();
            set => base.SetValue(value);
        }

        public string LaufzeitVersion
        {
            get => base.GetValue<string>();
            set => base.SetValue(value);
        }

        public string WinVersion
        {
            get => base.GetValue<string>();
            set => base.SetValue(value);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            StatusbarMain.Statusbar.DatabaseInfo = "Keine";
            StatusbarMain.Statusbar.DatabaseInfoTooltip = "Keine Datenbank verbunden";
            StatusbarMain.Statusbar.Notification = "Bereit";
        }

        private void OnCloseApplication(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnQuit()
        {
            this.Tag = null;
            this.Close();
        }

        private void OnStart()
        {
            this.QuitParamCommand.TryExecute();
        }

        private void OnQuit(string param)
        {
            this.Tag = param;
            this.Close();
        }

        private void OnInformationPopup()
        {
            this.InformationPopup.SetValue(MaskLayerBehavior.IsOpenProperty, true);
        }

        private void OnCloseInformation()
        {
            this.InformationPopup.SetValue(MaskLayerBehavior.IsOpenProperty, false);
        }

        private void OnSettingsPopup()
        {
            this.SettingsPopup.SetValue(MaskLayerBehavior.IsOpenProperty, true);
        }

        private void OnCloseSettingsPopup()
        {
            this.SettingsPopup.SetValue(MaskLayerBehavior.IsOpenProperty, false);
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = false;

            MessageBoxResult msgYN;
            if (this.Tag != null)
            {
                msgYN = MessageBox.Show($"Wollen Sie die Anwendung beenden? ({this.Tag})", LocalizationString.Get("MessageExit_Titel_DE"), MessageBoxButton.YesNo, MessageBoxImage.Question);
            }
            else
            {
                msgYN = MessageBox.Show("Wollen Sie die Anwendung beenden?", LocalizationString.Get("MessageExit_Titel_DE"), MessageBoxButton.YesNo, MessageBoxImage.Question);
            }

            if (msgYN == MessageBoxResult.Yes)
            {
                App.ApplicationExit();
            }
            else
            {
                e.Cancel = true;
            }
        }
    }

    public static class WpfIconHelper
    {
        public static ImageSource CreateIcon(DrawingImage drawingImage, int size = 32, double dpi = 96)
        {
            if (size.In(32, 48, 64) == false)
            {
                throw new ArgumentOutOfRangeException(nameof(size), "Der Wert für die Icon Größe muß 32 oder 64 Pixel sein.");
            }

            if (dpi == 96)
            {
                size = 64;
            }
            else if (dpi == 144)
            {
                size = 48;
            }
            else if (dpi == 192)
            {
                size = 64;
            }

            DrawingVisual visual = new DrawingVisual();
            using (var dc = visual.RenderOpen())
            {
                dc.DrawImage(drawingImage, new Rect(0, 0, size, size));
            }

            RenderTargetBitmap bitmap = new RenderTargetBitmap(size, size, dpi, dpi, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            bitmap.Freeze(); // Performance + Thread-Safety

            return bitmap;
        }

        public static void ApplyIcon(Window window, DrawingImage drawingImage, int size = 32)
        {
            window.Icon = CreateIcon(drawingImage, size);
        }
    }

    public static class IntegerExtensions
    {
        // Die 'this'-Anweisung vor 'int value' definiert den Typ, der erweitert wird
        public static bool In(this int value, params int[] allowedValues)
        {
            // Prüft, ob 'value' in der Menge 'allowedValues' enthalten ist
            return allowedValues.Contains(value);
        }

        public static bool NotIn(this int value, params int[] allowedValues)
        {
            // Prüft, ob 'value' in der Menge 'allowedValues' nicht enthalten ist
            return !allowedValues.Contains(value);
        }
    }

    public static class WindowExtensions
    {
        public static void SetVectorIcon(this Window window, string resourceKey, int size = 32, double dpi = 96)
        {
            if (size.In(32, 48, 64) == false)
            {
                throw new ArgumentOutOfRangeException(nameof(size), "Der Wert für die Icon Größe muß 32 oder 64 Pixel sein.");
            }

            DrawingImage drawing = (DrawingImage)window.TryFindResource(resourceKey);
            if (drawing != null)
            {
                window.Icon = WpfIconHelper.CreateIcon(drawing, size, dpi);
            }
            else
            {
                throw new ArgumentException($"Die Ressource mit dem Schlüssel '{resourceKey}' wurde nicht gefunden oder ist kein DrawingImage.", nameof(resourceKey));
            }
        }
    }

}