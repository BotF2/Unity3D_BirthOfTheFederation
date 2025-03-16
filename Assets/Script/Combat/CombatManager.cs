using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;
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
    public void InstatniateCombat(List<ShipController> sideOneShips, List<ShipController> sideTwoShips)
    { // call from diplomacy total war or from Diplomacy UI on fleet meets fleet 

    }
}
