using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CompanySelectionManager : MonoBehaviour
{
    [Header("UI References")]
    public Transform companyListParent; // Content object of the ScrollView
    public GameObject companyButtonPrefab; // Prefab for each company button
    public TMP_Text companyNameText;
    public TMP_Text companySizeText;
    public TMP_Text companyMoneyText;
    public TMP_Text companyImageText;
    public TMP_Text companyOwnerText;
    public TMP_Text companyRosterText;
    public TMP_Text companyStaffText;
    public TMP_Text companyTVText;
    public TMP_Text companyBeltsText;

    private List<Company> companies;
    private int selectedCompanyIndex = 0;

    void Start()
    {
        // Load companies
        TextAsset json = Resources.Load<TextAsset>("Databases/Companies");
        companies = JsonHelper.FromJson<Company>(json.text);

        // Sort alphabetically
        companies.Sort((a, b) => a.fullName.CompareTo(b.fullName));

        // Populate the company list
        for (int i = 0; i < companies.Count; i++)
        {
            int index = i;
            GameObject btnObj = Instantiate(companyButtonPrefab, companyListParent);
            TMP_Text btnText = btnObj.GetComponentInChildren<TMP_Text>();
            btnText.text = companies[i].fullName;
            btnObj.GetComponent<Button>().onClick.AddListener(() => OnCompanySelected(index));
        }

        ShowCompanyDetails(0);
    }

    public void OnCompanySelected(int index)
    {
        selectedCompanyIndex = index;
        ShowCompanyDetails(index);
    }

    void ShowCompanyDetails(int index)
    {
        var c = companies[index];
        companyNameText.text = c.fullName;
        companySizeText.text = $"Size: {c.size}";
        companyMoneyText.text = $"Starting Money: ${c.startingMoney:N0}";
        companyImageText.text = $"Public Image: {c.publicImage}%";
        companyOwnerText.text = $"Owner: {c.owner}";
        companyRosterText.text = $"Workers:\n{string.Join("\n", c.roster)}";
        companyStaffText.text = $"Staff:\n{string.Join("\n", c.staff)}";
        companyTVText.text = $"TV Shows:\n{string.Join("\n", c.tvShows)}";
        companyBeltsText.text = $"Belts:\n{string.Join("\n", c.belts)}";
    }

    public void OnBeginGameClicked()
    {
        // Set selected company
        GameSetupData.company = companies[selectedCompanyIndex].shortName;

        // Load MainGame scene
        SceneManager.LoadScene("MainGame");
    }
}
