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
    private (int, int) lastHighlightedTile = (-1, -1);

    private void Start()
    {
        originPosition = transform.position;
        tiles = new GameObject[gridWidth, gridHeight];
        occupiedTiles = new bool[gridWidth, gridHeight]; // Initialize occupancy array
        InitializeTiles();
    }

    private void InitializeTiles()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 cellPosition = GetWorldPosition(x, y);
                GameObject tile = Instantiate(tilePrefab, cellPosition, Quaternion.identity, transform);
                tile.name = $"Tile_{x}_{y}";
                tiles[x, y] = tile;
            }
        }
    }

    public Vector3 SnapToGrid(Vector3 worldPosition)
    {
        float x = (worldPosition.x / (cellSize / 2) + worldPosition.y / (cellSize / 4)) / 2;
        float y = (worldPosition.y / (cellSize / 4) - worldPosition.x / (cellSize / 2)) / 2;

        int gridX = Mathf.RoundToInt(x);
        int gridY = Mathf.RoundToInt(y);

        return GetWorldPosition(gridX, gridY);
    }

    public void HighlightTile(Vector3 worldPosition)
    {
        var (x, y) = GetGridPosition(worldPosition);

        if (x >= 0 && y >= 0 && x < gridWidth && y < gridHeight)
        {
            // Reset the previously highlighted tile if it's different
            if (lastHighlightedTile != (-1, -1) && lastHighlightedTile != (x, y))
            {
                int lx = lastHighlightedTile.Item1;
                int ly = lastHighlightedTile.Item2;
                ResetTileColor(lx, ly);
            }

            SpriteRenderer renderer = tiles[x, y].GetComponent<SpriteRenderer>();

            // Highlight valid tile as green, occupied tile as red
            if (IsTileOccupied(x, y))
                renderer.color = Color.red;
            else
                renderer.color = Color.green;

            lastHighlightedTile = (x, y);
        }
    }

    public void ResetTileColor(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < gridWidth && y < gridHeight)
        {
            SpriteRenderer renderer = tiles[x, y].GetComponent<SpriteRenderer>();

            // Reset to default color **even after placing an object**
            Color color = Color.cyan;
            color.a = 0.109f;
            renderer.color = color;
        }
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        float worldX = (x - y) * (cellSize / 2);
        float worldY = (x + y) * (cellSize / 4);
        return new Vector3(worldX, worldY, 0) + originPosition;
    }

    public (int, int) GetGridPosition(Vector3 worldPosition)
    {
        float x = (worldPosition.x / (cellSize / 2) + worldPosition.y / (cellSize / 4)) / 2;
        float y = (worldPosition.y / (cellSize / 4) - worldPosition.x / (cellSize / 2)) / 2;
        return (Mathf.FloorToInt(x), Mathf.FloorToInt(y));
    }

    public bool IsTileOccupied(int x, int y)
    {
        if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
            return occupiedTiles[x, y];
        return true;
    }

    public void SetTileOccupied(int x, int y, bool isOccupied)
    {
        if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
            occupiedTiles[x, y] = isOccupied;
    }
}
