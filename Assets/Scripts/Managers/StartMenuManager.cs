using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    // Called by New Game button
    public void OnNewGameClicked()
    {
        if (PlayerProfileManager.Instance != null)
        {
            PlayerProfileManager.Instance.CreateNewProfile();
        }

        SceneManager.LoadScene("NewGame");
    }

    // Called by Load Game button
    public void OnLoadGameClicked()
    {
        if (PlayerProfileManager.Instance != null)
        {
            PlayerProfileManager.Instance.PrepareLoadMenu();
        }

        SceneManager.LoadScene("LoadGame");
    }

    // Return to start menu (this scene)
    public void OnReturnToMainMenuClicked()
    {
        SceneManager.LoadScene("StartMenu");
    }

    // Placeholder actions
    public void OnDatabasesClicked()
    {
        Debug.Log("Databases Clicked");
    }

    public void OnSettingsClicked()
    {
        Debug.Log("Settings Clicked");
    }

    public void OnCreditsClicked()
    {
        Debug.Log("Credits Clicked");
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
}
