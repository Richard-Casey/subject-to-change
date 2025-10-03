using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [Header("Assign prefabs here")]
    public GameObject gameManagerPrefab;
    public GameObject playerProfileManagerPrefab;

    private void Awake()
    {
        // Spawn persistent GameManager if it doesn't exist
        if (GameManager.Instance == null && gameManagerPrefab != null)
        {
            Instantiate(gameManagerPrefab);
        }

        // Spawn PlayerProfileManager if it doesn't exist
        if (PlayerProfileManager.Instance == null && playerProfileManagerPrefab != null)
        {
            Instantiate(playerProfileManagerPrefab);
        }
    }
}
