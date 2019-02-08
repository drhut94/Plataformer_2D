using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prediction : MonoBehaviour
{
    public Camera camera;
    public Collider2D myCollider;
    public LineRenderer lineRenderer;
    public Rigidbody2D rigidbody2D;
    public float velocityMultiplier = 10;
    public int maxDistance = 50;
    public bool reverseControls = true;
    private float speed; // Variable privada que se utiliza para saber la velocidad de la pelota
    public float tiempo;


    void Start()
    {
        lineRenderer.enabled = false;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ActivateIfClickingMyCollider();
        }
        if (lineRenderer.enabled)
        {
            speed = rigidbody2D.velocity.magnitude;
            if (speed < 10) //Te permite volver a tirar si la velocidad de la pelota es menor a 0.3 m/s
            {

                Vector2 meToMouse = (camera.ScreenToWorldPoint(Input.mousePosition) - transform.position);
                if (reverseControls)
                    meToMouse = -meToMouse;
                if (meToMouse.magnitude > maxDistance)
                {
                    meToMouse.Normalize();
                    meToMouse *= maxDistance;
                }
                Vector2 velocity = meToMouse * velocityMultiplier;
                if (Input.GetMouseButton(0))
                {
                    PlotParabole(velocity);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    rigidbody2D.AddForce(velocity, ForceMode2D.Impulse);
                    //enabled = false;
                    tiempo = Time.time;
                    Debug.Log(tiempo);
                }
            }
        }


    }

    void ActivateIfClickingMyCollider()
    {
        Vector3 mouseClockedPos = camera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] raycastHits = Physics2D.RaycastAll(mouseClockedPos, Vector2.zero);
        lineRenderer.enabled = HitCollider(raycastHits);
    }

    bool HitCollider(RaycastHit2D[] raycastHits)
    {
        for (int i = 0; i < raycastHits.Length; i++)
        {
            if (raycastHits[i].collider.Equals(myCollider))
                return true;
        }
        return false;
    }

    void PlotParabole(Vector2 startVelocity)
    {
        List<Vector3> positions = new List<Vector3>();
        Vector2 start = transform.position;
        positions.Add(start);
        Vector2 velocity = startVelocity;
        var timeStep = Time.fixedDeltaTime * 3;
        velocity.y += Physics.gravity.y * timeStep;
        Vector2 nextStep = start + velocity * timeStep;
        for (int steps = 15; steps >= 0; steps--)
        {
            positions.Add(nextStep);
            start = nextStep;
            velocity.y += Physics2D.gravity.y * timeStep;
            nextStep = start + velocity * timeStep;
        }
        positions.Add(nextStep);
        lineRenderer.SetPositions(positions.ToArray());
    }
}
