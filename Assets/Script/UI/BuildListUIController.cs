using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Core;

public class BuildListUIController : MonoBehaviour
{
    public static BuildListUIController Instance { get; private set; }
    [SerializeField]
    private GameObject sysFactoriesUIprefab;
    [SerializeField]
    private GameObject sysQueueItemPrefab;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
