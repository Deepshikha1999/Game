using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class GameGrid : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    string[] tetrominoNames = {"Tile1" , "Tile2" , "Tile3" , "Tile4" , "Tile5" , "Tile6" , "Tile7" };
    private BoxCollider2D grid;
    public static int gridWidth;
    public static int gridHeight;
    public static Transform[,] gridArr;
    Bounds bounds;
    void Start()
    {
        grid = GetComponent<BoxCollider2D>();
        bounds = grid.bounds;
        gridWidth = (int)(bounds.max.x - bounds.min.x);
        gridHeight = (int)(bounds.max.y - bounds.min.y);
        gridArr = new Transform[gridWidth, gridHeight];
        Debug.Log(gridWidth + " : " + gridHeight);
        //Debug.Log(Mathf.Round(gridWidth/2) + " : " + Mathf.Round(gridHeight/2));
        //Debug.Log(gridArr.Length);
        SpawnNextTetromino();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsFullRowAt(int y)
    {
        for(int x = 0; x < gridWidth; x++)
        {
            if (gridArr[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }

    public void DeleteMinoAt(int y)
    {
        for(int x = 0; x < gridWidth; x++)
        {
            Destroy(gridArr[x, y].gameObject);
            gridArr[x, y] = null;
        }
    }

    public void MoveRowDown(int y)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            if (gridArr[x, y] != null)
            {
                gridArr[x, y - 1] = gridArr[x, y];
                gridArr[x, y] = null;
                gridArr[x, y - 1].position += Vector3.down;
            }
        }
    }

    public void MoveAllRowDown(int y)
    {
        for (int i = y; i > 0; i--)
        {
            MoveRowDown(i);
        }
    }

    public void DeleteRow()
    {
        for(int y = 0; y < gridHeight; y++)
        {
            if (IsFullRowAt(y))
            {
                DeleteMinoAt(y);
                MoveAllRowDown(y);
                y--;
            }
        }
    }

    public void UpdateGrid(TetrisPieceMove tetromino)
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                if (gridArr[x, y] != null)
                {
                    if (gridArr[x, y].parent == tetromino.transform)
                    {
                        gridArr[x, y] = null;

                    }
                }
            }
        }

        foreach (Transform mino in tetromino.transform)
        {
            Vector2 pos = Round(mino.position,new Vector2(0,0));
            Vector2Int coords = WorldToGrid(pos);
            if(coords.x<gridWidth && coords.x >= 0 && coords.y< gridHeight && coords.y >= 0)
                gridArr[coords.x, coords.y] = mino;
        }
        //RemoveCompletedLine();
    }

    private Vector2Int WorldToGrid(Vector2 worldPos)
    {
        int x = (int)(worldPos.x) + Mathf.RoundToInt(gridWidth / 2);
        int y = Mathf.RoundToInt(gridHeight / 2) - (int)(worldPos.y) - 1;
        return new Vector2Int(x, y);
    }

    public void SpawnNextTetromino()
    {
        GameObject nextTetromino = (GameObject)Instantiate(Resources.Load(SelectNextPrefabMino(), typeof(GameObject)),new Vector2(0.0f,10.0f),Quaternion.identity);
        // Note: The prefabs should be placed inside Resources folder so that to be accessed by Resources.Load
    }

    public bool CheckInGrid(Vector2 pos)
    {
        return (pos.x <= bounds.max.x && pos.x >= bounds.min.x) && (pos.y <= bounds.max.y && pos.y >= bounds.min.y);
    }

    public bool CheckPosInGrid(Vector2 pos, TetrisPieceMove tetromino)
    {
        Vector2Int gridPos = WorldToGrid(pos);
        bool validGridPos = gridPos.x < gridWidth && gridPos.x >= 0 && gridPos.y < gridHeight && gridPos.y >= 0;
        return validGridPos && (gridArr[gridPos.x, gridPos.y] == null || gridArr[gridPos.x, gridPos.y].parent == tetromino.transform);
    }
    public Vector2 Round(Vector2 pos,Vector2 valI)
    {
        return new Vector2(Mathf.Round(pos.x) + valI.x, Mathf.Round(pos.y) + valI.y);
    }

    public string SelectNextPrefabMino()
    {
        int n = Random.Range(0, tetrominoNames.Length);
        //Debug.Log(tetrominoNames[n - 1]);
        return tetrominoNames[n];
    }

    private void PrintGrid()
    {
        Debug.Log("**********");
        string str = "";
        for (int y = 0; y < gridHeight; y++)
        {
            string row = "";
            for (int x = 0; x < gridWidth; x++)
            {
                row += (gridArr[x, y] != null ? "X" : ".") + " ";
            }
            str += row + "\n";
        }
        Debug.Log(str);
        Debug.Log("**********");
    }

}