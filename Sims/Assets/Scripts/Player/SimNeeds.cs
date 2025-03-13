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
            { "Environment", new Need("Environment", 100f, 0.3f, sliders["Environment"]) },
            { "Mood", new Need("Mood", 100f, 0.3f, sliders["Fun"]) }, // Mood is heavily influenced by Fun
            { "GamingSatisfaction", new Need("Gaming Satisfaction", 100f, 0.4f, sliders["Fun"]) } // Drops if the Sim loses
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

    public void AdjustMoodBasedOnFun()
    {
        if (Needs.ContainsKey("Fun") && Needs.ContainsKey("Mood"))
        {
            float funValue = Needs["Fun"].GetCurrentValue();
            float moodValue = Needs["Mood"].GetCurrentValue();

            // Fun acts as a multiplier for Mood changes
            float moodAdjustment = (funValue - 50f) * 0.5f;
            Needs["Mood"].ModifyNeed(moodAdjustment);
        }
    }

    public void RankedGameResult(bool won)
    {
        if (Needs.ContainsKey("GamingSatisfaction") && Needs.ContainsKey("Mood"))
        {
            float change = won ? 20f : -30f; // Winning increases satisfaction, losing significantly decreases it
            Needs["GamingSatisfaction"].ModifyNeed(change);

            // Mood is also impacted
            Needs["Mood"].ModifyNeed(change * 0.7f); // Losing is a stronger mood penalty
        }
    }

    public bool AttemptStudy()
    {
        if (Needs.ContainsKey("Mood"))
        {
            float moodValue = Needs["Mood"].GetCurrentValue();

            // Higher mood increases study effectiveness
            float successChance = Mathf.Clamp((moodValue / 100f), 0.2f, 1.0f); // Min 20% chance, Max 100%

            if (Random.value < successChance)
            {
                Debug.Log("Study session was successful!");
                return true;
            }
            else
            {
                Debug.Log("Failed to concentrate...");
                return false;
            }
        }
        return false;
    }

    public bool WillDisobey()
    {
        if (Needs.ContainsKey("Mood") && Needs.ContainsKey("GamingSatisfaction"))
        {
            float moodValue = Needs["Mood"].GetCurrentValue();
            float gamingSatisfaction = Needs["GamingSatisfaction"].GetCurrentValue();

            if (moodValue < 40 && gamingSatisfaction < 50) // Low mood and low gaming satisfaction
            {
                float disobedienceChance = 1f - (moodValue / 50f); // Higher chance when mood is lower
                return Random.value < disobedienceChance; // Roll a chance
            }
        }
        return false;
    }
}
