using TMPro;
using UnityEngine;

public class HoverManager : MonoBehaviour
{
    public static HoverManager Instance;
    public Canvas parentCanvas;
    public Transform ToolTipTrans;
    public TextMeshProUGUI Name;

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
