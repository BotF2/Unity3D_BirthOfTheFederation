using Assets.Core;
using UnityEngine;

public class MousePointerChanger : MonoBehaviour
{
    public static MousePointerChanger Instance;
    // Reference to the new cursor texture
    [SerializeField]
    private Texture2D galaxyMapCursorFed;
    [SerializeField]
    private Texture2D galaxyMapCursorRom;
    [SerializeField]
    private Texture2D galaxyMapCursorKling;
    [SerializeField]
    private Texture2D galaxyMapCursorCard;
    [SerializeField]
    private Texture2D galaxyMapCursorDom;
    [SerializeField]
    private Texture2D galaxyMapCursorBorg;
    [SerializeField]
    private Texture2D galaxyMapCursorTerran;
    public bool HaveGalaxyMapCursor = false;


    // Define the hot spot of the cursor (the point that will be the "clicking" point)
    private Vector2 hotSpot = Vector2.zero;
    // Cursor mode - Auto uses system default, ForceSoftware uses Unity's software cursor
    public CursorMode cursorMode = CursorMode.Auto;

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
    // Call this function to change the cursor
    public void ChangeToGalaxyMapCursor()
    {
        if (GameController.Instance.AreWeLocalPlayer(CivEnum.FED))
            ChangeCursor(galaxyMapCursorFed, hotSpot, cursorMode);
        else if (GameController.Instance.AreWeLocalPlayer(CivEnum.ROM))
            ChangeCursor(galaxyMapCursorRom, hotSpot, cursorMode);
        else if (GameController.Instance.AreWeLocalPlayer(CivEnum.KLING))
            ChangeCursor(galaxyMapCursorKling, hotSpot, cursorMode);
        else if (GameController.Instance.AreWeLocalPlayer(CivEnum.CARD))
            ChangeCursor(galaxyMapCursorCard, hotSpot, cursorMode);
        else if (GameController.Instance.AreWeLocalPlayer(CivEnum.DOM))
            ChangeCursor(galaxyMapCursorDom, hotSpot, cursorMode);
        else if (GameController.Instance.AreWeLocalPlayer(CivEnum.BORG))
            ChangeCursor(galaxyMapCursorBorg, hotSpot, cursorMode);
        else ChangeCursor(galaxyMapCursorTerran, hotSpot, cursorMode);
    }

    // Function to change the cursor
    private void ChangeCursor(Texture2D cursorTexture, Vector2 hotSpot, CursorMode cursorMode)
    {
        HaveGalaxyMapCursor = true; // used by FleetUIManager
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    // Reset to default cursor
    public void ResetCursor()
    {
        HaveGalaxyMapCursor = false;
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}

