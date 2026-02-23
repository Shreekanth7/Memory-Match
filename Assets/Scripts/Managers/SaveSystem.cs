using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int score;
    public int rows;
    public int cols;
}

public class SaveSystem : MonoBehaviour
{
    private const string SaveKey = "CardMatchSave";

    /// <summary>
    /// Save current game progress.
    /// </summary>
    public void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Load saved progress if available.
    /// </summary>
    public SaveData Load()
    {
        if (!PlayerPrefs.HasKey(SaveKey))
        {
            return new SaveData { score = 0, rows = 0, cols = 0 };
        }

        string json = PlayerPrefs.GetString(SaveKey);
        return JsonUtility.FromJson<SaveData>(json);
    }

    /// <summary>
    /// Clear saved progress.
    /// </summary>
    public void Clear()
    {
        PlayerPrefs.DeleteKey(SaveKey);
    }
}