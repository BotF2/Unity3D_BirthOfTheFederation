namespace Assets.Core
{
    public class GameData
    {
        // For now we need something to give a "LocalPlayer" selection before CivManager is instantiated
        // this GameDate exists when main menu boots up but CivManger is not
        // Will be done in NetCode network check of GameObjects for local player
        // in CivManager but need in a Data file for save game ?
        public CivEnum LocalPlayerCivEnum; // temp set to fed for now


        //public CivController LocalPlayerCivController;

    }
}
