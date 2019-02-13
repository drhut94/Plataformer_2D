using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment_v2 : MonoBehaviour {

    public Animator animator;
    Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    protected float timer;
    Vector2 speedV2;
    Vector2 forceV2;
    public float gravityScale;
    public float acceleration;
    protected float moveHorizontal;
    public bool moveWhileJumping;



    enum Status {
        idle,
        accelerateRight,
        accelerateLeft,
        moveRight,
        moveLeft,
        jump,
        crouch
    }

    Status mode = Status.idle;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
	

    void Update () {


        moveHorizontal = Input.GetAxisRaw("Horizontal");

        speedV2 = rb.velocity;

        if (moveHorizontal > 0)
        {
            mode = Status.accelerateRight;

            if (rb.velocity.x >= speed)
            {
                mode = Status.moveRight;
            }            
        }
        else if (moveHorizontal < 0)
        {
            mode = Status.accelerateLeft;

            if (rb.velocity.x <= -speed)
            {
                mode = Status.moveLeft;
            }
        }
        else
        {
            mode = Status.idle;
        }

        if (Input.GetButtonDown("Jump"))
        {
            mode = Status.jump;
        }

        switch (mode)
        {
            case Status.idle:
                
                if (rb.velocity.x > 0)
                {
                    speedV2.x -= acceleration * Time.deltaTime;
                }
                else if (rb.velocity.x < 0)
                {
                    speedV2.x += acceleration * Time.deltaTime;
                }

                break;

            case Status.accelerateRight:

                speedV2.x += acceleration * Time.deltaTime * moveHorizontal;

                break;

            case Status.moveRight:

                speedV2.x = speed * moveHorizontal;

                break;

            case Status.accelerateLeft:

                speedV2.x += acceleration * Time.deltaTime * moveHorizontal;

                break;

            case Status.moveLeft:

                speedV2.x = speed * moveHorizontal;

                break;

            case Status.jump:

                forceV2 = Vector2.up * jumpForce * Time.deltaTime * 100;
                rb.AddForce(forceV2);
                Debug.Log("saltaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

                break;

            case Status.crouch:

                break;
        }

        rb.velocity = speedV2;
        
        mode = Status.idle;

        animator.SetFloat("speedX", Mathf.Abs(rb.velocity.x));

        Debug.Log(speedV2);
    }


}


