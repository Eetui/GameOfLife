using UnityEngine;

public class CellGrid : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;

    private Cell[,] cellGrid;

    [SerializeField] private bool randomStart;
    [Range(1,100)] [SerializeField] private int cellsAliveInStart;

    [SerializeField] private GameObject cellObject;
    [SerializeField] private Color32 gizmosColor;

    private void Awake()
    {
        cellGrid = new Cell[width, height];
        PopulateCellGrid();
        SearchForNeighbours();
    }

    private void PopulateCellGrid()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var cellObj = Instantiate(cellObject, new Vector2(x, y), Quaternion.identity);
                cellObj.name = $"Cell({x}, {y})";
                var cell = cellObj.GetComponent<Cell>();

                cell.randomStartState = randomStart;
                cell.alivePercent = cellsAliveInStart;
                cellGrid[x, y] = cell;
            }
        }
    }

    private void SearchForNeighbours()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++) 
            {
                if (x - 1 >= 0)
                {
                    cellGrid[x, y].neighbours.Add(cellGrid[x - 1, y]); // left

                    if (y + 1 <= height - 1)
                    {
                        cellGrid[x, y].neighbours.Add(cellGrid[x - 1, y + 1]); //top left
                    }

                    if (y - 1 >= 0)
                    {
                        cellGrid[x, y].neighbours.Add(cellGrid[x - 1, y - 1]); //bottom left
                    }
                }

                if (x + 1 <= width - 1)
                {
                    cellGrid[x, y].neighbours.Add(cellGrid[x + 1, y]); //right

                    if (y + 1 <= height - 1)
                    {
                        cellGrid[x, y].neighbours.Add(cellGrid[x + 1, y + 1]); //top right
                    }

                    if (y - 1 >= 0)
                    {
                        cellGrid[x, y].neighbours.Add(cellGrid[x + 1, y - 1]); //bottom right
                    }
                }

                if (y - 1 >= 0)
                {
                    cellGrid[x, y].neighbours.Add(cellGrid[x, y - 1]); // bottom
                }

                if (y + 1 <= height - 1)
                {
                    cellGrid[x, y].neighbours.Add(cellGrid[x, y + 1]); // top
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        int y = 0;
        int x = 0;

        Gizmos.color = gizmosColor;
        for (; y < height+1; y++)
        {
            Gizmos.DrawLine(new Vector3(0 - 0.5f, y - 0.5f), new Vector3(width - 0.5f, y - 0.5f));

            for (; x < width+1; x++)
            {
                Gizmos.DrawLine(new Vector3(x - 0.5f, 0 - 0.5f),new Vector3(x - 0.5f, height - 0.5f));
            }
        
        }
    }
}
