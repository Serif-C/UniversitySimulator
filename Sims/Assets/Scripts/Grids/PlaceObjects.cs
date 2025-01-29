using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    [SerializeField] GameObject placeAbleObject;
    private GridManager gridManager;
    private GameObject obj;

    private void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        obj = Instantiate(placeAbleObject);
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 snappedPosition = gridManager.SnapToGrid(mousePosition);
        transform.position = snappedPosition;

        var (x, y) = gridManager.GetGridPosition(snappedPosition);

        if (Input.GetMouseButton(0) && !gridManager.IsTileOccupied(x,y))
        {
            PlaceObject(snappedPosition, x, y);
        }
        else if(Input.GetMouseButton(2) && gridManager.IsTileOccupied(x,y))
        {
            // Implement later a way to delete an object
        }

        HighlightTileCell(snappedPosition, x, y);
    }

    private void PlaceObject(Vector3 position, int x, int y)
    {
        transform.position = position;
        gridManager.SetTileOccupied(x, y, true);

        Instantiate(placeAbleObject, transform.position, Quaternion.identity);
    }

    private void RemoveObject(Vector3 position, int x, int y)
    {
        transform.position = position;
        gridManager.SetTileOccupied(x, y, false);

    }

    private void HighlightTileCell(Vector3 position, int x, int y)
    {
        obj.transform.position = position;
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        obj.GetComponent<SpriteRenderer>().sortingOrder = 5;
        
        if (gridManager.IsTileOccupied(x, y))
        {
            obj.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            obj.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
