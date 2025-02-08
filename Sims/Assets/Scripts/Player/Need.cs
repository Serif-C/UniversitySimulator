using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Need
{
    [SerializeField] private string needName;
    [SerializeField] private float currentValue;
    [SerializeField] private float decayRate;
    [SerializeField] private float minValue = 0;
    [SerializeField] private float maxValue = 100;
    private Slider needSlider;

    public Need (string name, float initialValue, float decayRate, Slider slider)
    {
        this.needName = name;
        this.currentValue = Mathf.Clamp(initialValue, minValue, maxValue);
        this.decayRate = decayRate;
        needSlider = slider;

        if (needSlider != null)
        {
            needSlider.minValue = minValue;
            needSlider.maxValue = maxValue;
            needSlider.value = currentValue;
        }
    }

    public void UpdateNeed(float deltaTime)
    {
        this.currentValue = Mathf.Clamp(this.currentValue - decayRate * deltaTime, minValue, maxValue);

        if (needSlider != null)
            needSlider.value = currentValue; // Update UI
    }

    public void ModifyNeed(float amount)
    {
        this.currentValue = Mathf.Clamp(this.currentValue + amount, minValue, maxValue);

        if (needSlider != null)
            needSlider.value = currentValue; // Update UI
    }

    public bool IsCritical(float threshold = 20f)
    {
        return this.currentValue <= threshold;
    }
}
