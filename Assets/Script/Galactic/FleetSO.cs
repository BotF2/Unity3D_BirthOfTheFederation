
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
        public Vector3 Location;
        public List<ShipController> ShipsList;
        public float maxWarpFactor = 0f;
        public float currentWarpFactor = 0f;
        public string Name;
        public string Description;      
        public GameObject Destination;
    }
}




