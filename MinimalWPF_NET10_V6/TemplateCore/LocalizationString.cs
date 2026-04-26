//-----------------------------------------------------------------------
// <copyright file="ResourcesText.cs" company="Lifeprojects.de">
//     Class: ResourcesText
//     Copyright © Lifeprojects.de 2023
// </copyright>
//
// <Framework>4.8</Framework>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>15.01.2023 09:01:02</date>
//
// <summary>
// Klasse für 
// </summary>
//-----------------------------------------------------------------------

namespace System.Windows
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.Versioning;

    [SupportedOSPlatform("windows")]
    public class LocalizationString
    {
        private const string DICTIONARYNAME = "Resources\\Localization\\Localization.xaml";
        private static ResourceDictionary resourceDict;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcesText"/> class.
        /// </summary>
        static LocalizationString()
        {
            // Resources\Localization\Localization.xaml
            resourceDict = Application.Current.Resources.MergedDictionaries.Where(md => md.Source.OriginalString.EndsWith(DICTIONARYNAME,StringComparison.CurrentCulture)).FirstOrDefault();
        }

        public LocalizationString()
        {
        }

        public static int Count { get { return resourceDict.Count; } }

        public static IEnumerable<string> Keys { get { return resourceDict.Keys.Cast<string>().Select(s => s); } }

        public static Dictionary<string,string> KeyValue {
            get
            {
                return resourceDict.Keys.Cast<string>().ToDictionary
                    (x => x,                             // Key selector
                     x => (string)resourceDict[x]        // Value selector
                     );
            } 
        }

        public static string Get(string key)
        {
            ArgumentNullException.ThrowIfNull(key);

            if (resourceDict == null)
            {
                return $"ResourceDictionary 'TextString.xaml' nicht gefunden.";
            }

            bool keyFound = resourceDict.Cast<DictionaryEntry>().Any(f => f.Key.ToString().Equals(key, StringComparison.OrdinalIgnoreCase));
            if (keyFound == false)
            {
                return $"ResourceKey '{key}' nicht gefunden.";
            }

            string value = resourceDict.Cast<DictionaryEntry>().FirstOrDefault(f => f.Key.ToString().Equals(key, StringComparison.OrdinalIgnoreCase)).Value.ToString();

            return value;
        }

        public static string Get(string key, params object[] args)
        {
            ArgumentNullException.ThrowIfNull(key);

            if (resourceDict == null)
            {
                return $"ResourceDictionary 'TextString.xaml' nicht gefunden.";
            }

            bool keyFound = resourceDict.Cast<DictionaryEntry>().Any(f => f.Key.ToString().Equals(key, StringComparison.OrdinalIgnoreCase));
            if (keyFound == false)
            {
                return $"ResourceKey '{key}' nicht gefunden.";
            }

            string value = resourceDict.Cast<DictionaryEntry>().FirstOrDefault(f => f.Key.ToString().Equals(key, StringComparison.OrdinalIgnoreCase)).Value.ToString();

            return string.Format(CultureInfo.CurrentCulture, value, args);
        }

        public static void SetResources(string resourceFile)
        {
            try
            {
                resourceDict = Application.Current.Resources.MergedDictionaries.FirstOrDefault(md => md.Source.OriginalString.EndsWith(resourceFile.Replace(@"\", "/"), StringComparison.CurrentCulture));
                if (resourceDict == null)
                {
                    throw new NotSupportedException($"Die Resource Datei '{resourceFile}' konnte nicht in der Resources Liste gefunden werden");
                }
            }
            catch (Exception ex)
            {
                throw new NotSupportedException($"Die Resource Datei '{resourceFile}' konnte nicht gefunden werden", ex);
            }
        }
    }
}
