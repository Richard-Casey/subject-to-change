using UnityEngine;
using UnityEngine.UI;
using TMPro; // If using TextMeshPro
using UnityEngine.SceneManagement;
using System.Collections.Generic;

// Remove duplicate Company and CompanyList definitions if they exist elsewhere

public class NewGameManager : MonoBehaviour
{
    public TMP_InputField userNameInput;
    public TMP_Dropdown difficultyDropdown;

    void Start()
    {
        // Load and parse companies.json (array)
        TextAsset json = Resources.Load<TextAsset>("Databases/Companies");
        if (json == null)
        {
            Debug.LogError("Companies.json not found in Resources/Databases!");
            return;
        }
        List<Company> companies = JsonHelper.FromJson<Company>(json.text);
        if (companies == null || companies.Count == 0)
        {
            Debug.LogError("No companies found or JSON malformed!");
            return;
        }
        // Populate dropdown with shortNames
        List<string> options = new List<string>();
        foreach (var company in companies)
        {
            options.Add(company.shortName);
        }
    }

    public void OnStartGameClicked()
    {
        GameSetupData.userName = userNameInput.text;
        GameSetupData.difficulty = difficultyDropdown.options[difficultyDropdown.value].text;
        SceneManager.LoadScene("CompanySelection");
    }
}

// Helper for parsing JSON arrays with Unity's JsonUtility
public static class JsonHelper
{
    public static List<T> FromJson<T>(string json)
    {
        string newJson = "{\"array\":" + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array != null ? new List<T>(wrapper.array) : new List<T>();
    }
    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}