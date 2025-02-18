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
    [SerializeField] public ThemeSO CurrentTheme;
    [SerializeField] private Image imageBackground;
    [SerializeField] private Image spriteInsignia;
    [SerializeField] private Image spriteRace;
    [SerializeField] private Image spriteSystem;
    [SerializeField] private Image spriteFleetShip;
    [SerializeField] private Image spritePowerPlant;
    [SerializeField] private Image spriteFactory;
    [SerializeField] private Image spriteShipyard;
    [SerializeField] private Image spriteShields;
    [SerializeField] private Image spriteOrbitalBatteries;
    [SerializeField] private Image spriteResearchCenter;

    [SerializeField] private Font[] fonts;
    [SerializeField] private TMP_Text[] tMP_Texts;
    [SerializeField] private Button[] buttons;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        CurrentTheme = themeSOs[0];
    }

    public void ApplyTheme(ThemeEnum themeEnum)
    {
        switch (themeEnum)
        {
            case ThemeEnum.Fed:
                CurrentTheme = themeSOs[0];
                break;
            case ThemeEnum.Rom:
                CurrentTheme = themeSOs[1];
                break;
            case ThemeEnum.Kling:
                CurrentTheme = themeSOs[2];
                break;
            case ThemeEnum.Card:
                CurrentTheme = themeSOs[3];
                break;
            case ThemeEnum.Dom:
                CurrentTheme = themeSOs[4];
                break;
            case ThemeEnum.Borg:
                CurrentTheme = themeSOs[5];
                break;
            case ThemeEnum.Terran:
                CurrentTheme = themeSOs[6];
                break;
            default:
                CurrentTheme = themeSOs[7];
                break;
        }
        imageBackground.color = CurrentTheme.BackgroundColor;
        spriteInsignia.sprite = CurrentTheme.Insignia;
        spriteRace.sprite = CurrentTheme.RaceImage;
        spriteSystem.sprite = CurrentTheme.SystemImage;
        spriteFleetShip.sprite = CurrentTheme.FleetShipImage;
        spritePowerPlant.sprite = CurrentTheme.PowerPlantImage;
        spriteFactory.sprite = CurrentTheme.FactoryImage;
        spriteShipyard.sprite = CurrentTheme.ShipyardImage;
        spriteShields.sprite = CurrentTheme.ShieldImage;
        spriteOrbitalBatteries.sprite = CurrentTheme.OrbitalBatteriesImage;
        spriteResearchCenter.sprite = CurrentTheme.ResearchCenterImage;

        for (int i = 0; i < tMP_Texts.Length; i++)
        {
            tMP_Texts[i].color = CurrentTheme.TextColor;
            //text.font = CurrentTheme.Font;
        }

        // ToDo Apply to buttons
        for (int i = 0; i < buttons.Length; i++)
        {
            var buttonImage = buttons[1].GetComponent<UnityEngine.UI.Image>();
            if (buttonImage != null)
                buttonImage.sprite = CurrentTheme.ButtonSprite1;
        }
    }
    public ThemeSO GetLocalPlayerTheme()
    {
        return CurrentTheme;
    }
}

