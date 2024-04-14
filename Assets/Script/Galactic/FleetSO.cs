
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

namespace Assets.Core

{
    [CreateAssetMenu(menuName = "Galaxy/FleetSO")]
    public class FleetSO : ScriptableObject
    {
        public int CivIndex;
        public Sprite Insignia;
        public CivEnum CivOwnerEnum;
        //public string CivShortName;
        //public string CivLongName;
        public Vector3 Location;
        public List<Ship> ShipsList;
        public float WarpFactor = 0f;
        public float DefaultWarpFactor = 0f;
        public string Name;
        public string Description;      
        public GameObject Destination;
        //public GameObject origin;
    }
}




