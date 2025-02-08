using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Need
{
    [SerializeField] private string needName;
    [SerializeField] private float currentValue;
    [SerializeField] private float decayRate;
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;

    public Need (string name, float initialValue, float decayRate)
    {
        this.needName = name;
        this.currentValue = Mathf.Clamp(initialValue, minValue, maxValue);
        this.decayRate = decayRate;
    }

    public void UpdateNeed(float deltaTime)
    {
        this.currentValue = Mathf.Clamp(this.currentValue - decayRate * deltaTime, minValue, maxValue);
    }

    public void ModifyNeed(float amount)
    {
        this.currentValue = Mathf.Clamp(this.currentValue + amount, minValue, maxValue);
    }

    public bool IsCritical(float threshold = 20f)
    {
        return this.currentValue <= threshold;
    }
}
