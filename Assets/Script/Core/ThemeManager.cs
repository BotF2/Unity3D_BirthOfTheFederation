using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;
using TMPro;

public enum ThemeEnum
{
    Fed,
    Rom,
    Kling,
    Card,
    Dom,
    Borg,
    Terran
}
public class ThemeManager : MonoBehaviour
{
    public static ThemeManager Instance;
    [SerializeField] private ThemeSO[] themeSOs;
    [SerializeField] private ThemeSO currentTheme;
    [SerializeField] private UnityEngine.UI.Image[] imagesBackgrounds;
    [SerializeField] private Sprite[] spriteInsignias;
    [SerializeField] private Sprite[] spriteRaces;
    [SerializeField] private Color[] colors;
    [SerializeField] private TMP_Text[] tMP_Texts;
    //[SerializeField] private Text[] texts;
    [SerializeField] private UnityEngine.UI.Button[] buttons;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ApplyTheme(ThemeEnum themeEnum)
    {
        switch (themeEnum)
        {
            case ThemeEnum.Fed:
                currentTheme = themeSOs[0];
                break;
            case ThemeEnum.Rom:
                currentTheme = themeSOs[1];
                break;
            case ThemeEnum.Kling:
                currentTheme = themeSOs[2];
                break;
            case ThemeEnum.Card:
                currentTheme = themeSOs[3];
                break;
            case ThemeEnum.Dom:
                currentTheme = themeSOs[4];
                break;
            case ThemeEnum.Borg:
                currentTheme = themeSOs[5];
                break;
            case ThemeEnum.Terran:
                currentTheme = themeSOs[6];
                break;
            default:
                currentTheme = themeSOs[7];
                break;
        }


        // Apply to background
        if (imagesBackgrounds != null)
        {
            foreach (UnityEngine.UI.Image image in imagesBackgrounds)
            {
                image.color = currentTheme.BackgroundColor;
            }
        }
        if (spriteInsignias != null)
        {
            for (int i = 0; i < spriteInsignias.Length; i++) 
            {
                spriteInsignias[i] = currentTheme.Insignia;
            }
        }
        if (spriteRaces!= null)
        {
            for (int i = 0; i < spriteRaces.Length; i++)
            {
                spriteRaces[i] = currentTheme.Insignia;
            }
        }
        // Apply to texts
        for (int i = 0; i < tMP_Texts.Length; i++)
        {
            tMP_Texts[i].color = currentTheme.TMP_Text.color;
            tMP_Texts[i].font = currentTheme.TMP_Text.font;
            tMP_Texts[i].ForceMeshUpdate();
        }

        // Apply to buttons
        for (int i = 0; i < buttons.Length; i++)
        {

        //}
        ////foreach (var button in buttons)
        //{
            var buttonImage = buttons[1].GetComponent<UnityEngine.UI.Image>();
            if (buttonImage != null)
                buttonImage.sprite = currentTheme.ButtonSprite1;
        }
    }
}

