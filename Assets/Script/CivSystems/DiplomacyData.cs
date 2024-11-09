using Assets.Core;

public class DiplomacyData
{
    public CivController CivOne;
    public CivController CivTwo;
    public DiplomacyStatusEnum DiplomacyEnumOfCivs = DiplomacyStatusEnum.Neutral; // friendly, allied, at war
    public int DiplomacyPointsOfCivs = 50; // neutral
}
