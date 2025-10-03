using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static string GetSavePath(int slot)
    {
        return Path.Combine(Application.persistentDataPath, $"SaveSlot{slot}.json");
    }

    public static void SaveGame(int slot, SaveGameData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetSavePath(slot), json);
    }

    public static SaveGameData LoadGame(int slot)
    {
        string path = GetSavePath(slot);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<SaveGameData>(json);
        }
        return null;
    }

    public static void DeleteSave(int slot)
    {
        string path = GetSavePath(slot);
        if (File.Exists(path))
            File.Delete(path);
    }
}