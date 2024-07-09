using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    public class ShipUI : Draggable
    {
        public override void UpdateObject()
        {
             ShipSO ship = obj as ShipSO;   
        }
    }
}
