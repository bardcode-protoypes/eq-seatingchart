using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocalizationManager : MonoBehaviour
{
    private const string PlayerPrefsKey = "selectedLocale";

    public delegate void LocaleChanged();
    public static event LocaleChanged OnLocaleChanged;

    private void Awake()
    {
        // Load persisted locale or default
        string savedCode = PlayerPrefs.GetString(PlayerPrefsKey, string.Empty);

        if (!string.IsNullOrEmpty(savedCode))
        {
            SetLocale(savedCode);
        }
        else
        {
            // Default to first available locale if nothing saved
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
        }
    }

    public void SetLocale(string localeCode)
    {
        foreach (Locale locale in LocalizationSettings.AvailableLocales.Locales)
        {
            if (locale.Identifier.Code == localeCode)
            {
                LocalizationSettings.SelectedLocale = locale;
                PlayerPrefs.SetString(PlayerPrefsKey, localeCode);
                PlayerPrefs.Save();
                Debug.Log($"Language set to: {locale.Identifier.CultureInfo.NativeName}");
                OnLocaleChanged?.Invoke();
                return;
            }
        }

        Debug.LogWarning($"Locale code '{localeCode}' not found in available locales.");
    }

    public void SetLocaleByName(string localeName)
    {
        foreach (Locale locale in LocalizationSettings.AvailableLocales.Locales)
        {
            if (locale.Identifier.CultureInfo.NativeName == localeName)
            {
                SetLocale(locale.Identifier.Code);
                return;
            }
        }

        Debug.LogWarning($"Locale name '{localeName}' not found in available locales.");
    }

    public string[] GetAvailableLocaleNames()
    {
        var locales = LocalizationSettings.AvailableLocales.Locales;
        string[] names = new string[locales.Count];

        for (int i = 0; i < locales.Count; i++)
            names[i] = locales[i].Identifier.CultureInfo.NativeName;

        return names;
    }

    public string GetCurrentLocaleName()
    {
        return LocalizationSettings.SelectedLocale != null
            ? LocalizationSettings.SelectedLocale.Identifier.CultureInfo.NativeName
            : string.Empty;
    }
}
