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
        //public float maxWarpFactor;
        //private float fudgeFactor = 0.05f;
        //private Rigidbody rb;
        ////public FleetData fleetData; 

        //private void Start()
        //{
        //    rb = GetComponent<Rigidbody>();
        //    //fleetData = GetComponent<FleetData>();
        //    //maxWarpFactor = fleetData.currentWarpFactor;
        //}
        //public void DeltaFleetWarpFactor(float warpFactor)
        //{
        //    maxWarpFactor = warpFactor;
        //}
        //private void FixedUpdate()
        //{
        //    //maxWarpFactor = fleetData.MaxWarpFactor;
        //    if (maxWarpFactor > 0)
        //    {
        //        Vector3 destinationPosition = Destination.transform.position;
        //        Vector3 currentPosition = transform.position;
        //        float distance = Vector3.Distance(destinationPosition, currentPosition);
        //        if (distance < dropOutOfWarpDistance)
        //        {
        //            Vector3 travelVector = destinationPosition - transform.position;
        //            travelVector.Normalize();
        //            rb.MovePosition(currentPosition + (travelVector * maxWarpFactor * fudgeFactor * Time.deltaTime));
        //        }
        //    }
        //}
    }
}
