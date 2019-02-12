using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment_v2 : MonoBehaviour {


    Rigidbody2D rb;
    public float speed;
    Vector2 speedV2;
    public float force = 10;
    protected float moveHorizontal;
    enum Status {
        idle,
        accelerate,
        move,
        jump,
        crouch
    }

    Status mode = Status.idle;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
	

void Update () {

        moveHorizontal = Input.GetAxisRaw("Horizontal");
        Debug.Log(mode);
        

        if (moveHorizontal != 0)
        {
            mode = Status.accelerate;

            if (rb.velocity.x >= speed * moveHorizontal
                || rb.velocity.x <= -speed * moveHorizontal)
            {
                mode = Status.move;
            }
        }
        else
        {
            mode = Status.idle;
        }

        switch (mode)
        {
            case Status.idle:
                
                if (rb.velocity.x != 0)
                {
                    rb.AddForce(transform.right * force / 10 * -1);
                }
                break;

            case Status.accelerate:
                rb.AddForce(transform.right * force * moveHorizontal);
                mode = Status.idle;
                break;

            case Status.move:
                speedV2.x =  speed * moveHorizontal;
                rb.velocity = speedV2;
                mode = Status.idle;
                break;

            case Status.jump:

                break;

            case Status.crouch:

                break;
        }
    }


}


