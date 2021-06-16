using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CellManager : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private Vector2 startPositionGrid;
    [SerializeField] private float distanceBetweenCells;
    [SerializeField] private float timeBetweenCellUpdates;
    [SerializeField] private int chanceToBecomeAlive;

    private Cell[,] cells;
    private float timer;

    private void Start()
    {
        cells = new Cell[gridSize.x, gridSize.y];

        for(int y = 0; y < gridSize.y; y++)
        {
            for(int x = 0; x < gridSize.x; x++)
            {
                Vector2 position = new Vector2(x + distanceBetweenCells * x, y + distanceBetweenCells * y);
                position += startPositionGrid;
                GameObject c = Instantiate(cellPrefab, position, Quaternion.identity);
                Cell cell = c.GetComponent<Cell>();

                cells[x, y] = cell;

                int roll = Random.Range(0, 100);
                if(roll <= chanceToBecomeAlive)
                {
                    cell.IsAlive = true;
                }
            }
        }

        SendNeighbours();
    }

    private void Update()
    {
        timer += 1f * Time.deltaTime;

        // Time between cell updates
        if(timer >= timeBetweenCellUpdates)
        {
            for(int y = 0; y < gridSize.y; y++)
            {
                for(int x = 0; x < gridSize.x; x++)
                {
                    cells[x, y].CheckNeighbours();
                }
            }

            for(int y = 0; y < gridSize.y; y++)
            {
                for(int x = 0; x < gridSize.x; x++)
                {
                    cells[x, y].UpdateState();
                }
            }

            timer = 0f;
        }

        // Reset the current grid with random rolls
        if(Input.GetKeyDown(KeyCode.Space))
        {
            for(int y = 0; y < gridSize.y; y++)
            {
                for(int x = 0; x < gridSize.x; x++)
                {
                    int roll = Random.Range(0, 100);

                    if(roll <= chanceToBecomeAlive)
                    {
                        cells[x, y].ForceState(true);
                    }
                    else
                    { 
                        cells[x, y].ForceState(false);
                    }
                }
            }

            timer = 0f;
        }
    }

    private void SendNeighbours()
    {
        for(int y = 1; y < gridSize.y - 1; y++)
        {
            for(int x = 1; x < gridSize.x - 1; x++)
            {
                // Add the neighbours around each cell
                for(int i = -1; i <= 1; i++)
                {
                    for(int j = -1; j <= 1; j++)
                    {
                        // Cells shouldn't add themselves as neighbour
                        if(cells[x + i, y + j] != cells[x, y]) 
                        {
                            cells[x, y].ReceiveNeighbour(cells[x + i, y + j]);
                        }
                    }
                }
            }
        }
    }
}