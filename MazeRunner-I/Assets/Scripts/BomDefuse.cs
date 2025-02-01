using UnityEngine;
using System;
// using System.Timers;

public class BomDefuse : MonoBehaviour
{
    int[,] positionsPossible = new int[4, 2]
    {
        {1,0},
        {0,1},
        {-1,0},
        {0,-1}
    };
    int timer = 5;
    DateTime _now; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _now = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan delay = DateTime.Now - _now ;
        Debug.Log("Running Time: " + delay.Milliseconds);
        if (delay.Seconds >= timer)
        {
            Debug.Log("Time Over: " + delay.Seconds);
            Vector2Int pos = new Vector2Int(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y));
            DestroyMonsters(pos);
            Destroy(this);
            FindObjectOfType<GridSystem>().UpdatePositionInGrid(pos, new Vector2Int(), this.transform);
        }
    }

    void DestroyMonsters(Vector2Int pos)
    {
        for (int i = 0; i < 4; i++)
        {
            Vector2Int near_pos = new Vector2Int(pos.x + positionsPossible[i, 0], pos.y + positionsPossible[i, 1]);
            Transform monster = FindObjectOfType<GridSystem>().GetObject(near_pos);
            if (monster!=null && monster.CompareTag("Monsters"))
            {
                FindObjectOfType<GridSystem>().DestroyFromGrid(near_pos);
            }
        }
    }
}
