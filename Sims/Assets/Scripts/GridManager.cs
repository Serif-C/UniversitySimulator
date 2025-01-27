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

    private void Start()
    {
        originPosition = transform.position;
        tiles = new GameObject[gridWidth, gridHeight];
        InitializeTiles();
    }

    public Vector3 SnapToGrid(Vector3 worldPosition)
    {
        float x = Mathf.Floor((worldPosition.x - originPosition.x) / cellSize) * cellSize + originPosition.x;
        float y = Mathf.Floor((worldPosition.y - originPosition.y) / cellSize) * cellSize + originPosition.y;
        return new Vector3(x, y, 0); // Assume 2D, Z = 0
    }

    private void InitializeTiles()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 cellPosition = GetWorldPosition(x, y);
                Debug.DrawLine(cellPosition, cellPosition + Vector3.right * cellSize, Color.white, 100f);
                Debug.DrawLine(cellPosition, cellPosition + Vector3.up * cellSize, Color.white, 100f);
            }
        }
    }

    public void HighlightTile(Vector3 worldPosition, bool isValid)
    {
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
}
