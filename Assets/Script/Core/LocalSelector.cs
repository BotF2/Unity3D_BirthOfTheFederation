using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;
using UnityEngine.Localization.Settings;

public class LocalSelector : MonoBehaviour
{
    private bool active = false; // press button only once
    public void ChangeLocale(int localeID)
      {
        if (active == true)
            return;
        StartCoroutine((IEnumerator)SetLocale(localeID));
        
    }
    IEnumerable SetLocale(int _locale)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index: _locale];
        active = false;

    }

}
