using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSize : MonoBehaviour
{
    [SerializeField] private int objectWidth;
    [SerializeField] private int objectHeight;

    public int GetObjectWidth()
    {
        return objectWidth;
    }

    public int GetObjectHeight()
    {
        return objectHeight;
    }

    public Vector2 Size()
    {
        return new Vector2(objectWidth, objectHeight);
    }
}
