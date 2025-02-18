using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sim : MonoBehaviour
{
    public SimNeeds Needs { get; private set; }

    public Slider HungerSlider;
    public Slider EnergySlider;
    public Slider BladderSlider;
    public Slider FunSlider;
    public Slider SocialSlider;
    public Slider HygieneSlider;
    public Slider ComfortSlider;
    public Slider EnvironmentSlider;

    void Start()
    {
        var sliders = new Dictionary<string, Slider>
        {
            { "Hunger", HungerSlider },
            { "Energy", EnergySlider },
            { "Bladder", BladderSlider },
            { "Fun", FunSlider },
            { "Social", SocialSlider },
            { "Hygiene", HygieneSlider },
            { "Comfort", ComfortSlider },
            { "Environment", EnvironmentSlider }
        };

        // Initialize SimNeeds with sliders
        Needs = new SimNeeds(sliders, this);
    }

    void Update()
    {
        Needs.UpdateNeeds(Time.deltaTime);

        if (Needs.IsNeedCritical("Hunger"))
        {   
            Debug.Log("Sim is starving!");
        }
    }

    public void EatFood(float foodValue)
    {
        Needs.ModifyNeed("Hunger", foodValue);
    }

    public void Energize(ref float energyValue, ref float duration)
    {
        //Needs.ModifyNeed("Energy", energyValue);
        Needs.ModifyNeedOverTime("Energy", energyValue, duration);
        Debug.Log("Energy + " +  energyValue);
    }
}
