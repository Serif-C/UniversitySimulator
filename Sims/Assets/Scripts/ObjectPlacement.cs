using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    private GridManager gridManager;
    private bool isPlaced = false;
    private (int, int) lastHighlightedTile = (-1, -1);

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject prefab;

    private void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
    }

    private void Update()
    {
        if (isPlaced) return;

        Color color = Color.white;
        color.a = 0.1f;
        spriteRenderer.color = color;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 snappedPosition = gridManager.SnapToGrid(mousePosition);
        transform.position = snappedPosition;

        var (x, y) = gridManager.GetGridPosition(snappedPosition);

        // Reset the last highlighted tile correctly
        if (lastHighlightedTile != (-1, -1) && lastHighlightedTile != (x, y))
        {
            int lx = lastHighlightedTile.Item1;
            int ly = lastHighlightedTile.Item2;
            //gridManager.ResetTileColor(lx, ly);
        }

        // Highlight the tile correctly
        gridManager.HighlightTile(snappedPosition);
        lastHighlightedTile = (x, y);

        // Place object if valid
        if (Input.GetMouseButton(0) && !gridManager.IsTileOccupied(x, y))
        {
            PlaceObject(snappedPosition, x, y);
        }
    }

    private void PlaceObject(Vector3 position, int x, int y)
    {
        transform.position = position;
        gridManager.SetTileOccupied(x, y, true);
        isPlaced = true;

        // Ensure that the placed tile does NOT stay red
        gridManager.ResetTileColor(x, y);

        // Spawn a new object for placement
        GameObject newObject = Instantiate(gameObject, transform.position, Quaternion.identity);
        newObject.GetComponent<ObjectPlacement>().isPlaced = false;

        Debug.Log($"Object placed at: {position} on tile ({x}, {y})");
    }

}
