using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    private GridManager gridManager;
    private bool isPlaced = false;

    private void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
    }

    private void Update()
    {
        if (isPlaced) return; // If already placed, stop updating

        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // Snap the position to the grid
        Vector3 snappedPosition = gridManager.SnapToGrid(mousePosition);
        transform.position = snappedPosition;

        // Get grid coordinates
        var (x, y) = gridManager.GetGridPosition(snappedPosition);

        // Highlight the tile
        bool isValid = !gridManager.IsTileOccupied(x, y); // Valid if not occupied
        gridManager.HighlightTile(snappedPosition, isValid);

        // Place the object when the left mouse button is clicked
        if (Input.GetMouseButtonDown(0) && isValid)
        {
            PlaceObject(snappedPosition, x, y);
        }
    }

    private void PlaceObject(Vector3 position, int x, int y)
    {
        transform.position = position; // Lock the object to the snapped position
        gridManager.SetTileOccupied(x, y, true); // Mark the tile as occupied
        isPlaced = true; // Prevent further updates
        Debug.Log($"Object placed at: {position} on tile ({x}, {y})");
    }
}
