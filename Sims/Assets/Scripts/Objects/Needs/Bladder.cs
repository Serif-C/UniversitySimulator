using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bladder : MonoBehaviour
{
    [SerializeField] private float bladderValue;
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
                RelieveBladder();
            }
        }
    }

    public void RelieveBladder()
    {
        sim.BladderRelief(ref bladderValue, ref duration);
    }
}
