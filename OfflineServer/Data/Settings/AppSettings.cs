﻿using MahApps.Metro;
using MahApps.Metro.Controls.Dialogs;
using OfflineServer.Servers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace OfflineServer.Data.Settings
{
    public class AppSettings : ObservableObject
    {
        public UISettings uiSettings { get; set; }
        public AppSettings()
        {
            uiSettings = new UISettings();
        }

        public void saveInstance()
        {
            File.WriteAllText(DataEx.xml_Settings, this.SerializeObject(), Encoding.UTF8);
        }

        public class UISettings : ObservableObject
        {
            private Accent accent;
            private AppTheme theme;

            public Boolean haveSeenCustomAccentSampleMessage = false;
            public Boolean haveSeenCustomThemeSampleMessage = false;

            [XmlIgnore]
            public List<String> list_Accents { get; set; } = new List<String>();
            [XmlIgnore]
            public List<String> list_Themes { get; set; } = new List<String>();
            public String Accent
            {
                get
                {
                    return accent.Name;
                }
                set
                {
                    if (!String.IsNullOrWhiteSpace(value))
                    {
                        if (Access.mainWindow != null && value == "CustomAccentSample" && !haveSeenCustomAccentSampleMessage)
                        {
                            Access.mainWindow.informUser("Sample custom accent", "Did you know that you can add or create infinite amounts of custom accents? Well, now you do! Go to " + DataEx.dir_Accents + " and get creative!");
                            haveSeenCustomAccentSampleMessage = true;
                        }
                        accent = ThemeManager.GetAccent(value);
                        if (theme != null) applyNewStyle();
                        RaisePropertyChangedEvent("Accent");
                    }
                }
            }
            public String Theme
            {
                get
                {
                    return theme.Name;
                }
                set
                {
                    if (!String.IsNullOrWhiteSpace(value))
                    {
                        if (Access.mainWindow != null && value == "CustomThemeSample" && !haveSeenCustomThemeSampleMessage)
                        {
                            Access.mainWindow.informUser("Sample custom theme", "Just like the accents, you can add or create infinite amounts of custom themes as well! Go to " + DataEx.dir_Themes + " and go crazy with it!");
                            haveSeenCustomThemeSampleMessage = true;
                        }
                        theme = ThemeManager.GetAppTheme(value);
                        if (accent != null) applyNewStyle();
                        Application.Current.Resources["ThemeImageColor"] = new SolidColorBrush(value == "BaseDark" ? Color.FromArgb(255, 80, 80, 80) : Color.FromArgb(255, 0, 0, 0));
                        RaisePropertyChangedEvent("Theme");
                    }
                }
            }

            public void doDefault()
            {
                accent = ThemeManager.GetAccent("Steel");
                theme = ThemeManager.GetAppTheme("BaseDark");
                RaisePropertyChangedEvent("Accent");
                RaisePropertyChangedEvent("Theme");
                applyNewStyle();
            }

            public void applyNewStyle()
            {
                ThemeManager.ChangeAppStyle(Application.Current, accent, theme);
                if (Access.dataAccess != null) Access.dataAccess.appSettings.saveInstance();
            }

            public UISettings()
            {
                foreach (String accentXaml in Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, DataEx.dir_Accents), "*.xaml", SearchOption.TopDirectoryOnly))
                {
                    String accentName = accentXaml.Replace(Path.Combine(Environment.CurrentDirectory, DataEx.dir_Accents), String.Empty).Replace(".xaml", String.Empty);

                    ThemeManager.AddAccent(accentName, new Uri(accentXaml, UriKind.Absolute));
                    list_Accents.Add(accentName);
                }

                foreach (String themeXaml in Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, DataEx.dir_Themes), "*.xaml", SearchOption.TopDirectoryOnly))
                {
                    String themeName = themeXaml.Replace(Path.Combine(Environment.CurrentDirectory, DataEx.dir_Themes), String.Empty).Replace(".xaml", String.Empty);

                    ThemeManager.AddAppTheme(themeName, new Uri(themeXaml, UriKind.Absolute));
                    list_Themes.Add(themeName);
                }
            }
        }
    }
}