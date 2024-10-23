using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private string saveFilePath;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");
        }
    }

    public void SaveGame(GameData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game saved to " + saveFilePath);
    }

    public GameData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Game loaded from " + saveFilePath);
            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found!");
            return null;
        }
    }
}