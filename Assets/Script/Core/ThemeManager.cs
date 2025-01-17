using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core;
using TMPro;
using UnityEngine.UI;

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
    [SerializeField] private Image imageBackground;
    [SerializeField] private Image spriteInsignia;
    [SerializeField] private Image spriteRace;
    [SerializeField] private Image spriteSystem;
    [SerializeField] private Image spriteFleetShip;
    //[SerializeField] private Color[] colors;
    [SerializeField] private Font[] fonts;
    [SerializeField] private TMP_Text[] tMP_Texts;
    [SerializeField] private Button[] buttons;

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
        //if (colors != null)
        //{
        //    for (int i = 0; i < colors.Length; i++)
        //    {
                imageBackground.color = currentTheme.BackgroundColor;
        //    }
        //}
        //if (spriteInsignias != null)
        //{
        //    for (int i = 0; i < spriteInsignias.Length; i++) 
        //    {
        //        spriteInsignias[i] = currentTheme.Insignia;
        //    }
        //}
        spriteInsignia.sprite = currentTheme.Insignia;
        //if (spriteRaces!= null)
        //{
        //    for (int i = 0; i < spriteRaces.Length; i++)
        //    {
        //        spriteRaces[i] = currentTheme.Insignia;
        //    }
        //}
        spriteRace.sprite = currentTheme.RaceImage;
        spriteSystem.sprite = currentTheme.SystemImage;
        spriteFleetShip.sprite = currentTheme.FleetShipImage  ;
        //if (spriteSystems != null)
        //{
        //    for (int i = 0; i < spriteSystems.Length; i++)
        //    {
        //        spriteSystems[i] = currentTheme.Insignia;
        //    }
        //}
        //if (spriteFleetShips != null)
        //{
        //    for (int i = 0; i < spriteFleetShips.Length; i++)
        //    {
        //        spriteFleetShips[i] = currentTheme.Insignia;
        //    }
        //}
        // Apply to texts
        for (int i = 0; i < tMP_Texts.Length; i++)
        {
            tMP_Texts[i].color = currentTheme.TextColor;
            //text.font = currentTheme.Font;
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

