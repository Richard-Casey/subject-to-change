using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class LoadGameManager : MonoBehaviour
{
    public Transform slotListParent;
    public GameObject slotButtonPrefab;
    public TMP_Text noSavesText;
    public Button continueButton;
    public GameObject confirmDeletePanel;
    public Button confirmDeleteButton;
    public Button cancelDeleteButton;

    private const int SlotCount = 10;
    private int selectedSlotIndex = -1;
    private int slotToDelete = -1;

    void Start()
    {
        PopulateSlotList();
    }

    void PopulateSlotList()
    {
        // Clear existing slots
        foreach (Transform child in slotListParent)
        {
            Destroy(child.gameObject);
        }

        // Create 10 slot buttons
        for (int i = 1; i <= 10; i++)
        {
            int slotIndex = i;
            GameObject slotButton = Instantiate(slotButtonPrefab, slotListParent);
            
            // Get the text component and set the slot info
            TMP_Text buttonText = slotButton.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                SaveGameData existingData = SaveSystem.LoadGame(i);
                buttonText.text = existingData != null ? existingData.saveName : $"Empty Slot {i}";
            }

            // Add click listener for loading
            Button button = slotButton.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => OnSlotClicked(slotIndex));
            }
        }
    }

    void OnSlotClicked(int slotIndex)
    {
        SaveGameData data = SaveSystem.LoadGame(slotIndex);
        if (data != null)
        {
            selectedSlotIndex = slotIndex;
            Debug.Log($"Selected save slot {slotIndex}: {data.saveName}");
        }
        else
        {
            Debug.Log($"Slot {slotIndex} is empty");
        }
    }

    public void OnContinueClicked()
    {
        if (selectedSlotIndex == -1) return;
        SaveGameData data = SaveSystem.LoadGame(selectedSlotIndex);
        if (data != null)
        {
            GameSetupData.userName = data.userName;
            GameSetupData.company = data.companyShortName;
            GameSetupData.difficulty = data.difficulty;
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
        }
    }

    public void OnDeleteSlotClicked(int slotIndex)
    {
        slotToDelete = slotIndex;
        confirmDeletePanel.SetActive(true);
    }

    public void OnConfirmDeleteClicked()
    {
        if (slotToDelete != -1)
        {
            SaveSystem.DeleteSave(slotToDelete);
            confirmDeletePanel.SetActive(false);
            PopulateSlotList(); // Refresh the slot list
        }
    }

    public void OnCancelDeleteClicked()
    {
        confirmDeletePanel.SetActive(false);
        slotToDelete = -1;
    }
}