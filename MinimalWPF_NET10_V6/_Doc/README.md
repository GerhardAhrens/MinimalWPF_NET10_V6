# MinimalWPF Projekt Template

![NET](https://img.shields.io/badge/NET-10.0-green.svg)
![License](https://img.shields.io/badge/License-MIT-blue.svg)
![VS2026](https://img.shields.io/badge/Visual%20Studio-2026-white.svg)
![Version](https://img.shields.io/badge/Version-1.0.2026.0-yellow.svg)

Dieses Projekt ist ein einfaches WPF-Projekt Template für .NET 10.0, das die grundlegenden Komponenten für den Start einer WPF-Anwendung enthält. Es ist ideal für Entwickler, die schnell mit der Entwicklung von WPF-Anwendungen beginnen möchten.\
<img src="MainWindow.png" style="width:650px;"/>


Das Template soll als Grundlage für die Entwicklung von WPF-Anwendungen dienen und enthält bereits einige nützliche Funktionen und Strukturen, die in vielen Anwendungen benötigt werden. Es ist so konzipiert, dass es leicht an die spezifischen Anforderungen eines Projekts angepasst werden kann.

Hauptaufgabe des Templates ist es, eine solide Basis für die Entwicklung von WPF-Anwendungen als Prototyp zu bieten, damit Entwickler sich auf die Implementierung der spezifischen Funktionen ihrer Anwendung konzentrieren können, anstatt Zeit mit der Einrichtung der grundlegenden Struktur zu verbringen.

# Features des Template
- Inside MVVM (durch WindowsBase Klasse)
- Dialog Popup
- IconButton
- Localization
- Settings in JSON File (durch SmartSettingsBase)

Alle Klassen für das Template sind unter dem Namespace `System.Windows` organisiert.

## WindowBase

Die Klasse WindowBase ist eine benutzerdefinierte Basisklasse für Fenster in der WPF-Anwendung. Sie bietet grundlegende Funktionalitäten und Eigenschaften, die von allen Fenstern in der Anwendung geerbt werden können. Dadurch wird die Wiederverwendbarkeit von Code erhöht und die Wartbarkeit der Anwendung verbessert.
```xml
<base:WindowBase
    x:Class="MinimalWPF.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:base="clr-namespace:System.Windows"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:local="clr-namespace:MinimalWPF"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    Title="{Binding Path=WindowTitel, FallbackValue=~WindowTitel}"
    Width="900"
    Height="600"
    Icon="{StaticResource ResourceKey=IconCustomA2}"
    mc:Ignorable="d">

    <Grid x:Name="gridMain">

    </Grid>
</base:WindowBase>
```
## Binding Properties

```csharp
public string WindowTitel
{
    get => base.GetValue<string>();
    set => base.SetValue(value);
}
```

## CommandBase

Die CommmandBase Klasse ist eine benutzerdefinierte Implementierung des ICommand-Interfaces, die es ermöglicht, Befehle in der WPF-Anwendung zu erstellen und zu verwenden. Sie bietet eine einfache Möglichkeit, Aktionen an Benutzeroberflächenelemente zu binden, wie z.B. Schaltflächen oder Menüs.
```csharp
public MainWindow()
{
    this.QuitCommand = new CommandBase(this.OnQuit);
}

public CommandBase QuitCommand { get; private set; }

private void OnQuit()
{
    this.Tag = null;
    this.Close();
}
```
## Settings
```csharp
```


## StatusbarMain

Die statische Klasse StatusbarMain bietet eine zentrale Anlaufstelle für die Verwaltung der Statusleiste in der WPF-Anwendung. Sie enthält eine statische Instanz der Statusbar, die von verschiedenen Teilen der Anwendung verwendet werden kann, um Informationen anzuzeigen oder zu aktualisieren.
```xml
<!--#region Statuszeile-->
<StatusBar
    Grid.Row="2"
    Height="Auto"
    Background="Transparent"
    BorderBrush="DarkGray"
    BorderThickness="2"
    DataContext="StatusMain"
    FontSize="13">

    <StatusBarItem DockPanel.Dock="Left">
        <StackPanel Orientation="Horizontal">
            <Label Content="{StaticResource ResourceKey=IconStatusbarUser}" />
            <TextBlock
                Margin="5,0,0,0"
                VerticalAlignment="Center"
                Text="{Binding Path=CurrentUser, Source={x:Static base:StatusbarMain.Statusbar}}" />
        </StackPanel>
    </StatusBarItem>
</StatusBar>
<!--#endregion Statuszeile-->
```

Ändern eines Eintrags in der Statusleiste:
```csharp
StatusbarMain.Statusbar.DatabaseInfo = "Keine";
StatusbarMain.Statusbar.DatabaseInfoTooltip = "Keine Datenbank verbunden";
StatusbarMain.Statusbar.Notification = "Bereit";
```
# Versionshistorie
![Version](https://img.shields.io/badge/Version-1.0.2026.6-yellow.svg)
- Weitere Basis Klassen für das Template
  * Settings
  * DialogPopup
  * IconButton

![Version](https://img.shields.io/badge/Version-1.0.2026.2-yellow.svg)
- Migration auf NET 10
