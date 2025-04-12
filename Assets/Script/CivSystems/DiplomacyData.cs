using Assets.Core;
using NUnit.Framework;
using System.Collections.Generic;

public class DiplomacyData
{
    public CivController CivOne;
    public CivController CivTwo;
    public DiplomacyStatusEnum DiplomacyEnumOfCivs = DiplomacyStatusEnum.Neutral; // friendly, allied, at war
    public int DiplomacyPointsOfCivs = 60; // neutral
    public bool IsFirstContact = true;

}
