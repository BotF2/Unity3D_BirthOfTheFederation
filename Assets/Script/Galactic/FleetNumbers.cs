using Assets.Core;
using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FleetNumbers : MonoBehaviour
{
    //private CivEnum civEnum;
    private List<int> numsInUse =new List<int>();

    public int GetNewFleetInt()
    {
        int numToReturn = 1;
        if (numsInUse.Count == 0)
            return numToReturn;
        else
        {
            for (int i = 1; i < numsInUse.Count +1; i++)
            {
                if (numsInUse[i] != i)
                    numToReturn = i;
            }
            numsInUse.Add(numToReturn);
            numsInUse.Sort();
        }
        return numToReturn;
    }
    
}
