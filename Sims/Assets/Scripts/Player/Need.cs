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

    public IEnumerator ModifyNeedOverTime(float amount, float duration, MonoBehaviour monoBehaviour)
    {
        float startValue = currentValue;
        float targetValue = Mathf.Clamp(this.currentValue + amount, minValue, maxValue);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            currentValue = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);

            if(needSlider != null)
            {
                needSlider.value = currentValue;
            }

            yield return null; // wait for next frame
        }

        // Ensures the exact value is set at the end
        currentValue = targetValue;
        if (needSlider != null)
            needSlider.value = currentValue;
    }

    public bool IsCritical(float threshold = 20f)
    {
        return this.currentValue <= threshold;
    }

    public float GetCurrentValue()
    {
        return this.currentValue;
    }
}
