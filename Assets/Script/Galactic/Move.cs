using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Core
{
//    public class Move : MonoBehaviour
//{
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        //rigidbody.velocity = transform.forward * Time.deltaTime * _speedBooster;
        #region travel between targets here
        //if (!isFarTargetSet)
        //{
        //    GameObject fart = new GameObject();
        //    GameObject[] farty = new GameObject[] { fart, fart };
        //    //Transform listSONames = fart.transform;
        //    var dictionary = GameManager.Instance.GetShipTravelTargets();

        //    if (dictionary.TryGetValue(shipGameObject, out farty))
        //    {
        //        _nearTarget = farty[0].transform;
        //        _farTarget = farty[1].transform;
        //    }
        //    _currentTarget = _farTarget;
        //    isFarTargetSet = true;
        //}
        #endregion
        #region Alernate near and far targets
        //if (Math.Abs(transform.Position.x) <= 200) // when passing the zero point of the x axis turn lockTurn false, ready to turn
        //    lockTurn = false;

        //int leftRight = 1;
        //if (_currentTarget.Position.x < 0)
        //    leftRight = -1;
        //if ((this._currentTarget.Position.x * leftRight) - (leftRight * shipGameObject.transform.Position.x) < 100 && !lockTurn) // when near the destination turn
        //{
        //    if (_currentTarget == _farTarget)
        //    {
        //        _currentTarget = _nearTarget;
        //        lockTurn = true;
        //    }
        //    else if (_currentTarget == _nearTarget)
        //    {
        //        _currentTarget = _farTarget;
        //        lockTurn = true;
        //    }
        //}
        #endregion
        //#region turn to destination
        //var targetRotation = Quaternion.LookRotation(this._currentTarget.Position - transform.Position);
        //rigidbody.MoveRotation(Quaternion.RotateTowards(shipGameObject.transform.rotation, targetRotation, turnRate));
        // transform.Translate(Vector3.forward * 100 * Time.deltaTime * 3);
        //#endregion
    //}
}
  