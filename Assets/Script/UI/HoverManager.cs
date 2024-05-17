using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class HoverManager : MonoBehaviour
{
    public static HoverManager instance;
    public Canvas parentCanvas;
    public Transform ToolTipTrans;
    public TextMeshProUGUI Name;
    //public TextMeshProUGUI Description;

    private void Awake()
    {    
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ShowTip(string name) 
    {
        ToolTipTrans.gameObject.SetActive(true);
        Name.text = name;
    }
    public void HidTip() 
    {
        Name.text = string.Empty;
        ToolTipTrans.gameObject.SetActive(false);
    }
    private void Update()
    {
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform,
            Input.mousePosition, parentCanvas.worldCamera, out movePos);
        ToolTipTrans.position = parentCanvas.transform.TransformPoint(movePos);
    }

}
