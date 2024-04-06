using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoverTipManager : MonoBehaviour
{
    public TextMeshProUGUI tipText;
    public RectTransform tipWindow;
    public TextMeshProUGUI starNameText;
    public RectTransform starNameWindow;
    public static Action<string, Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;
    public Camera galaxyEventCamera;

    private void OnEnable()
    {
        tipText.text = this.name;
        OnMouseHover += ShowTip;
        OnMouseLoseFocus += HideTip;
    }
    private void OnDisable()
    {
        OnMouseHover -= ShowTip;
        OnMouseLoseFocus -= HideTip;
    }

    void Start()
    {
        tipText.text = this.name;// ToDo more info here
        starNameText.text = this.name;
        starNameWindow.sizeDelta = new Vector2(starNameText.preferredWidth > 10 ? starNameText.preferredWidth/4 : 10, 15);
        HideTip();

        if (galaxyEventCamera == null)
        {
            galaxyEventCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
            this.GetComponent<Canvas>().worldCamera = galaxyEventCamera; // world camear also is event camera for world space canvas
        }

    }

    private void ShowTip(string tip, Vector2 mousePosition)
    {
        tipText.text = tip;
        tipWindow.sizeDelta = new Vector2(tipText.preferredWidth > 200 ? 200 : 
            tipText.preferredWidth, tipText.preferredHeight);
        tipWindow.gameObject.SetActive(true);
        tipWindow.transform.position = new Vector2(mousePosition.x + tipWindow.sizeDelta.x * 2, mousePosition.y);
    }
    private void HideTip()
    {
        tipText.text = default;
        tipWindow.gameObject.SetActive(false);
    }
}
