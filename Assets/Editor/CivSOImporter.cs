using UnityEngine;
using UnityEditor;
using System.IO;
using Assets.Core;
using System;
using Unity.VisualScripting;
using System.Diagnostics.Eventing.Reader;

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

            if (fields.Length >8)
            {
                CivSO civSO = CreateInstance<CivSO>();
                civSO.CivInt = int.Parse(fields[0]);
                civSO.CivEnum = GetMyCivEnum(fields[1]);
                civSO.CivShortName= fields[2];
                civSO.CivLongName = fields[3];
                civSO.CivHomeSystem = fields[4];
                civSO.WarlikeToPeaseful = int.Parse(fields[5]);
                civSO.TraitOne = GetCivTraitsEnum(fields[6]);
                civSO.TraitTwo = GetCivTraitsEnum(fields[7]);
                civSO.TraitThree = GetCivTraitsEnum(fields[8]);
                var name = Resources.Load<Sprite>("Insignias/" + fields[2].ToUpper());
                if (name == null) { name = Resources.Load<Sprite>("Insignias/" + fields[2].ToUpper() + "S"); }
                civSO.Insignia = name;
                civSO.CivTechLevel = TechLevel.EARLY;// TechLevel enum
                civSO.HasWarp = bool.Parse(fields[11]);
                civSO.Playable = bool.Parse(fields[12]);
                civSO.Decription = "ToDo, connect to libaray of civSO descriptions";
                //civSO.StarSysOwned = new System.Collections.Generic.List<StarSysController> { new StarSysController("Place Holder") };
                //civSO.ContactList = new System.Collections.Generic.List<CivData>(); // we know our self + maybe a 'Vulcans' for each major??
                string assetPath = $"Assets/SO/CivilizationSO/CivSO_{civSO.CivInt}_{civSO.CivShortName}.asset";
                AssetDatabase.CreateAsset(civSO, assetPath);
                AssetDatabase.SaveAssets();
            }
        }

        Debug.Log("CivSOImporter Import Complete");
    }

    private static CivTraitsEnum GetCivTraitsEnum(string ourString)
    {
        string[] switchTraits = new string[] {"Scientific","Materialistic",
                "Fanatic","Xenophobic","Indifferent","Compassion","Honorable","Ruthless", "Null"};
        if (ourString == switchTraits[0])
            return CivTraitsEnum.Scientific;
        else if (ourString == switchTraits[1])
            return CivTraitsEnum.Materialistic;
        else if (ourString == switchTraits[2])
            return CivTraitsEnum.Fanatic;
        else if (ourString == switchTraits[3])
            return CivTraitsEnum.Xenophobia;
        else if (ourString == switchTraits[4])
            return CivTraitsEnum.Indifferent;
        else if (ourString == switchTraits[5])
            return CivTraitsEnum.Compassion;
        else if (ourString == switchTraits[6])
            return CivTraitsEnum.Honourable;
        else if (ourString == switchTraits[7])
            return CivTraitsEnum.Ruthless;
        else if (ourString == switchTraits[8])
            return CivTraitsEnum.Null;
        else return CivTraitsEnum.Null;
           
        
    }
    public static CivEnum GetMyCivEnum(string title)
    {
        CivEnum st;
        Enum.TryParse(title, out st);
        return st;
    }
} 
