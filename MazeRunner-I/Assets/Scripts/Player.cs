using UnityEngine;
// using UnityEngine.UIElements;
// using static UnityEditor.PlayerSettings;

public class Player : MonoBehaviour
{
    [SerializeField]float speed = 1f;
    [SerializeField] Transform bombObject;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Motion();
    }

    void Motion(){
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            Vector2Int pos = new Vector2Int(Mathf.RoundToInt(transform.position.x - (1 * speed)), Mathf.RoundToInt(transform.position.y));
            if (FindObjectOfType<GridSystem>().IsEmptySpace(pos) || FindObjectOfType<GridSystem>().CheckForCoin(pos))
            {
                Vector2Int prev_pos = new Vector2Int(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y));
                FindObjectOfType<GridSystem>().UpdatePositionInGrid(prev_pos,pos, this.transform);
            }
            else if (FindObjectOfType<GridSystem>().ClashWithMonster(pos))
            {
                Debug.Log("Game Over");
            }

        }
            
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            Vector2Int pos = new Vector2Int(Mathf.RoundToInt(transform.position.x + (1 * speed)), Mathf.RoundToInt(transform.position.y));
            if (FindObjectOfType<GridSystem>().IsEmptySpace(pos) || FindObjectOfType<GridSystem>().CheckForCoin(pos))
            {
                Vector2Int prev_pos = new Vector2Int(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y));
                FindObjectOfType<GridSystem>().UpdatePositionInGrid(prev_pos, pos, this.transform);
            }
            else if (FindObjectOfType<GridSystem>().ClashWithMonster(pos))
            {
                Debug.Log("Game Over");
            }
        }
            
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            Vector2Int pos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y + (1 * speed)));
            if (FindObjectOfType<GridSystem>().IsEmptySpace(pos) || FindObjectOfType<GridSystem>().CheckForCoin(pos))
            {
                Vector2Int prev_pos = new Vector2Int(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y));
                FindObjectOfType<GridSystem>().UpdatePositionInGrid(prev_pos, pos, this.transform);
            }
            else if (FindObjectOfType<GridSystem>().ClashWithMonster(pos))
            {
                Debug.Log("Game Over");
            }

        }
            
        if (Input.GetKeyDown(KeyCode.DownArrow)){
            Vector2Int pos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y - 1 * speed));
            if (FindObjectOfType<GridSystem>().IsEmptySpace(pos) || FindObjectOfType<GridSystem>().CheckForCoin(pos))
            {
                Vector2Int prev_pos = new Vector2Int(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y));
                FindObjectOfType<GridSystem>().UpdatePositionInGrid(prev_pos, pos, this.transform);
            }
            else if (FindObjectOfType<GridSystem>().ClashWithMonster(pos))
            {
                Debug.Log("Game Over");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DropABomb();
        }
    }

    void DropABomb()
    {
        Vector2Int pos1 = new Vector2Int(Mathf.RoundToInt(transform.position.x - (1 * speed)), Mathf.RoundToInt(transform.position.y));
        bool x1 = FindObjectOfType<GridSystem>().IsEmptySpace(pos1);
        Vector2Int pos2 = new Vector2Int(Mathf.RoundToInt(transform.position.x + (1 * speed)), Mathf.RoundToInt(transform.position.y));
        bool x2 = FindObjectOfType<GridSystem>().IsEmptySpace(pos2);
        Vector2Int pos3 = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y + (1 * speed)));
        bool x3 = FindObjectOfType<GridSystem>().IsEmptySpace(pos3);
        Vector2Int pos4 = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y - 1 * speed));
        bool x4 = FindObjectOfType<GridSystem>().IsEmptySpace(pos4);

        Transform bomb = Instantiate(bombObject);
        //bomb.position = new Vector3(transform.position.x, transform.position.y, 0);
        Vector2Int prev_pos = new Vector2Int(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y));
        if (x1 || FindObjectOfType<GridSystem>().CheckForCoin(pos1))
        {
            FindObjectOfType<GridSystem>().UpdatePositionInGrid(new Vector2Int(), new Vector2Int(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y)), bomb);
            FindObjectOfType<GridSystem>().UpdatePositionInGrid(prev_pos, pos1, this.transform);
            return;
        }

        else if (x2 || FindObjectOfType<GridSystem>().CheckForCoin(pos2))
        {
            FindObjectOfType<GridSystem>().UpdatePositionInGrid(new Vector2Int(), new Vector2Int(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y)), bomb);
            FindObjectOfType<GridSystem>().UpdatePositionInGrid(prev_pos, pos2, this.transform);
            return;
        }

        else if (x3 || FindObjectOfType<GridSystem>().CheckForCoin(pos3))
        {
            FindObjectOfType<GridSystem>().UpdatePositionInGrid(new Vector2Int(), new Vector2Int(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y)), bomb);
            FindObjectOfType<GridSystem>().UpdatePositionInGrid(prev_pos, pos3, this.transform);
            return;
        }
        else if (x4 || FindObjectOfType<GridSystem>().CheckForCoin(pos4))
        {
            FindObjectOfType<GridSystem>().UpdatePositionInGrid(new Vector2Int(), new Vector2Int(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y)), bomb);
            FindObjectOfType<GridSystem>().UpdatePositionInGrid(prev_pos, pos4, this.transform);
            return;
        }
        else
        {
            Destroy(bomb);
        }
    } 
}
