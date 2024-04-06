using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.VisualScripting;

namespace Assets.Core
{
    public class MoveGalacticObjects : MonoBehaviour
    {
        //public GalaxyView galaxyView;
        [SerializeField]
        public GameObject target;
        //private GameObject planeEndPoint;
        [SerializeField]
        public float warpSpeed = 5f;
        private float realSpeedFactor = 0.05f;

        Transform myTrans;
        //Transform lastTrans;
        Vector3 myTargetPosition;
        Transform _galaxyPlaneTrans;

        private void Update()
        {
            //if (myTrans != null && lastTrans != null)
            //{
            //    if (myTrans.transform != lastTrans.transform)
            //    {
            //        var fleet = this.GetComponentInParent<Fleet>();
            //        if (fleet._galaxyPlanePoint != null)
            //        {
            //            Vector3 planePint = new Vector3(myTrans.position.x, myTrans.position.inputY, 600f);
            //            GameObject emptyForFleetPlanePoint = Instantiate(endpointPrefab, planePint, Quaternion.identity);
            //            Transform[] endFleetPoints = new Transform[2] { myTrans, emptyForFleetPlanePoint.transform };
            //            galaxyDropLine.SetUpLine(endFleetPoints);
            //        }
            //    }
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
            //myTargetPosition = myTarget.transform.position;
            //warpSpeed = myWarpSpeed;
            //_galaxyPlaneTrans = newPlaneEndPoint.transform;

        }

        public void ThrustVector() 
        {
            //myTrans.position += myTrans.forward * warpSpeed; // * Time.deltaTime;
            if (target != null && myTrans != null)
            {
                myTrans.position = Vector3.MoveTowards(myTrans.position, target.transform.position, warpSpeed * realSpeedFactor);
                _galaxyPlaneTrans.position = new Vector3(myTrans.position.x, myTrans.position.y, _galaxyPlaneTrans.position.z);

            }
            else if (myTargetPosition != null && myTrans != null)
            {
                myTrans.position = Vector3.MoveTowards(myTrans.position, myTargetPosition, warpSpeed * realSpeedFactor);
                _galaxyPlaneTrans.position = new Vector3(myTrans.position.x, myTrans.position.y, _galaxyPlaneTrans.position.z);
                //_galaxyPlaneTrans.Translate(myTrans.localPosition.x, myTrans.localPosition.inputY, 600f);
            }
        }
    }
}
