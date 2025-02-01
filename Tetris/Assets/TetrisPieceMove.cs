using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

public class TetrisPieceMove : MonoBehaviour
{
    float fall = 0;
    [SerializeField] public float fallSpeed = 1;
    [SerializeField] public bool allowRotation = true;
    [SerializeField] public bool limitRotation = false;
    void Start()
    {
        
    }

    void Update()
    {
        //Debug.Log(transform.position.x+ " : " + transform.position.y);
        checkUserInput();
    }

    void checkUserInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (checkIsValidPosition(new Vector2(-1, 0)))
            {
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x) - 1, Mathf.RoundToInt(transform.position.y), 0);
                FindObjectOfType<GameGrid>().UpdateGrid(this);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            
            
            if (checkIsValidPosition(new Vector2(1, 0)))
            {
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x) + 1, Mathf.RoundToInt(transform.position.y), 0);
                FindObjectOfType<GameGrid>().UpdateGrid(this);
            }
           
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            
            if (checkIsValidPosition(new Vector2(0, 0)))
            {
                this.transform.Rotate(0, 0, 90);
                FindObjectOfType<GameGrid>().UpdateGrid(this);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || (Time.time - fall) >= fallSpeed) //  || (Time.time - fall) >= fallSpeed
        {   
            if (checkIsValidPosition(new Vector2(0,-1)))
            {
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y) - 1, 0);
                FindObjectOfType<GameGrid>().UpdateGrid(this);
            }
            else
            {
                //transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y) + 1, 0);
                FindObjectOfType<GameGrid>().DeleteRow();
                enabled = false;
                FindObjectOfType<GameGrid>().SpawnNextTetromino();
            }
            fall = Time.time;
        }
    }

    bool checkIsValidPosition(Vector2 val)
    {
        foreach (Transform mino in transform)
        {
            Vector2 pos = FindObjectOfType<GameGrid>().Round(mino.position,val);
            if (!FindObjectOfType<GameGrid>().CheckInGrid(pos) || (FindObjectOfType<GameGrid>().CheckInGrid(pos) && !FindObjectOfType<GameGrid>().CheckPosInGrid(pos, this)))
            {
                return false;
            }
        }
        return true;
    }


}
