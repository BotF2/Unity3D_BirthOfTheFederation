using Assets.Core;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DiplomacyData
{
    public CivController CivOne;
    public CivController CivTwo;
    public Vector3 PositionOfNonLocalPlayerHomeSys;
    public DiplomacyStatusEnum DiplomacyEnumOfCivs = DiplomacyStatusEnum.Neutral; // friendly, allied, at war
    public int DiplomacyPointsOfCivs = 60; // neutral
    public bool IsFirstContact = true;

}
