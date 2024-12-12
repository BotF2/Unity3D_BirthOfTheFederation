using Assets.Core;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class PowerPlantSOImporter : EditorWindow
{
#if UNITY_EDITOR

    [MenuItem("Tools/Import PowerPlantSO CSV")]
    public static void ShowWindow()
    {
        GetWindow<PowerPlantSOImporter>("PowerPlantSO CSV Importer");
    }

    private string filePath = $"Assets/Resources/Data/StarSysPowerPlant.csv";

    void OnGUI()
    {
        GUILayout.Label("PowerPlantSO CSV Importer", EditorStyles.boldLabel);
        filePath = EditorGUILayout.TextField("CSV File Path", filePath);

        if (GUILayout.Button("Import PowerPlantSO CSV"))
        {
            //Output the Game data path to the console
            Debug.Log("dataPath : " + Application.dataPath);
            ImportStarSysCSV(filePath);
        }
    }
    private static void ImportStarSysCSV(string filePath)
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

            if (fields.Length > 7) // Ensure there are enough fields
            {
                string imageString = fields[4];
                foreach (string file in Directory.GetFiles($"Assets/Resources/Facilities/", "*.png"))
                {
                    if (file == "Assets/Resources/Facilities/" + imageString + ".png")
                    {
                        imageString = "Facilities/" + imageString;
                    }
                }


                if (fields.Length >= 7) // Ensure there are enough fields
                {
                    PowerPlantSO powerPlantSO = CreateInstance<PowerPlantSO>();
                    ////StarSysInt	,	powerPlantSO Enum	,	powerPlantSO Short Name	,	powerPlantSO Long Name	,	Home System	,	Triat One	,	Trait Two	,	powerPlantSO Image	,	Insginia	,	Population	,	Credits	,	TechLevel Points
                    powerPlantSO.CivInt = int.Parse(fields[0]);
                    powerPlantSO.TechLevel = (TechLevel)int.Parse(fields[1]);
                    powerPlantSO.FacilitiesEnumType = (StarSysFacilities)int.Parse(fields[2]);
                    powerPlantSO.Name = (fields[3]);
                    powerPlantSO.StartStarDate = int.Parse(fields[5]);
                    powerPlantSO.BuildDuration = int.Parse(fields[6]);
                    powerPlantSO.PowerOut = int.Parse(fields[7]);
                    powerPlantSO.PowerPlantSprite = Resources.Load<Sprite>(imageString);
                    powerPlantSO.Description = (fields[8]);
                    string assetPath = $"Assets/SO/StarSysPowerPlantSO/PowerPlantSO_{powerPlantSO.CivInt}_{powerPlantSO.Name}.asset";
                    AssetDatabase.CreateAsset(powerPlantSO, assetPath);
                    AssetDatabase.SaveAssets();
                }
            }
            Debug.Log("CivSOImporter Import Complete");
        }
    }

#endif
}
