using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSessionManager : MonoBehaviour
{
    public static GameSessionManager Instance { get; private set; }

    [SerializeField] private int currentSlotId;

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

    public static void Spawn(int slotId)
    {
        if (Instance != null) Destroy(Instance.gameObject);

        // Load prefab from Resources (place your prefab there)
        GameObject prefab = Resources.Load<GameObject>("GameSessionManager");
        GameObject sessionObj = Instantiate(prefab);
        var session = sessionObj.GetComponent<GameSessionManager>();
        session.currentSlotId = slotId;

        // Load the main game scene (replace with your scene name)
        SceneManager.LoadScene("MainGameScene");
    }

    public void QuitToMenu()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("StartMenu");
    }
}
