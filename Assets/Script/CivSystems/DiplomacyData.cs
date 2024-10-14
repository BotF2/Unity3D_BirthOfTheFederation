using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiplomacyData
{
    public CivController CivOne;
    public CivController CivTwo;
    public DiplomacyStatusEnum DiplomacyEnumOfCivs = DiplomacyStatusEnum.Neutral; // friendly, allied, at war
    public int DiplomacyPointsOfCivs = 400;
}
