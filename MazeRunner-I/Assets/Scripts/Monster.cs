using UnityEngine;
// using static UnityEditor.PlayerSettings;

public class Monster : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int monsterDelay = 1;
    float monsterStartTime = 0;
    string defaultMove = "Left";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (monsterStartTime == 0 || (Time.time - monsterStartTime >= monsterDelay))
        {
            moveMonster();
            monsterStartTime = Time.time;
        }
    }

    void moveMonster()
    {
        Vector2Int pos1 = new Vector2Int(Mathf.RoundToInt(transform.position.x - (1 * speed)), Mathf.RoundToInt(transform.position.y));
        bool x1 = FindObjectOfType<GridSystem>().IsEmptySpace(pos1);
        Vector2Int pos2 = new Vector2Int(Mathf.RoundToInt(transform.position.x + (1 * speed)), Mathf.RoundToInt(transform.position.y));
        bool x2 = FindObjectOfType<GridSystem>().IsEmptySpace(pos2);
        //Vector2Int pos3 = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y + (1 * speed)));
        //bool x3 = FindObjectOfType<GridSystem>().IsEmptySpace(pos3);
        //Vector2Int pos4 = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y - 1 * speed));
        //bool x4 = FindObjectOfType<GridSystem>().IsEmptySpace(pos4);

        Vector2Int prev_pos = new Vector2Int(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y));
        if (defaultMove == "Left")
        {
            if (x1)
            {
                FindObjectOfType<GridSystem>().UpdatePositionInGrid(prev_pos, pos1, this.transform);
            }
            else if (FindObjectOfType<GridSystem>().ClashWithPlayer(pos1))
            {
                Debug.Log("Game Over");
            }
            else
            {
                defaultMove = "Right";
            }
        }

        if (defaultMove == "Right")
        {
            if (x2)
            {
                FindObjectOfType<GridSystem>().UpdatePositionInGrid(prev_pos, pos2, this.transform);
            }
            else if (FindObjectOfType<GridSystem>().ClashWithPlayer(pos2))
            {
                Debug.Log("Game Over");
            }
            else
            {
                defaultMove = "Left";
            }
        }
        

        //else if (x3)
        //{ 
        //    FindObjectOfType<GridSystem>().UpdatePositionInGrid(prev_pos, pos3, this.transform);
        //}
        //else if (x4)
        //{
        //    FindObjectOfType<GridSystem>().UpdatePositionInGrid(prev_pos, pos4, this.transform);
        //}
    }
}
