using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeliveryDriver : MonoBehaviour
{
    [SerializeField] float steerSpeed = 0.1f;
    [SerializeField] float moveSpeed = 0.01f;
    [SerializeField] float slowSpeed = 15f;
    [SerializeField] float boostSpeed = 30f;

    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal")*steerSpeed*Time.deltaTime; // Intervals of sec from last frame to the current frame(Read only)
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed*Time.deltaTime;
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, moveAmount, 0);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Boosts")
        {
            moveSpeed = boostSpeed;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("I m going here");
        moveSpeed = slowSpeed;
    }
}
