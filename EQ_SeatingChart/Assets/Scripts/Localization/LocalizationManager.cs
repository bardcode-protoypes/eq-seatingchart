using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LocalizationManager : MonoBehaviour
{
    private const string PlayerPrefsKey = "selectedLocale";
    public static LocalizationManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        StartCoroutine(this.Initialize());

    }
    private IEnumerator Initialize()
    {
        // ensure LocalizationSettings is ready
        yield return new WaitForEndOfFrame();
        
        // Wait for localization to initialize
        var initOperation = LocalizationSettings.InitializationOperation;
        if (!initOperation.IsValid())
        {
            Debug.LogError("LocalizationSettings.InitializationOperation is not valid. Check LocalizationSettings setup.");
            yield break;
        }
        
        yield return initOperation;

        if (initOperation.Status == AsyncOperationStatus.Succeeded)
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
        else
        {
            Debug.LogError("Failed to initialize localization!");
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
