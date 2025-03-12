using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hygiene : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float hygieneValue;
    [SerializeField] private float energyValue;
    [SerializeField] private HighlightObjects highlightedObject;
    [SerializeField] private float duration;

    private Sim sim;

    private void Start()
    {
        sim = FindFirstObjectByType<Sim>();
    }

    private void Update()
    {
        if ((highlightedObject.IsHighlighted()))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (sim.IsTakingShower()) { Shower(); }
                else if (sim.IsTakingBath()) { Bath(); }
            }
        }
    }

    public void Shower()
    {
        sim.TakeShower(ref hygieneValue, ref duration);
    }

    public void Bath()
    {
        sim.TakeBath(ref hygieneValue, ref energyValue, ref duration);
    }
}
