using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Unity.VisualScripting;

namespace Assets.Core
{
    public class MoveGalacticObjects : MonoBehaviour
    {
        [SerializeField]
        public GameObject destination;
        private GameObject planeEndPoint;
        [SerializeField]
        public float warpSpeed = 5f;
        private float realSpeedFactor = 0.05f;
        Transform lastTrans;
        Vector3 myDestinationPosition;
        //Transform _galaxyPlaneTrans;

        private void Start()
        {
                lastTrans = transform;
        }
        private void Update()
        {
            //if (transform != null)
            //{
            //    if (transform != lastTrans)
            //    {
            //        var controller = this.GetComponentInParent<FleetController>();
            //        if (controller.fleetData.deltaYofGalaxyImage != null)
            //        {
            //            Vector3 planePiont = new Vector3(transform.position.x, controller.fleetData.deltaYofGalaxyImage, transform.position.z);
            //            //GameObject emptyForFleetPlanePoint = Instantiate(endpointPrefab, planePiont, Quaternion.identity);
            //            Vector3[] points = new Vector3[] {transform.position, planePiont };
            //            var ourDropLine = this.GetComponent<DropLineMovable>();
            //            //Transform[] endFleetPoints = new Transform[2] { myTrans, emptyForFleetPlanePoint.transform };
            //            ourDropLine.SetUpLine(points);
            //        }
            //    }
            //    lastTrans = transform;
            //}

        }
        public void BoldlyGoing(FleetData fleet, GameObject myTarget, GameObject newPlaneEndPoint, float myWarpSpeed) //, GalaxyDropLine fleetLine)
        {
            //if (myWarpSpeed == 0 && GalaxyView._movingGalaxyObjects.Contains(fleet.gameObject))
            //{
            //    GalaxyView._movingGalaxyObjects.Remove(fleet.gameObject);
            //    return;
            //}
            //else if (!GalaxyView._movingGalaxyObjects.Contains(fleet.gameObject))
            //{
            //    GalaxyView._movingGalaxyObjects.Add(fleet.gameObject);
            //}
            //galaxyDropLine = fleetLine;
            //myTrans = fleet.transform;
            ////lastTrans = myTrans;
            //myDestinationPosition = myTarget.transform.position;
            //warpSpeed = myWarpSpeed;
            //_galaxyPlaneTrans = newPlaneEndPoint.transform;

        }

        public void ThrustVector() 
        {
            //myTrans.position += myTrans.forward * warpSpeed; // * Time.deltaTime;
            //if (destination != null && myTrans != null)
            //{
            //    myTrans.position = Vector3.MoveTowards(myTrans.position, destination.transform.position, warpSpeed * realSpeedFactor);
            //    _galaxyPlaneTrans.position = new Vector3(myTrans.position.x, myTrans.position.y, _galaxyPlaneTrans.position.z);

            //}
            //else if (myDestinationPosition != null && myTrans != null)
            //{
            //    myTrans.position = Vector3.MoveTowards(myTrans.position, myDestinationPosition, warpSpeed * realSpeedFactor);
            //    _galaxyPlaneTrans.position = new Vector3(myTrans.position.x, myTrans.position.y, _galaxyPlaneTrans.position.z);
            //    //_galaxyPlaneTrans.Translate(myTrans.localPosition.x, myTrans.localPosition.inputY, 600f);
            //}
        }
    }
}
