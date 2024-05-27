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
                CivSO civSO = CreateInstance<CivSO>();
                //CivInt	,	Civ Enum	,	Civ Short Name	,	Civ Long Name	,	Home System	,	Triat One	,	Trait Two	,	Civ Image	,	Insginia	,	Population	,	Credits	,	TechLevel Points
                civSO.CivInt = int.Parse(fields[0]);
                civSO.CivEnum = GetMyCivEnum(fields[1]);
                civSO.CivShortName= fields[2];
                civSO.CivLongName = fields[3];
                civSO.CivHomeSystem = fields[4];
                civSO.TraitOne = fields[5];
                civSO.TraitTwo = fields[6];
                civSO.CivImage = Resources.Load<Sprite>(imageString);
                //if (fields[2].LastIndexOf == "S") { }
               //civSO.Insignia =
                var name = Resources.Load<Sprite>("Insignias/" + fields[2].ToUpper());
                if (name == null) { name = Resources.Load<Sprite>("Insignias/" + fields[2].ToUpper() + "S"); }
                civSO.Insignia = name;
                civSO.Population = int.Parse(fields[9]);
                civSO.Credits = int.Parse(fields[10]);
                civSO.TechPoints = int.Parse(fields[11]);
                civSO.CivTechLevel = TechLevel.EARLY;// TechLevel enum
                if (civSO.CivInt <= 5 || civSO.CivInt == 158){ civSO.Playable = true; }
                else civSO.Playable = false;
                civSO.HasWarp = bool.Parse(fields[12]);
                civSO.Decription = "ToDo, connect to libaray of civSO descriptions";
                civSO.StarSysOwned = new System.Collections.Generic.List<StarSysData> { new StarSysData("Place Holder") };
                civSO.IntelPoints = 0f;
                //civSO.ContactList = new System.Collections.Generic.List<CivData>(); // we know our self + maybe a 'Vulcans' for each major??
                string assetPath = $"Assets/SO/CivilizationSO/CivSO_{civSO.CivInt}_{civSO.CivShortName}.asset";
                AssetDatabase.CreateAsset(civSO, assetPath);
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
