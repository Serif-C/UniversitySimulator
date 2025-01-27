using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    private GridManager gridManager;

    private void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            Vector3 snappedPosition = gridManager.SnapToGrid(mousePosition);
            transform.position = snappedPosition;

            bool isValid = CheckPlacementValidity(snappedPosition);
            gridManager.HighlightTile(snappedPosition, isValid);

            if( isValid && Input.GetMouseButtonDown(0))
            {
                PlaceObject(snappedPosition);
            }
        }
    }

    private bool CheckPlacementValidity(Vector3 position)
    {
        // Add logic to check if the tile is occupied
        return true; // Placeholder
    }

    private void PlaceObject(Vector3 position)
    {
        transform.position = position;
        // Lock this object in place
        enabled = false;
    }
}
