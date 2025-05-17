using UnityEngine;
using Assets.Core;
using TMPro;


[CreateAssetMenu(fileName = "ThemeSO", menuName = "ThemeSO")]
/// <summary>
/// This is a type of UI menu theme based on the style, color scheme and design of the faction/civilization (Federation, Klingon...) selected by the local player
/// </summary>
public class ThemeSO: ScriptableObject
{
    public int CivInt;
    public Sprite BackImage;
    public Sprite Insignia;
    public Sprite RaceImage;
    public Sprite SystemImage;
    public Sprite FleetShipImage;
    public Sprite PowerPlantImage;
    public Sprite FactoryImage;
    public Sprite ShipyardImage;
    public Sprite ShieldImage;
    public Sprite OrbitalBatteriesImage;
    public Sprite ResearchCenterImage;
    public Color BackgroundColor;
    public Color ForegroundColor;
    public Color BoarderColor;
    public Color HighLightColor;
    public Color LowLightColor;
    public Color TextColor;
    public TMP_Text TMP_Text;
    public Sprite ButtonSprite1;
    public Sprite ButtonSprite2;
    public Sprite ButtonSprite3;
    public Sprite ButtonSprite4;
}
