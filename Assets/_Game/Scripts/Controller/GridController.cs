using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private float cellSizeX;
    [SerializeField] private float cellSizeY;

    [SerializeField] private Vector2 gridOrigin;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private PlayController gameplayManager;

    private int numRows = 5;
    private int numCols = 9;

    private Cell[,] cells;
    private PlantBase[,] plantedPlants;

    private Vector3 mouseWorldPos;
    private RaycastHit2D[] hits;


    public void Start()
    {
        CreateGrid();
    }
    public void CreateGrid()
    {
        cells= new Cell[numCols, numRows];
        for (int x = 0; x < numCols; x++)
        {
            for (int y = 0; y < numRows; y++)
            {
                Vector3 cellPosition = new Vector3(gridOrigin.x + x * cellSizeX, gridOrigin.y + y * cellSizeY, 0);
                GameObject newCell = Instantiate(cellPrefab, cellPosition, Quaternion.identity);
                newCell.transform.SetParent(this.transform);

                Cell cellScript = newCell.GetComponent<Cell>();
                cellScript.Init(x, y);

                cells[x, y] = cellScript;
            }
        }
    }

    public void OnInit()
    {
        plantedPlants = new PlantBase[numCols, numRows];
    }
    
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
        }
    }

    private void HandleMouseClick()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hits = Physics2D.RaycastAll(mouseWorldPos, Vector2.zero);
        if (hits.Length > 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
                Cell cell1 = hit.collider.GetComponent<Cell>();
                if (cell1 != null)
                {
                    gameplayManager.AttemptToPlantAt(cell1);
                    return;
                }
            }
        }
    }

    public bool IsCellOccupied(Cell cell)
    {
        int x = cell.x_coord;
        int y = cell.y_coord;
        if (x >= 0 && x < numCols && y >= 0 && y < numRows)
        {
            return plantedPlants[x, y] != null;
        }
        return false; 
    }

    public void RevomePlant(int x, int y)
    {
        if (plantedPlants[x, y] == null) return;
        plantedPlants[x, y].OnDespawn();
        plantedPlants[x, y] = null;
    }

    public void RevomePlant(PlantBase plant)
    {
        for (int x = 0; x < numCols; x++)
        {
            for (int y = 0; y < numRows; y++)
            {
                if(plantedPlants[x, y] == plant)
                {
                    RevomePlant(x, y);
                }
            }
        }
    }

    public void PlantObjectAt(Cell cell, PlantBase plant)
    {
        if (IsCellOccupied(cell)) return;

        int x = cell.x_coord;
        int y = cell.y_coord;
        SoundManager.Instance.PlaySFX(FX.PlantingClip);

        Vector3 plantPosition = cell.transform.position;
        plant.transform.position = plantPosition;
        plant.OnInit();
        plantedPlants[x, y] = plant;
    }
    
    public void OnDespawn()
    {
        if (plantedPlants != null)
        {
            for (int x = 0; x < numCols; x++)
            {
                for (int y = 0; y < numRows; y++)
                {
                    if (plantedPlants[x, y] != null)
                    {
                        plantedPlants[x, y].OnDespawn();

                    }
                }
            }
        }
    }
}