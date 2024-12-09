using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using Assets.Core;
using TMPro;
public class SystemUIMenuManager : MonoBehaviour
{
    public GameObject SystemUIMenuPrefab;
    [SerializeField]
    private TMP_Text text;

    void Start()
    {
        //for (int i = 0; i < 10; i++)
        //{
        //    GameObject obj = Instantiate(SystemUIMenuPrefab);
        //    obj.transform.SetParent(this.gameObject.transform, false);
        //    obj.transform.GetChild(1).GetComponent<TMP_Text>().text = i.ToString(); 
        //}
    }


    void Update()
    {
        
    }
}
