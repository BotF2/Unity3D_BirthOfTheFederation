using UnityEngine;
using UnityEditor;
using System.IO;
using Assets.Core;
using System;
using Unity.VisualScripting;
using System.Diagnostics.Eventing.Reader;
//using UnityEngine.Windows;

public class CivSOImporter : EditorWindow
{
    [MenuItem("Tools/Import CivSO CSV")]
    public static void ShowWindow()
    {
        GetWindow<CivSOImporter>("CivSO CSV Importer");
    }

    private string filePath = $"Assets/Resources/Data/Civilizations.csv";

    void OnGUI()
    {
        GUILayout.Label("CivSO CSV Importer", EditorStyles.boldLabel);
        filePath = EditorGUILayout.TextField("CSV File Path", filePath);

        if (GUILayout.Button("Import CIV CSV"))
        {
            //Output the Game data path to the console
            Debug.Log("dataPath : " + Application.dataPath);
            ImportCIVCSV(filePath);
        }
    }

    private static void ImportCIVCSV(string filePath)
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

            if (fields.Length == 13) // Ensure there are enough fields
            {
                string imageString = fields[2].ToLower();
                foreach (string file in Directory.GetFiles($"Assets/Resources/Races/", "*.png"))
                {
                    if(file == "Assets/Resources/Races/" + imageString +".png")
                    {
                        imageString = "Races/"+ imageString;
                    }
                    else if (file == "Assets/Resources/Races/"+ imageString + "s" +".png")
                    {
                        imageString = "Races/" + imageString+ "s"; 
                    }
                }


                CivSO civ = CreateInstance<CivSO>();
                //CivInt	,	Civ Enum	,	Civ Short Name	,	Civ Long Name	,	Home System	,	Triat One	,	Trait Two	,	Civ Image	,	Insginia	,	Population	,	Credits	,	Tech Points
                civ.CivInt = int.Parse(fields[0]);
                civ.CivEnum = GetMyCivEnum(fields[1]);
                civ.CivShortName= fields[2];
                civ.CivLongName = fields[3];
                civ.CivHomeSystem = fields[4];
                civ.TraitOne = fields[5];
                civ.TraitTwo = fields[6];
                civ.CivImage = Resources.Load<Sprite>(imageString);
                //if (fields[2].LastIndexOf == "S") { }
               //civ.Insignia =
                var name = Resources.Load<Sprite>("Insignias/" + fields[2].ToUpper());
                if (name == null) { name = Resources.Load<Sprite>("Insignias/" + fields[2].ToUpper() + "S"); }
                civ.Insignia = name;
                civ.Population = int.Parse(fields[9]);
                civ.Credits = int.Parse(fields[10]);
                civ.TechPoints = int.Parse(fields[11]);
                civ.CivTechLevel = TechLevel.EARLY;// TechLevel enum
                if (civ.CivInt <= 5 || civ.CivInt == 158){ civ.Playable = true; }
                else civ.Playable = false;
                civ.HasWarp = bool.Parse(fields[12]);
                civ.Decription = "ToDo, connect to libaray of civ descriptions";
                civ.StarSysOwned = new System.Collections.Generic.List<StarSysData> { new StarSysData() };
                civ.IntelPoints = 0f;
                //civ.ContactList = new System.Collections.Generic.List<CivData>(); // we know our self + maybe a 'Vulcans' for each major??


                string assetPath = $"Assets/SO/CivilizationSO/CivSO_{civ.CivInt}_{civ.CivShortName}.asset";
                AssetDatabase.CreateAsset(civ, assetPath);
                AssetDatabase.SaveAssets();
            }
        }

        Debug.Log("CivSOImporter Import Complete");
    }
    public static CivEnum GetMyCivEnum(string title)
    {
        CivEnum st;
        Enum.TryParse(title, out st);
        return st;
    }
} 
