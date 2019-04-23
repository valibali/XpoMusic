﻿using ModernSpotifyUWP.Classes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using static ModernSpotifyUWP.Helpers.ProxyHelper;

namespace ModernSpotifyUWP.Classes
{
    public static class LocalConfiguration
    {
        private static readonly Size compactOverlayDefaultSize = new Size(300, 300);

        public static Size CompactOverlaySize
        {
            get
            {
                var config = GetConfiguration("CompactOverlaySize");

                if (string.IsNullOrEmpty(config))
                    return compactOverlayDefaultSize;

                try
                {
                    var configParts = config.Split(';');
                    return new Size(double.Parse(configParts[0]), double.Parse(configParts[1]));
                }
                catch
                {
                    return compactOverlayDefaultSize;
                }
            }

            set
            {
                SetConfiguration("CompactOverlaySize", $"{value.Width};{value.Height}");
            }
        }

        public static Size WindowMinSize => new Size(500, 500);

        public static bool IsLoggedInByFacebook
        {
            get
            {
                return GetConfiguration("IsLoggedInByFacebook") == "1";
            }
            set
            {
                SetConfiguration("IsLoggedInByFacebook", value ? "1" : "0");
            }
        }

        public static bool IsCustomProxyEnabled
        {
            get
            {
                return GetConfiguration("IsCustomProxyEnabled") == "1";
            }
            set
            {
                SetConfiguration("IsCustomProxyEnabled", value ? "1" : "0");
            }
        }

        public static string CustomProxyAddress
        {
            get
            {
                return GetConfiguration("CustomProxyAddress");
            }
            set
            {
                SetConfiguration("CustomProxyAddress", value);
            }
        }

        public static string CustomProxyPort
        {
            get
            {
                return GetConfiguration("CustomProxyPort");
            }
            set
            {
                SetConfiguration("CustomProxyPort", value);
            }
        }

        public static ProxyType CustomProxyType
        {
            get
            {
                if (!int.TryParse(GetConfiguration("CustomProxyType"), out int type))
                    type = (int)ProxyType.HttpHttps;

                return (ProxyType)type;
            }
            set
            {
                SetConfiguration("CustomProxyType", ((int)value).ToString());
            }
        }

        public static Language Language
        {
            get
            {
                if (!int.TryParse(GetConfiguration("Language"), out int type))
                    type = (int)Language.Default;

                return (Language)type;
            }
            set
            {
                SetConfiguration("Language", ((int)value).ToString());
            }
        }

        public static Theme Theme
        {
            get
            {
                if (!int.TryParse(GetConfiguration("Theme"), out int type))
                    type = (int)Theme.Dark;

                return (Theme)type;
            }
            set
            {
                SetConfiguration("Theme", ((int)value).ToString());
            }
        }


        private static string GetConfiguration(string key)
        {
            var completeKey = "LocalConfiguration_" + key;
            if (!ApplicationData.Current.LocalSettings.Values.ContainsKey(completeKey))
                return null;

            return ApplicationData.Current.LocalSettings.Values[completeKey].ToString();
        }

        private static void SetConfiguration(string key, string value)
        {
            var completeKey = "LocalConfiguration_" + key;
            ApplicationData.Current.LocalSettings.Values[completeKey] = value;
        }
    }
}
