
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
        public float DefaultWarpFactor = 0f;
        public float warpFactor;
        public string Name;
        public string description;
        public List<Ship> Ships;
        public Vector3 location;
        public GameObject destination;
        public GameObject origin;
    }
}




