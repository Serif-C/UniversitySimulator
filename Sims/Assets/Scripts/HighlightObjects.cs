using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObjects : MonoBehaviour
{
    [SerializeField] private Sprite highlight;
    private SpriteRenderer objSprite;
    private Sprite original;

    private void Start()
    {
        original = GetComponent<SpriteRenderer>().sprite;
    }
    private void OnMouseEnter()
    {
        objSprite = gameObject.GetComponent<SpriteRenderer>();
        objSprite.sprite = highlight;
    }

    private void OnMouseExit()
    {
        objSprite.sprite = original;
    }
}
