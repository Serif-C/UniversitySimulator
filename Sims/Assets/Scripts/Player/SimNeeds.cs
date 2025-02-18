using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimNeeds
{
    public Dictionary<string, Need> Needs { get; private set; }
    private MonoBehaviour coroutineRunner;

    public SimNeeds(Dictionary<string, Slider> sliders, MonoBehaviour coroutineRunner)
    {
        this.coroutineRunner = coroutineRunner;

        Needs = new Dictionary<string, Need>()
        {
            { "Hunger", new Need("Hunger", 100f, 1.2f, sliders["Hunger"]) },
            { "Energy", new Need("Energy", 100f, 1.8f, sliders["Energy"]) },
            { "Bladder", new Need("Bladder", 100f, 1.5f, sliders["Bladder"]) },
            { "Fun", new Need("Fun", 100f, 0.5f, sliders["Fun"]) },
            { "Social", new Need("Social", 100f, 0.7f, sliders["Social"]) },
            { "Hygiene", new Need("Hygiene", 100f, 1.0f, sliders["Hygiene"]) },
            { "Comfort", new Need("Comfort", 100f, 0.6f, sliders["Comfort"]) },
            { "Environment", new Need("Environment", 100f, 0.3f, sliders["Environment"]) }
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

    public void ModifyNeedOverTime(string needName, float amount, float duration)
    {
        if (Needs.ContainsKey(needName) && coroutineRunner != null)
        {
            coroutineRunner.StartCoroutine(Needs[needName].ModifyNeedOverTime(amount, duration, this.coroutineRunner));
        }
    }

    public bool IsNeedCritical(string needName)
    {
        return Needs.ContainsKey(needName) && Needs[needName].IsCritical();
    }
}
