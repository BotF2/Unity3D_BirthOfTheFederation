using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Core;

public class BuildListUIManager : MonoBehaviour
{
    public static BuildListUIManager Instance { get; private set; }
    [SerializeField]
    private GameObject shipItemPrefab;
    [SerializeField]
    private GameObject factoryItemPrefab;
    [SerializeField]
    private Image powerPlant;
    [SerializeField]
    private Image powerPlantBGroud;
    [SerializeField]
    private Image factory;
    [SerializeField]
    private Image factoryBGround;
    [SerializeField]
    private Image shipyard;
    [SerializeField]
    private Image shipyardBGround;
    [SerializeField]
    private Image shield;
    [SerializeField]
    private Image shieldBGround;
    [SerializeField]
    private Image orbital;
    [SerializeField]
    private Image orbitalBGround;
    [SerializeField]
    private Image researchCenter;
    [SerializeField]
    private Image researchCenterBGround;
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
    public void SetFacilityImage(ThemeSO theme)
    {
        powerPlant.sprite = theme.PowerPlantImage;
        powerPlantBGroud.sprite = theme.PowerPlantImage;

        factory.sprite = theme.FactoryImage;
        factoryBGround.sprite = theme.FactoryImage;
        
        shipyard.sprite = theme.ShipyardImage;
        shipyardBGround.sprite = theme.ShipyardImage;
  
        shield.sprite = theme.ShieldImage;
        shieldBGround.sprite = theme.ShieldImage;

        orbital.sprite = theme.OrbitalBatteriesImage;
        orbitalBGround.sprite = theme.OrbitalBatteriesImage;

        researchCenter.sprite = theme.ResearchCenterImage;
        researchCenterBGround.sprite = theme.ResearchCenterImage;
    }
}
