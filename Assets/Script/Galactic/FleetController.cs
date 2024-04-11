using GalaxyMap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    public class FleetController : MonoBehaviour
    {
        //Fields
        public FleetData fleetData;
       // public GameObject fleetPrefab;


        void Update()
        {
            if (fleetData != null)
            {
                fleetData.Location = transform.position;
            }
        }
        public void UpdateWarpFactor(int delta)
        {
            fleetData.WarpFactor += delta;
        }
        private void OnEnable()
        {
            //fleetData =  
            if (fleetData != null)
                fleetData.Location = transform.position;
        }
        void OnTriggerEnter(Collider collider)
        {

            FleetController fleetController = collider.gameObject.GetComponent<FleetController>();
            if (fleetController != null)
            {
                // fleet controller to get civ list we know
                Debug.Log("fleet Controller collided with " + fleetController.gameObject.name);
            }
            
        }
    }

}
