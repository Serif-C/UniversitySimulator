using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
     public int gridWidth = 10;
     public  int gridHeight = 10;
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

    public bool IsAreaOccupied(int startX, int startY, int width, int height)
    {
        for (int x = startX; x < startX + width; x++)
        {
            for (int y = startY; y < startY + height; y++)
            {
                if (x < 0 || x >= gridWidth || y < 0 || y >= gridHeight || occupiedTiles[x, y])
                    return true; // If any cell is occupied or out of bounds, return true
            }
        }
        return false;
    }

    public void SetAreaOccupied(int startX, int startY, int width, int height, bool isOccupied)
    {
        for (int x = startX; x < startX + width; x++)
        {
            for (int y = startY; y < startY + height; y++)
            {
                if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
                    occupiedTiles[x, y] = isOccupied;
            }
        }
    }

    public GameObject GetTileAt(int x, int y)
    {
        if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
            return tiles[x, y];
        return null;
    }

    ///////////// Setters and Getters ///////////////
    
    public int GetGridWidth()
    {
        return gridWidth;
    }

    public int GridHeight()
    {
        return gridHeight;
    }
}
