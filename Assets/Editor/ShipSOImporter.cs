using UnityEngine;
using UnityEditor;
using System.IO;
using Assets.Core;
using System;


public class ShipSOImporter : EditorWindow
{
    [MenuItem("Tools/Import ShipSO CSV")]
    public static void ShowWindow()
    {
        GetWindow<ShipSOImporter>("ShipSO CSV Importer");
    }

    private string filePath = $"Assets/Resources/Data/ShipSO.csv";

    void OnGUI()
    {
        GUILayout.Label("ShipSO CSV Importer", EditorStyles.boldLabel);
        filePath = EditorGUILayout.TextField("CSV File Path", filePath);

        if (GUILayout.Button("Import CSV"))
        {
            //Output the Game data path to the console
            Debug.Log("dataPath : " + Application.dataPath);
            ImportCSV(filePath);
        }
    }

    private static void ImportCSV(string filePath)
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
            string assetPath = " ";

            if (fields.Length == 11) // Ensure there are enough fields
            {
                ShipSO ship = CreateInstance<ShipSO>();
                ship.ShipName = fields[0];
                string[] data = ship.ShipName.Split("_");
                ship.CivEnum = GetMyCivEnum(data[0]);
                ship.TechLevel = GetMyTechLevel(data[2], out TechLevel st);
                ship.Class = GetMyShipClass(data[1]);
                ship.ShieldMaxHealth = int.Parse(fields[2]);
                ship.HullMaxHealth = int.Parse(fields[4]);
                ship.TorpedoDamage = int.Parse(fields[6]);
                ship.BeamDamage = int.Parse(fields[8]);
                ship.Cost = int.Parse(fields[10]);
                ship.maxWarpFactor = float.Parse(fields[11]);
                if (ship.TechLevel == TechLevel.EARLY)
                    assetPath = $"Assets/SO/ShipSO_Level_0/ShipSO_{ship.ShipName}.asset";
                else if (ship.TechLevel == TechLevel.DEVELOPED)
                    assetPath = $"Assets/SO/ShipSO_Level_1/ShipSO_{ship.ShipName}.asset";
                else if (ship.TechLevel == TechLevel.ADVANCED)
                    assetPath = $"Assets/SO/ShipSO_Level_2/ShipSO_{ship.ShipName}.asset";
                else if (ship.TechLevel == TechLevel.SUPREME)
                    assetPath = $"Assets/SO/ShipSO_Level_3/ShipSO_{ship.ShipName}.asset";
                AssetDatabase.CreateAsset(ship, assetPath);
                AssetDatabase.SaveAssets();
            }
        }

        Debug.Log("ShipSO Import Complete");
    }
    public static CivEnum GetMyCivEnum(string title)
    {
        CivEnum st;
        Enum.TryParse(title, out st);
        return st;
    }
    public static TechLevel GetMyTechLevel(string title, out TechLevel st)
    {
        title = title.Replace("(CLONE)", "");

        switch (title)
        {
            case "I":
                st = TechLevel.EARLY;
                break;
            case "II":
                st = TechLevel.DEVELOPED;
                break;
            case "III":
                st = TechLevel.ADVANCED;
                break;
            case "IV":
                st = TechLevel.SUPREME;
                break;
            default:
                st = TechLevel.EARLY;
                break;
        }
        return st;
    }
    public static ShipType GetMyShipClass(string title)
    {
        ShipType st;
        Enum.TryParse(title, out st);
        return st;
    }
}
