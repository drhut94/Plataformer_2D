using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour {

    public Rigidbody2D rb;
    protected float moveHorizontal;
    protected Vector2 v2;
    public float maxVelocity;
    bool jump;
    public float jumpForce;
    private Vector2 force;


	void Start () {

	}
	

	void Update () {

        Debug.Log(Input.GetAxisRaw("Jump"));

        moveHorizontal = Input.GetAxisRaw("Horizontal");
        jump = Input.GetButton("Jump");
       
    }

    private void FixedUpdate()
    {
        v2 = rb.velocity;
        v2.x = maxVelocity * moveHorizontal * Time.deltaTime * 100;
        rb.velocity = v2;

        if (jump && rb.velocity.y == 0)
        {
            force = Vector2.up * jumpForce * 100;
            rb.AddForce(force);
            jump = false;
        }
    }
}
