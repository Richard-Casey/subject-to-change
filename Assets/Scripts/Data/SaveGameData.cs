using System;

[Serializable]
public class SaveGameData
{
    public string saveName;
    public string userName;
    public string companyShortName;
    public string difficulty;
    public int currentWeek;
    public DateTime saveTime;
    // Add more fields as needed for your game state
}