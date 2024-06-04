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

        //public GameObject _destination;
        //private Transform droplineEndPoint; //ToDo
        //public float dropOutOfWarpDistance = 0.1f;
        //public float MaxWarpFactor;
        //private float fudgeFactor = 0.05f;
        //private Rigidbody rb;
        ////public FleetData fleetData; 

        //private void Start()
        //{
        //    rb = GetComponent<Rigidbody>();
        //    //fleetData = GetComponent<FleetData>();
        //    //MaxWarpFactor = fleetData.CurrentWarpFactor;
        //}
        //public void DeltaFleetWarpFactor(float warpFactor)
        //{
        //    MaxWarpFactor = warpFactor;
        //}
        //private void FixedUpdate()
        //{
        //    //MaxWarpFactor = fleetData.MaxWarpFactor;
        //    if (MaxWarpFactor > 0)
        //    {
        //        Vector3 destinationPosition = _destination.transform.position;
        //        Vector3 currentPosition = transform.position;
        //        float distance = Vector3.Distance(destinationPosition, currentPosition);
        //        if (distance < dropOutOfWarpDistance)
        //        {
        //            Vector3 travelVector = destinationPosition - transform.position;
        //            travelVector.Normalize();
        //            rb.MovePosition(currentPosition + (travelVector * MaxWarpFactor * fudgeFactor * Time.deltaTime));
        //        }
        //    }
        //}
    }
}
