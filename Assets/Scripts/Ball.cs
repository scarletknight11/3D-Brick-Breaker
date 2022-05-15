using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    Rigidbody rb;

    public static float initialForce = 600f;
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
            brick.TakeDamage();
        }
    }

    public void StartBall()
    {
        if(!ballStarted)
        {
            rb.isKinematic = false;
            rb.AddForce(new Vector3(initialForce, initialForce, 0));
            ballStarted = true;
            //PARENT BACK TO THE WORLD
            transform.SetParent(transform.parent.parent);
        }
    } 

    public bool BallStarted()
    {
        return ballStarted;
    }
}
