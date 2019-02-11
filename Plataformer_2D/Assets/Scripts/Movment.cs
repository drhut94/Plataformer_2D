using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour {

    public Rigidbody2D rb;
    protected float moveHorizontal;
    protected Vector2 v2;
    public float force;
    public float maxVelocity;


	void Start () {

	}
	

	void Update () {

        Debug.Log(Input.GetAxisRaw("Horizontal"));

    }

    private void FixedUpdate()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        v2.x = force * moveHorizontal * Time.deltaTime;

        if (rb.velocity.x > maxVelocity)
        {
            rb.velocity = new Vector2 (maxVelocity, 0);
        }

        rb.AddForce(v2);
    }
}
