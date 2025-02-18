using System.Collections;
using System.Collections.Generic;
using TMPro;
using Assets.Core;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class BuildListUIController : MonoBehaviour
{
    public static BuildListUIController Instance;

    public int MaxProgress;
    public int CurrentProgress;
    public Image Mask;
    //[SerializeField]
    //private List<GameObject> listOfSysUIsinCanavasBuildList; 
    //[SerializeField]
    //private Image powerPlant;
    //[SerializeField]
    //private Image powerPlantBGroud;
    //[SerializeField]
    //private Image factory;
    //[SerializeField]
    //private Image factoryBGround;
    //[SerializeField]
    //private Image shipyard;
    //[SerializeField]
    //private Image shipyardBGround;
    //[SerializeField]
    //private Image shield;
    //[SerializeField]
    //private Image shieldBGround;
    //[SerializeField]
    //private Image orbital;
    //[SerializeField]
    //private Image orbitalBGround;
    //[SerializeField]
    //private Image researchCenter;
    //[SerializeField]
    //private Image researchCenterBGround;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void Update()
    {
        GetCurrentFill();
    }

    private void GetCurrentFill()
    {
        float fillAmount = (float)CurrentProgress / (float)MaxProgress;
        Mask.fillAmount = fillAmount;
    }
    //public void SetFacilityImages(ThemeSO theme)
    //{
    //    powerPlant.sprite = theme.PowerPlantImage;
    //    powerPlantBGroud.sprite = theme.PowerPlantImage;

    //    factory.sprite = theme.FactoryImage;
    //    factoryBGround.sprite = theme.FactoryImage;

    //    shipyard.sprite = theme.ShipyardImage;
    //    shipyardBGround.sprite = theme.ShipyardImage;

    //    shield.sprite = theme.ShieldImage;
    //    shieldBGround.sprite = theme.ShieldImage;

    //    orbital.sprite = theme.OrbitalBatteriesImage;
    //    orbitalBGround.sprite = theme.OrbitalBatteriesImage;

    //    researchCenter.sprite = theme.ResearchCenterImage;
    //    researchCenterBGround.sprite = theme.ResearchCenterImage;
    //}
}

