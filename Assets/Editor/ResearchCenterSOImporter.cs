using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using System.IO;

public class ResearchCenterSOImporter : EditorWindow
{

#if UNITY_EDITOR

    [MenuItem("Tools/Import ResearchCenterSO TSV")]
    public static void ShowWindow()
    {
        GetWindow<ResearchCenterSOImporter>("ResearchCenterSO TSV Importer");
    }

    private string filePath = $"Assets/Resources/Data/StarSysResearchCenter.tsv";

    void OnGUI()
    {
        GUILayout.Label("ResearchCenterSO TSV Importer", EditorStyles.boldLabel);
        filePath = EditorGUILayout.TextField("TSV File Path", filePath);

        if (GUILayout.Button("Import ResearchCenterSO TSV"))
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
                foreach (string file in Directory.GetFiles($"Assets/Resources/Facilities/", "*.png"))
                {
                    if (file == "Assets/Resources/Facilities/" + imageString + ".png")
                    {
                        imageString = "Facilities/" + imageString;
                    }
                }


                if (fields.Length >= 7) // Ensure there are enough fields
                {
                    ResearchCenterSO ResearchCenterSO = CreateInstance<ResearchCenterSO>();
                    ////StarSysInt	,	ResearchCenterSO Enum	,	ResearchCenterSO Short TextComponent	,	ResearchCenterSO Long TextComponent	,	Home System	,	Triat One	,	Trait Two	,	ResearchCenterSO Image	,	Insginia	,	Population	,	Credits	,	StartingTechLevel Points
                    ResearchCenterSO.CivInt = int.Parse(fields[0]);
                    ResearchCenterSO.TechLevel = (TechLevel)int.Parse(fields[1]);
                    ResearchCenterSO.FacilitiesEnumType = (StarSysFacilities)int.Parse(fields[2]);
                    ResearchCenterSO.Name = (fields[3]);
                    ResearchCenterSO.StartStarDate = int.Parse(fields[5]);
                    ResearchCenterSO.BuildDuration = int.Parse(fields[6]);
                    ResearchCenterSO.PowerLoad = int.Parse(fields[7]);
                    ResearchCenterSO.ResearchCenterSprite = Resources.Load<Sprite>(imageString);
                    ResearchCenterSO.Description = (fields[8]);
                    string assetPath = $"Assets/SO/StarSysResearchCenterSO/ResearchCenterSO_{ResearchCenterSO.CivInt}_{ResearchCenterSO.Name}.asset";
                    AssetDatabase.CreateAsset(ResearchCenterSO, assetPath);
                    AssetDatabase.SaveAssets();
                }
            }
            Debug.Log("ResearchCenterSOImporter Import Complete");
        }
    }

#endif
}

