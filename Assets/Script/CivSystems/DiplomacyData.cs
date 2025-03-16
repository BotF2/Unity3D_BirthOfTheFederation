using Assets.Core;

public class DiplomacyData
{
    public CivController CivOne;
    public CivController CivTwo;
    public int DegreesOfSeparation = 0;  
    public DiplomacyStatusEnum DiplomacyEnumOfCivs = DiplomacyStatusEnum.Neutral; // friendly, allied, at war
    public int DiplomacyPointsOfCivs = 60; // neutral
    public bool IsFirstContact = true;
}
