using UnityEngine;

public class SaveManager : MonoBehaviour
{
    #region Lazy Singleton
    private static SaveManager _instance;

    public static SaveManager Instance
    {
        // if we try to access it, check to see if we
        // should create it
        get
        {
            //if an instance doesn't exist, create it
            if(_instance == null)
            {
                _instance = FindAnyObjectByType<SaveManager>();
                // if there still none, create it
                if(_instance == null)
                {
                    GameObject newGO = new GameObject();
                    _instance = newGO.AddComponent<SaveManager>();
                    newGO.name = "DataManager";
                    DontDestroyOnLoad(newGO);
                }
            }
            // return instance to the thing that requested it
            return _instance;
        }
    }
    #endregion

    public SaveData ActiveSaveData { get; private set; } = new SaveData();
    public void Save()
    {
        SaveSystem.SaveToFile(ActiveSaveData);
    }    
    public void Load()
    {
        ActiveSaveData = SaveSystem.LoadFromFile();
    }
    public void ResetSave()
    {
        ActiveSaveData = SaveSystem.CreateNewSaveFile();
    }
}
