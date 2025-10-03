using System.Collections.Generic;
using UnityEngine;

public static class WorkerLoader
{
    public static Worker LoadWorker(string fileName)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(fileName);
        if (jsonFile == null)
        {
            Debug.LogError($"❌ WorkerLoader: Couldn't find file: {fileName}.json — Check path and Resources folder!");
            return null;
        }

        Worker worker = JsonUtility.FromJson<Worker>(jsonFile.text);
        return worker;
    }

    public static List<Worker> LoadAllWorkers()
    {
        List<Worker> workers = new List<Worker>();
        TextAsset[] jsonFiles = Resources.LoadAll<TextAsset>("Databases/Workers");

        foreach (TextAsset json in jsonFiles)
        {
            try
            {
                Worker worker = JsonUtility.FromJson<Worker>(json.text);
                workers.Add(worker);
            }
            catch
            {
                Debug.LogError($"❌ Failed to parse worker from file: {json.name}");
            }
        }

        Debug.Log($"📦 Total Workers Loaded: {workers.Count}");
        return workers;
    }
}
