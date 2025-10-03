using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SimpleSaveManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject saveOverlayPanel;
    public Transform slotListParent;
    public GameObject slotButtonPrefab;
    public TMP_InputField saveNameInput;
    public Button confirmButton;
    public Button cancelButton;

    private int selectedSlotIndex = -1;

    void Start()
    {
        if (confirmButton != null)
            confirmButton.onClick.AddListener(OnConfirmSave);
        if (cancelButton != null)
            cancelButton.onClick.AddListener(OnCancelSave);
    }

    public void OnSaveGameButtonClicked()
    {
        saveOverlayPanel.SetActive(true);
        PopulateSlotList();
    }

    void PopulateSlotList()
    {
        foreach (Transform child in slotListParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 1; i <= 10; i++)
        {
            int slotIndex = i;
            GameObject slotButton = Instantiate(slotButtonPrefab, slotListParent);

            TMP_Text buttonText = slotButton.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                SaveGameData existingData = SaveSystem.LoadGame(i);
                buttonText.text = existingData != null ? existingData.saveName : $"Empty Slot {i}";
            }

            Button button = slotButton.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => OnSlotClicked(slotIndex));
            }
        }
    }

    void OnSlotClicked(int slotIndex)
    {
        selectedSlotIndex = slotIndex;

        SaveGameData existingData = SaveSystem.LoadGame(slotIndex);
        if (existingData != null && saveNameInput != null)
        {
            saveNameInput.text = existingData.saveName;
        }
        else if (saveNameInput != null)
        {
            saveNameInput.text = "";
        }

        Debug.Log($"Selected slot {slotIndex} for saving");
    }

    void OnConfirmSave()
    {
        if (selectedSlotIndex == -1)
        {
            Debug.LogWarning("No slot selected!");
            return;
        }

        string saveName = saveNameInput.text.Trim();
        if (string.IsNullOrEmpty(saveName))
        {
            Debug.LogWarning("Cannot save with empty name!");
            return;
        }

        SaveGameData data = new SaveGameData
        {
            saveName = saveName,
            userName = GameSetupData.userName,
            companyShortName = GameSetupData.company,
            difficulty = GameSetupData.difficulty,
            currentWeek = 1,
            saveTime = DateTime.Now
        };

        SaveSystem.SaveGame(selectedSlotIndex, data);
        Debug.Log($"Game saved to slot {selectedSlotIndex}: {saveName}");

        saveOverlayPanel.SetActive(false);
        selectedSlotIndex = -1;
        saveNameInput.text = "";
    }

    void OnCancelSave()
    {
        saveOverlayPanel.SetActive(false);
        selectedSlotIndex = -1;
        saveNameInput.text = "";
    }
}