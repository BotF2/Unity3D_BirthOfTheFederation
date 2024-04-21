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
    public TextMeshProUGUI FleetName;
    public TextMeshProUGUI FleetDescription;

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

    public void ShowTip(string fleetName, string fleetDetail) //TextMeshProUGUI mouseOverText, GameObject background)//Vector2 position, GameObject background)
    {
        FleetName.text = fleetName;
        FleetDescription.text = fleetDetail;
        ToolTipTrans.gameObject.SetActive(true);

    }
    public void HidTip() 
    {
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
