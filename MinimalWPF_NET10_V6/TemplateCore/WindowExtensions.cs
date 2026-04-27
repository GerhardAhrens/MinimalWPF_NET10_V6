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

namespace System.Windows
{
    using System.Windows.Media;

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