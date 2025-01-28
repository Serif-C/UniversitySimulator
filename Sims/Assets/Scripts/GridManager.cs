using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] int gridWidth = 10;
    [SerializeField] int gridHeight = 10;
    [SerializeField] float cellSize = 1f;
    private Vector3 originPosition;
    [SerializeField] GameObject tilePrefab;
    private GameObject[,] tiles;
    private bool[,] occupiedTiles;

    private void Start()
    {
        originPosition = transform.position;
        tiles = new GameObject[gridWidth, gridHeight];
        occupiedTiles = new bool[gridWidth, gridHeight]; // Initialize occupancy array
        InitializeTiles();
    }

    private void InitializeTiles()
    {
        // Initialize and draw the isometric grid
        for (int x = 0; x <= gridWidth; x++)
        {
            for (int y = 0; y <= gridHeight; y++)
            {
                Vector3 cellPosition = GetWorldPosition(x, y);

                // Instantiate the tile prefab at the calculated cell position
                if (x < gridWidth && y < gridHeight)
                {
                    GameObject tile = Instantiate(tilePrefab, cellPosition, Quaternion.identity, transform);
                    tile.name = $"Tile_{x}_{y}";
                    tiles[x, y] = tile;
                }

                // Draw isometric grid lines (diamond-shaped layout)
                if (x < gridWidth)
                    Debug.DrawLine(cellPosition, GetWorldPosition(x + 1, y), Color.white, 100f);
                if (y < gridHeight)
                    Debug.DrawLine(cellPosition, GetWorldPosition(x, y + 1), Color.white, 100f);
            }
        }
    }

    public Vector3 SnapToGrid(Vector3 worldPosition)
    {
        // Convert world position to grid coordinates
        float x = (worldPosition.x / (cellSize / 2) + worldPosition.y / (cellSize / 4)) / 2;
        float y = (worldPosition.y / (cellSize / 4) - worldPosition.x / (cellSize / 2)) / 2;

        // Round to nearest grid cell
        int gridX = Mathf.RoundToInt(x);
        int gridY = Mathf.RoundToInt(y);

        // Convert grid coordinates back to world position
        return GetWorldPosition(gridX, gridY);
    }

    public void HighlightTile(Vector3 worldPosition, bool isValid)
    {
        // Highlight a specific tile based on validity
        var (x, y) = GetGridPosition(worldPosition);
        if (x >= 0 && y >= 0 && x < gridWidth && y < gridHeight)
        {
            SpriteRenderer renderer = tiles[x, y].GetComponent<SpriteRenderer>();
            renderer.color = isValid ? Color.green : Color.red;
        }
    }


    public Vector3 GetWorldPosition(int x, int y)
    {
        // Convert grid coordinates to isometric world position
        float worldX = (x - y) * (cellSize / 2);
        float worldY = (x + y) * (cellSize / 4); // Divide height by 2 for isometric scaling
        return new Vector3(worldX, worldY, 0) + originPosition;
    }

    public (int, int) GetGridPosition(Vector3 worldPosition)
    {
        // Convert isometric world position to grid coordinates
        float x = (worldPosition.x / (cellSize / 2) + worldPosition.y / (cellSize / 4)) / 2;
        float y = (worldPosition.y / (cellSize / 4) - worldPosition.x / (cellSize / 2)) / 2;
        return (Mathf.FloorToInt(x), Mathf.FloorToInt(y));
    }

    public bool IsTileOccupied(int x, int y)
    {
        if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
            return occupiedTiles[x, y];
        return true; // Treat out-of-bounds tiles as occupied
    }

    public void SetTileOccupied(int x, int y, bool isOccupied)
    {
        if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
            occupiedTiles[x, y] = isOccupied;
    }
}
