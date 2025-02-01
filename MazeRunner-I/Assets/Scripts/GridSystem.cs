using UnityEngine;
/*
 * 0: Empty Space
 * 1: Wall
 * 2: Bomb
 * 3: Coins
 * 4: Monsters
 * 5: Player
 * **/
public class GridSystem : MonoBehaviour
{
    int[,] maze = new int[18, 38] {
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
{1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,1},
{1,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,1,1,0,1,1,1,1,1,0,1,1,0,1,1,0,1,1,1,1,1,0,1,1,0,1,1,0,1,1,1,1,1,0,1,1,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1},
{1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,1},
{1,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,1,1,0,1,1,1,1,1,0,1,1,0,1,1,0,1,1,1,1,1,0,1,1,0,1,1,0,1,1,1,1,1,0,1,1,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1},
{1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,1},
{1,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,1,1,0,1,1,1,1,1,0,1,1,0,1,1,0,1,1,1,1,1,0,1,1,0,1,1,0,1,1,1,1,1,0,1,1,0,1},
{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}};

    Transform[,] gridArea = new Transform[38,18];
    [SerializeField] Transform gameObject;
    [SerializeField] Transform coinObject;
    [SerializeField] Transform monsterObject;
    [SerializeField] Transform player;

    int monsterGenerationDelay = 30;
    float monsterStartTime = 0;

    int coinGenerationDelay = 30;
    float coinStartTime = 0;

    int coinsCollected = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0;i<38;i++){
            for(int j = 0;j<18;j++){
                if (maze[j,i] == 1)
                {
                    gridArea[i, j] = Instantiate(this.gameObject);
                    gridArea[i, j].position = new Vector2(i-19,j-9);
                }
            }
        }
        gridArea[(Mathf.RoundToInt(19)), (Mathf.RoundToInt(9))] = Instantiate(player);
        player.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (monsterStartTime == 0 || (Time.time - monsterStartTime >= monsterGenerationDelay))
        {
            for (int i = 0; i < 10; i++)
            {
                GenerateMonsters();
            }
            monsterStartTime = Time.time;
        }

        if (coinStartTime == 0 || (Time.time - coinStartTime >= coinGenerationDelay)) 
        {
            for (int i = 0; i < 10; i++)
            {
                CoinGenerator();
            }
            coinStartTime = Time.time;
        }
        Debug.Log("Coins Collected: " + coinsCollected);

    }

    public bool IsEmptySpace(Vector2Int pos)
    {
        int x = pos.x + 19;
        int y = pos.y + 9;
        return gridArea[x, y] == null;
    }

    public void UpdatePositionInGrid(Vector2Int prev_pos,Vector2Int pos,Transform obj)
    {
        int x = pos.x + 19;
        int y = pos.y + 9;

        if (prev_pos != null)
        {
            int x_prev = prev_pos.x + 19;
            int y_prev = prev_pos.y + 9;
            gridArea[x_prev, y_prev] = null;
        }

        if (pos != null)
        {
            gridArea[x, y] = obj;
            gridArea[x, y].position = new Vector3(pos.x, pos.y, 0);
        }
    }

    void GenerateMonsters()
    {
        Transform monster = Instantiate(monsterObject);
        Generator(monster);
    }

    void CoinGenerator()
    {
        Transform coin = Instantiate(coinObject);
        Generator(coin);
    }

    void Generator(Transform obj)
    {
        bool flag = true;
        while (flag)
        {
            int x = Random.Range(0, 38);
            int y = Random.Range(0, 19);
            //Debug.Log(x + " : " + y);
            if (x>=0 && y>= 0 && x < 38 && y < 18 && IsEmptySpace(new Vector2Int(x - 19, y - 9)) && x-19!=0 && y-9!=0)
            {
                UpdatePositionInGrid(new Vector2Int(), new Vector2Int(x - 19, y - 9), obj.transform);
                flag = false;
                break;
            }
        }
    }

    public bool CheckForCoin(Vector2Int pos)
    {
        int x = pos.x + 19;
        int y = pos.y + 9;
        if (gridArea[x, y] != null && gridArea[x, y].CompareTag("Coin"))
        {
            coinsCollected += 1;
            Destroy(gridArea[x, y].gameObject);
            gridArea[x, y] = null;
            return true;
        }
            
        return false;
    }

    public void RestartGame()
    {
        for (int i = 0; i < 38; i++)
        {
            for (int j = 0; j < 18; j++)
            {
                if (gridArea[i,j]!=null)
                {
                    Destroy(gridArea[i, j].gameObject);
                    gridArea[i, j] = null;
                }
            }
        }
        for (int i = 0; i < 38; i++)
        {
            for (int j = 0; j < 18; j++)
            {
                if (maze[j, i] == 1)
                {
                    gridArea[i, j] = Instantiate(this.gameObject);
                    gridArea[i, j].position = new Vector2(i - 19, j - 9);
                }
            }
        }
        Instantiate(player).position = new Vector3(0,0,0);
        gridArea[(Mathf.RoundToInt(this.transform.position.x + 19)), (Mathf.RoundToInt(this.transform.position.y + 9))] = player;

        monsterStartTime = 0;
        coinStartTime = 0;
        coinsCollected = 0;
    }

    public bool ClashWithPlayer(Vector2Int pos)
    {
        int x = pos.x + 19;
        int y = pos.y + 9;
        if (gridArea[x,y]!=null && gridArea[x, y].CompareTag("Player")){
            Debug.Log("Monster killed you !!!!!");
            RestartGame();
            return true;
        }
        return false;
    }

    public bool ClashWithMonster(Vector2Int pos)
    {
        int x = pos.x + 19;
        int y = pos.y + 9;
        if (gridArea[x, y] != null && gridArea[x, y].CompareTag("Monsters"))
        {
            Debug.Log("Monster killed you !!!!!");
            RestartGame();
            return true;
        }
        return false;
    }

    public void DestroyFromGrid(Vector2Int pos)
    {
        int x = pos.x + 19;
        int y = pos.y + 9;

        if (gridArea[x, y] != null)
        {
            Destroy(gridArea[x, y].gameObject);
            gridArea[x, y] = null;
        }
    }

    public Transform GetObject(Vector2Int pos)
    {
        int x = pos.x + 19;
        int y = pos.y + 9;
        return gridArea[x, y];
    }
}
