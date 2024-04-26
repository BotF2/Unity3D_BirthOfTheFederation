using UnityEngine;
using UnityEditor;
using System.IO;
using Assets.Core;
using System;

public class FleetSOImporter : EditorWindow
{
    [MenuItem("Tools/Import FleetSO CSV")]
    public static void ShowWindow()
    {
        GetWindow<FleetSOImporter>("FleetSO CSV Importer");
    }

    private string filePath = $"Assets/Resources/Data/Fleets.csv";

    void OnGUI()
    {
        GUILayout.Label("FleetSO CSV Importer", EditorStyles.boldLabel);
        filePath = EditorGUILayout.TextField("CSV File Path", filePath);

        if (GUILayout.Button("Import FleetSO CSV"))
        {
            //Output the Game data path to the console
            Debug.Log("dataPath : " + Application.dataPath);
            ImportFleetCSV(filePath);
        }
    }

    private static void ImportFleetCSV(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("File not found: " + filePath);
            return;
        }

        string[] lines = File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            string[] fields = line.Split(',');

            if (fields.Length == 5) // Ensure there are enough fields
            {
                string imageString = fields[1];
                foreach (string file in Directory.GetFiles($"Assets/Resources/Insignias/", "*.png"))
                {
                    if (file == "Assets/Resources/Insignias/" + imageString + ".png")
                    {
                        imageString = "Insignias/" + imageString;
                    }
                    else if (file == "Assets/Resources/Insignias/" + imageString + "S" + ".png")
                    {
                        imageString = "Insignias/" + imageString + "S";
                    }
                }
                FleetSO fleet = CreateInstance<FleetSO>();
                //index, insignia, fleetName, civOwnerEnum, defaultWarp
                fleet.CivIndex = int.Parse(fields[0]);
                fleet.Insignia = Resources.Load<Sprite>(imageString);
                fleet.CivOwnerEnum = GetMyCivEnum(fields[2]);
                fleet.DefaultWarpFactor = float.Parse(fields[3]);
                fleet.Description = fields[4];
                string assetPath = $"Assets/SO/FleetSO/FleetSO_{fleet.CivIndex}_{fleet.CivOwnerEnum}.asset";
                AssetDatabase.CreateAsset(fleet, assetPath);
                AssetDatabase.SaveAssets();
            }
        }

        Debug.Log("FleetSOImporter Import Complete");
    }
    public static CivEnum GetMyCivEnum(string title)
    {
        CivEnum st;
        Enum.TryParse(title, out st);
        return st;
    }
}
