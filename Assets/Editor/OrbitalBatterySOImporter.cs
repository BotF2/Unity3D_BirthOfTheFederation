using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Assets.Core;
using System;
using System.IO;

public class OrbitalBatterySOImporter : EditorWindow
{ 
#if UNITY_EDITOR

    [MenuItem("Tools/Import OrbitalBatterySO TSV")]
    public static void ShowWindow()
    {
        GetWindow<OrbitalBatterySOImporter>("OrbitalBatterySO TSV Importer");
    }

    private string filePath = $"Assets/Resources/Data/StarSysOrbitalBattery.tsv";

    void OnGUI()
    {
        GUILayout.Label("OrbitalBatterySO TSV Importer", EditorStyles.boldLabel);
        filePath = EditorGUILayout.TextField("TSV File Path", filePath);

        if (GUILayout.Button("Import OrbitalBatterySO TSV"))
        {
            //Output the Game data path to the console
            Debug.Log("dataPath : " + Application.dataPath);
            ImportShieldTSV(filePath);
        }
    }
    private static void ImportShieldTSV(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("File not found: " + filePath);
            return;
        }

        string[] lines = File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            string[] fields = line.Split("\t");

            if (fields.Length > 7) // Ensure there are enough fields
            {
                string imageString = fields[4];
                foreach (string file in Directory.GetFiles($"Assets/Resources/OrbitalBatteries/", "*.png"))
                {
                    if (file == "Assets/Resources/OrbitalBatteries/" + imageString + ".png")
                    {
                        imageString = "OrbitalBatteries/" + imageString;
                    }
                }


                if (fields.Length >= 7) // Ensure there are enough fields
                {
                    OrbitalBatterySO OrbitalBatterySO = CreateInstance<OrbitalBatterySO>();
                    ////StarSysInt	,	OrbitalBatterySO Enum	,	OrbitalBatterySO Short TextComponent	,	OrbitalBatterySO Long TextComponent	,	Home System	,	Triat One	,	Trait Two	,	OrbitalBatterySO Image	,	Insginia	,	Population	,	Credits	,	StartingTechLevel Points
                    OrbitalBatterySO.CivInt = int.Parse(fields[0]);
                    OrbitalBatterySO.TechLevel = (TechLevel)int.Parse(fields[1]);
                    OrbitalBatterySO.FacilitiesEnumType = (StarSysFacilities)int.Parse(fields[2]);
                    OrbitalBatterySO.Name = (fields[3]);
                    OrbitalBatterySO.StartStarDate = int.Parse(fields[5]);
                    OrbitalBatterySO.BuildDuration = int.Parse(fields[6]);
                    OrbitalBatterySO.PowerLoad = int.Parse(fields[7]);
                    OrbitalBatterySO.OrbitalBatterySprite = Resources.Load<Sprite>(imageString);
                    OrbitalBatterySO.Description = (fields[8]);
                    string assetPath = $"Assets/SO/StarSysOrbitalBatterySO/OrbitalBatterySO_{OrbitalBatterySO.CivInt}_{OrbitalBatterySO.Name}.asset";
                    AssetDatabase.CreateAsset(OrbitalBatterySO, assetPath);
                    AssetDatabase.SaveAssets();
                }
            }
            Debug.Log("OrbitalBatterySOImporter Import Complete");
        }
    }

#endif
}

