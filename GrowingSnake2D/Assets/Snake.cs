using UnityEngine;
using System.Collections.Generic;
public class Snake : MonoBehaviour
{
    //[SerializeField] float speedOfSnake = 1f;
    [SerializeField] float sizeOfSnake = 1f;

    Vector2 _direction = Vector2.right;

    List<Transform> _segments = new List<Transform>();
    [SerializeField] public Transform segmentPrefab;
    [SerializeField] public int initialSize = 4;
    void Start()
    {
        // Adding snake head to segment
        //_segments = new List<Transform>();
        //_segments.Add(this.transform);
        //for (int i = 1; i < initialSize; i++)
        //{
        //    _segments.Add(Instantiate(this.segmentPrefab));
        //}
        ResetState();
    }

    
    void Update()
    {
        // The below code was moving 1 step left or right or up or down when key is pressed
        //float amountOfMoveHorizontally = Input.GetAxis("Horizontal") * speedOfSnake * Time.deltaTime;
        //float amountOfMoveVertically = Input.GetAxis("Vertical") * speedOfSnake * Time.deltaTime;
        //transform.Translate(amountOfMoveHorizontally, amountOfMoveVertically, 0);

        // The bellow code will move the snake auutomatically in a direction we need to change the directions by pressing up down right left
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _direction = Vector2.up;
        }

        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _direction = Vector2.right;
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _direction = Vector2.left;
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Debug.Log("I m food");
            //transform.localScale = transform.localScale + new Vector3(sizeOfSnake, 0, 0);
            Grow();
        }
        else if (collision.tag == "Obstacles")
        {
            Debug.Log("Into the walls I see....;(");
            ResetState();
        }
    }

    public void ResetState()
    {
        for (int i=1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);
        for (int i = 1; i < initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }
        this.transform.position = Vector3.zero;
    }

    public void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0f
           );
    }

    public void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
    }
}
