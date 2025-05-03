using UnityEngine;
using System.IO;

public static class SaveSystem
{
    public static readonly string SAVE_FOLDER = Application.persistentDataPath + "/";
    public static readonly string FILE_NAME = "save.json";

    public static void SaveToFile(SaveData saveData)
    {
        Debug.Log("Save");
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(SAVE_FOLDER + FILE_NAME, json);
    }

    public static SaveData LoadFromFile()
    {
        Debug.Log("Load");
        SaveData saveData = new SaveData();
        if (DoesSaveFileExist())
        {
            string json = File.ReadAllText(SAVE_FOLDER + FILE_NAME);
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            saveData = CreateNewSaveFile();
        }
        return saveData;
    }
    public static SaveData CreateNewSaveFile()
    {
        SaveData saveData = new SaveData();
        SaveToFile(saveData);
        return saveData;
    }
    public static bool DoesSaveFileExist()
    {
        if (File.Exists(SAVE_FOLDER + FILE_NAME))
            return true;
        else
            return false;
    }
}
