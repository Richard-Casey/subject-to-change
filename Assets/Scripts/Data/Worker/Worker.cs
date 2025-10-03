using System;

[System.Serializable]
public class InRingSkills
{
    public int brawling;
    public int technical;
    public int highFlying;
}

[System.Serializable]
public class PerformanceSkills
{
    public int charisma;
    public int acting;
    public int selling;
    public int psychology;
}

[System.Serializable]
public class Reliability
{
    public int safety;
    public int consistency;
}

[System.Serializable]
public class Physical
{
    public int stamina;
    public int athleticism;
}

[System.Serializable]
public class Perception
{
    public int experience;
    public int respect;
    public int reputation;
    public int overness;
}

[System.Serializable]
public class Reign
{
    public string startDate;
    public string endDate;
    public string notes;
}

[System.Serializable]
public class TitleHistory
{
    public string titleName;
    public Reign[] reigns;
}

[System.Serializable]
public class Worker
{
    public string name;
    public int age;
    public string gender;
    public string nationality;
    public string gimmick;
    public string alignment;
    public int injuryProne;
    public string bookingPreference;
    public string assignedPush;
    public string tagPartner;

    public InRingSkills inRingSkills;
    public PerformanceSkills performanceSkills;
    public Reliability reliability;
    public Physical physical;
    public Perception perception;

    public string[] traits;
    public TitleHistory[] titlesHeld;
}
