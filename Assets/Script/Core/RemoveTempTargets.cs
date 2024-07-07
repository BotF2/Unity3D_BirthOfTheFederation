using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using TMPro;

namespace Assets.Core
{
    public class RemoveTempTargets : MonoBehaviour
    {
        [SerializeField]
        private TrekEventSO onRemoveTempTargets;
        public void RemoveTheTempTargets()
        {
            foreach (var gameObj in GameObject.FindGameObjectsWithTag("DestroyTemp"))
            {
                Destroy(gameObj);
            }
        }
    }
}
