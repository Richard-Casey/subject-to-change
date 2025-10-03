using UnityEngine;
using System.Collections.Generic;

public static class BookingSystem
{
    static List<Worker> wrestlerPool = new List<Worker>();

    public static void BookExampleMatch()
    {
        if (wrestlerPool.Count == 0)
        {
            wrestlerPool = WorkerLoader.LoadAllWorkers();
            Debug.Log($"📥 Loaded {wrestlerPool.Count} workers from JSON.");
        }

        Worker w1 = GetRandomWrestler();
        Worker w2;
        do { w2 = GetRandomWrestler(); } while (w2.name == w1.name);

        Worker winner = SimulateMatch(w1, w2);
        int w1Rating = CalculateOverallRating(w1);
        int w2Rating = CalculateOverallRating(w2);

        Debug.Log($"📖 Booked Match: {w1.name} ({w1Rating}) vs {w2.name} ({w2Rating}) — Winner: {winner.name}");
    }

    static Worker GetRandomWrestler()
    {
        int index = Random.Range(0, wrestlerPool.Count);
        return wrestlerPool[index];
    }

    static Worker SimulateMatch(Worker w1, Worker w2)
    {
        float score1 = CalculateOverallRating(w1) + Random.Range(0f, 20f);
        float score2 = CalculateOverallRating(w2) + Random.Range(0f, 20f);
        return score1 > score2 ? w1 : w2;
    }

    static int CalculateOverallRating(Worker w)
    {
        return Mathf.RoundToInt((
            w.inRingSkills.brawling +
            w.inRingSkills.technical +
            w.inRingSkills.highFlying +
            w.performanceSkills.charisma +
            w.performanceSkills.psychology
        ) / 5f); // basic average for now
    }
}
