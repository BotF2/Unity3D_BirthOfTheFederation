using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Unity.VisualScripting;

namespace Assets.Core
{
    public class MoveFleets : MonoBehaviour
    {
        // NOW USING FLEETCONTROLLER........

        //public GameObject Destination;
        //private Transform droplineEndPoint; //ToDo
        //public float dropOutOfWarpDistance = 0.1f;
        //public float WarpFactor;
        //private float fudgeFactor = 0.05f;
        //private Rigidbody rb;
        ////public FleetData fleetData; 

        //private void Start()
        //{
        //    rb = GetComponent<Rigidbody>();
        //    //fleetData = GetComponent<FleetData>();
        //    //WarpFactor = fleetData.DefaultWarpFactor;
        //}
        //public void DeltaFleetWarpFactor(float warpFactor)
        //{
        //    WarpFactor = warpFactor;
        //}
        //private void FixedUpdate()
        //{
        //    //WarpFactor = fleetData.MaxWarpFactor;
        //    if (WarpFactor > 0)
        //    {
        //        Vector3 destinationPosition = Destination.transform.position;
        //        Vector3 currentPosition = transform.position;
        //        float distance = Vector3.Distance(destinationPosition, currentPosition);
        //        if (distance < dropOutOfWarpDistance)
        //        {
        //            Vector3 travelVector = destinationPosition - transform.position;
        //            travelVector.Normalize();
        //            rb.MovePosition(currentPosition + (travelVector * WarpFactor * fudgeFactor * Time.deltaTime));
        //        }
        //    }
        //}
    }
}
