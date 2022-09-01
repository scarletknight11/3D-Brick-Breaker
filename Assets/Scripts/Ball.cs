using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    Rigidbody rb;
    public static float initialForce = 250f;
    bool ballStarted;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //DEBUGING
        rb.AddForce(new Vector3(0,initialForce,0));
    }

    void OnCollisionEnter(Collision collision)
    {
        Brick brick = collision.gameObject.GetComponent<Brick>();
        //if brick did not find gameobject we wanna collide with
        if (brick != null)
        {
            brick.TakeDamage(1);
        }
    }

    public void StartBall()
    {
        if(!ballStarted)
        {
            rb.isKinematic = false;
            //CALCULATE X FORCE
            float xDist = Camera.main.transform.position.x - transform.position.x;
            Debug.Log(xDist);

            rb.AddForce(new Vector3(xDist * 43, initialForce, 0));
            ballStarted = true;
            //PARENT BACK TO THE WORLD
            transform.SetParent(transform.parent.parent);
        }
    } 

    public bool BallStarted()
    {
        return ballStarted;
    }

    public void SetBall()
    {
        ballStarted = true;
    }
}