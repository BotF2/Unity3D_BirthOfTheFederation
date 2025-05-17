using Assets.Core;
using UnityEngine;

public class MousePointerChanger : MonoBehaviour
{
    public static MousePointerChanger Instance;
    // Reference to the new cursor texture
    [SerializeField]
    private Texture2D galaxyMapCursorForFedDestination;
    [SerializeField]
    private Texture2D galaxyMapCursorForRomDestination;
    [SerializeField]
    private Texture2D galaxyMapCursorForKlingDestination;
    [SerializeField]
    private Texture2D galaxyMapCursorForCardDestination;
    [SerializeField]
    private Texture2D galaxyMapCursorForDomDestination;
    [SerializeField]
    private Texture2D galaxyMapCursorForBorgDestination;
    [SerializeField]
    private Texture2D galaxyMapCursorTerran;
    public bool HaveGalaxyMapCursor = false;
    public FleetController fleetConBehindGalaxyMapDestinationCursor = null; // used by FleetUIController to check if the cursor is in use
    //public StarSysController starSysForCursor = null;

    // Define the hot spot of the cursor (the point that will be the "clicking" point)
    private Vector2 hotSpot = Vector2.zero;
    // uses Unity's software cursor
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
    public void ChangeToGalaxyMapCursor(FleetController fleetCon)
    {
        fleetConBehindGalaxyMapDestinationCursor = fleetCon;
        ChangeToGalaxyMapCursor();
    }

    public void ChangeToGalaxyMapCursor()
    {
        if (GameController.Instance.AreWeLocalPlayer(CivEnum.FED))
            ChangeCursor(galaxyMapCursorForFedDestination, hotSpot, cursorMode);
        else if (GameController.Instance.AreWeLocalPlayer(CivEnum.ROM))
            ChangeCursor(galaxyMapCursorForRomDestination, hotSpot, cursorMode);
        else if (GameController.Instance.AreWeLocalPlayer(CivEnum.KLING))
            ChangeCursor(galaxyMapCursorForKlingDestination, hotSpot, cursorMode);
        else if (GameController.Instance.AreWeLocalPlayer(CivEnum.CARD))
            ChangeCursor(galaxyMapCursorForCardDestination, hotSpot, cursorMode);
        else if (GameController.Instance.AreWeLocalPlayer(CivEnum.DOM))
            ChangeCursor(galaxyMapCursorForDomDestination, hotSpot, cursorMode);
        else if (GameController.Instance.AreWeLocalPlayer(CivEnum.BORG))
            ChangeCursor(galaxyMapCursorForBorgDestination, hotSpot, cursorMode);
        else ChangeCursor(galaxyMapCursorTerran, hotSpot, cursorMode);
    }

    // Function to change the cursor
    private void ChangeCursor(Texture2D cursorTexture, Vector2 hotSpot, CursorMode cursorMode)
    {
        HaveGalaxyMapCursor = true; // used by FleetUIController
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    // Reset to default cursor
    public void ResetCursor()
    {
        HaveGalaxyMapCursor = false;
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}

