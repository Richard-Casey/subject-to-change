using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentWeek = 1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartNewGame()
    {
        currentWeek = 1;
        Debug.Log($"🎮 Starting new game for {GameSetupData.userName} at {GameSetupData.company}");

        // ONLY update persistent data here, no scene-specific references
        // e.g., BookingSystem may need to be moved to MainGame scene or use persistent data
    }
}
