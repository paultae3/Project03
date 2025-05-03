using UnityEngine;
using UnityEngine.InputSystem;

public class LevelSaveTester : MonoBehaviour
{
    private void Update()
    {
        // SPACE - print save data
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("Current Score: " + SaveManager.Instance.ActiveSaveData.Score);
        }
        // Q KEY - save
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            SaveManager.Instance.Save();
        }
        // W KEY - load
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            SaveManager.Instance.Load();
        }
        // E KEY - increase score
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            SaveManager.Instance.ActiveSaveData.Score += 1;
        }
        // R KEY - reset score
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            SaveManager.Instance.ResetSave();
            Debug.Log("Score Reset");
        }
    }
}
