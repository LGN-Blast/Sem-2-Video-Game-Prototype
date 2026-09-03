using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement + Dashing
    public float movespeed;
    Rigidbody2D rb;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public bool isDashing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        InputManagement();
    }

    private void FixedUpdate() //More Suited For Physics calcs (Calls on regular 50 frame intervals)
    {
        if (!isDashing)
        {
            Move();
        }
        
    }

    void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;

        if(moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
        }

        if(moveDir.y !=0)
        {
            lastVerticalVector = moveDir.y;
        }

    }

    void Move ()
    {
        rb.velocity = new Vector2(moveDir.x * movespeed, moveDir.y * movespeed);
    }

}
