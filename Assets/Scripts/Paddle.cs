using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    public static Paddle instance;
    Rigidbody rb;
    BoxCollider col;

    float speed = 10f;

    void Awake()
    {
        instance = this;    
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if((int)h == 0 && rb.velocity != Vector3.zero)
        {
            rb.velocity = Vector3.zero;
        }
        rb.MovePosition(transform.position + new Vector3(h, 0, 0).normalized * speed * Time.fixedDeltaTime);
    }
}
