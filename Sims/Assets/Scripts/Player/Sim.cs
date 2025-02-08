using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim : MonoBehaviour
{
    public SimNeeds Needs { get; private set; }

    void Start()
    {
        Needs = new SimNeeds();
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
}
