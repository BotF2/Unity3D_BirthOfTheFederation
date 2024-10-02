using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    public class GameData
    {

        public CivEnum LocalPlayerCivEnum; // same as in CivManager but need in a Data file for save game and
                                           // this GameDate exists when menu boot up but CivManger does not
        //public CivController LocalPlayerCivController;

    }
}
