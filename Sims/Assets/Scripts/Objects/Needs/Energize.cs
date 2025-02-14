using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energize : MonoBehaviour
{
    [SerializeField] private float energyValue;
    [SerializeField] private HighlightObjects highlightedObject;

    private Sim sim;

    private void Start()
    {
        sim = FindFirstObjectByType<Sim>();
    }

    private void Update()
    {
        GiveEnergy();
    }

    public void GiveEnergy()
    {
        if ((highlightedObject.IsHighlighted()))
        {
            if (Input.GetMouseButtonDown(0))
            {
                sim.Energize(ref energyValue);
            }
        }

    }
}
