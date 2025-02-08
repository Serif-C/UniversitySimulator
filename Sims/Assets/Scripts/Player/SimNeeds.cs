using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimNeeds
{
    public Dictionary<string, Need> Needs { get; private set; }

    public SimNeeds()
    {
        Needs = new Dictionary<string, Need>()
        {
            {"Hunger", new Need("Hunger", 100f, 1.2f) },
            { "Energy", new Need("Energy", 100f, 0.8f) },
            { "Bladder", new Need("Bladder", 100f, 1.5f) },
            { "Fun", new Need("Fun", 100f, 0.5f) },
            { "Social", new Need("Social", 100f, 0.7f) },
            { "Hygiene", new Need("Hygiene", 100f, 1.0f) },
            { "Comfort", new Need("Comfort", 100f, 0.6f) },
            { "Environment", new Need("Environment", 100f, 0.3f) }
        };
    }

    public void UpdateNeeds(float deltaTime)
    {
        foreach (var need in Needs.Values)
        {
            need.UpdateNeed(deltaTime);
        }
    }

    public void ModifyNeed(string needName, float amount)
    {
        if (Needs.ContainsKey(needName))
        {
            Needs[needName].ModifyNeed(amount);
        }
    }

    public bool IsNeedCritical(string needName)
    {
        return Needs.ContainsKey(needName) && Needs[needName].IsCritical();
    }
}
