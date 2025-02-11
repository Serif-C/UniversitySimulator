using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObjects : MonoBehaviour
{
    [SerializeField] private Sprite highlight;
    private SpriteRenderer objSprite;
    private Sprite original;
    private bool isHighlighted = false;

    private void Start()
    {
        original = GetComponent<SpriteRenderer>().sprite;
    }
    private void OnMouseEnter()
    {
        isHighlighted = true;

        objSprite = gameObject.GetComponent<SpriteRenderer>();
        objSprite.sprite = highlight;
    }

    private void OnMouseExit()
    {
        isHighlighted = false;

        objSprite.sprite = original;
    }

    public bool IsHighlighted()
    {
        return isHighlighted;
    }
}
