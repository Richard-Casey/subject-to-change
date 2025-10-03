using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainGameUIManager : MonoBehaviour
{
    public TMP_Text userNameText;
    public TMP_Text companyText;
    public TMP_Text difficultyText;

    void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Called automatically whenever a scene finishes loading
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Only run initialization for the MainGame scene
        if (scene.name != "MainGame") return;

        InitializeUI();
    }

    void InitializeUI()
    {
        // Assign persistent GameSetupData to UI
        userNameText.text = GameSetupData.userName;
        difficultyText.text = $"Current Difficulty: {GameSetupData.difficulty}";

        // Load companies.json safely
        TextAsset json = Resources.Load<TextAsset>("Databases/Companies");
        if (json == null)
        {
            Debug.LogError("Companies.json not found in Resources/Databases!");
            return;
        }

        List<Company> companies = JsonUtility.FromJson<CompanyList>("{\"companies\":" + json.text + "}").companies;

        // Find the selected company by shortName
        Company selectedCompany = companies.Find(c => c.shortName == GameSetupData.company);
        companyText.text = selectedCompany != null ? selectedCompany.fullName : GameSetupData.company;
    }
}

// Serializable wrapper for JSON parsing
[System.Serializable]
public class CompanyList
{
    public List<Company> companies;
}
