using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveFun : MonoBehaviour
{
    [SerializeField] private float funValue;
    [SerializeField] private HighlightObjects highlightedObject;

    private Sim sim;

    private void Start()
    {
        sim = FindFirstObjectByType<Sim>();
    }

    private void Update()
    {
        Entertain();
    }

    public void Entertain()
    {
        //if ((highlightedObject.IsHighlighted()))
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        sim.Energize(ref funValue);
        //    }
        //}

    }
}
