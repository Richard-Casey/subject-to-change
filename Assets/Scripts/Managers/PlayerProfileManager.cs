using UnityEngine;

public class PlayerProfileManager : MonoBehaviour
{
    public static PlayerProfileManager Instance { get; private set; }

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

    public void CreateNewProfile()
    {
        Debug.Log("PlayerProfileManager: Creating new profile (stub)");

        // TODO: Here you would assign GameSetupData.userName, difficulty, etc.

        // Spawn GameSessionManager for this new game
        GameSessionManager.Spawn(-1); // -1 or slot index if you have it
    }

    public void PrepareLoadMenu()
    {
        Debug.Log("PlayerProfileManager: Preparing load menu (stub)");

        // TODO: Populate SaveSlotListManager in LoadGame scene if needed
    }

    /// <summary>
    /// Called when the player selects a save slot from LoadGame
    /// </summary>
    public void LoadSlot(int slotIndex)
    {
        Debug.Log($"PlayerProfileManager: Loading slot {slotIndex}");

        // You could load GameSetupData from save here

        // Spawn GameSessionManager for this slot
        GameSessionManager.Spawn(slotIndex);
    }
}
