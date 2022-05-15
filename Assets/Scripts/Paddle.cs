using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.IO.Ports;

public class Paddle : MonoBehaviour
{

    public static Paddle instance;

    public GameObject center, leftCap, rightCap;
    Rigidbody rb;
    BoxCollider col;

    float speed = 10f;
    public float newSize = 2f;

    SerialPort sp;


    void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        //Arduino COM port number
        sp = new SerialPort("COM3", 9600);
        sp.ReadTimeout = 10;
        //sp.WriteTimeout = 5;
        sp.Open();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
        ResetPaddle();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((int)h == 0 && rb.velocity != Vector3.zero)
        {
            rb.velocity = Vector3.zero;
        }
        rb.MovePosition(transform.position + new Vector3(h, 0, 0).normalized * speed * Time.fixedDeltaTime);

        if (sp.IsOpen)
        {
            // try-catch statement prevents our entire game
            //   from stopping in case the serial read function does not work, 
            // because no data is being sent by the Arduino
            try
            {
                //read the by tes that are sent from the Arduino and store them locally
                int value = sp.ReadByte();
                //maps input into unity cordinate system
                float positionUnity = (21 - ((float)value / 5));
                //moves position of bar object through exoskeleton control. Updates game object’s x-position with the new value at every frame
                transform.position = new Vector3(positionUnity, transform.position.y,
                   transform.position.z);
                //sp.DiscardInBuffer();
            }
            //catch statements will be activated and provide an error handling message in case the serial communication does not work
            catch (System.Exception e)
            {
            }
        }
    }

    void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ball"))
            {
                Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();
                float vel = Ball.initialForce;
                Vector3 hitPoint = collision.contacts[0].point;
                float difference = transform.position.x - hitPoint.x;

                if (hitPoint.x < transform.position.x)
                {
                    ballRb.AddForce(new Vector3(-(Mathf.Abs(difference * 200)), vel, 0));
                }
                else
                {
                    ballRb.AddForce(new Vector3(Mathf.Abs(difference * 200), vel, 0));
                }
            }
        }

    void Resize(float xScale)
        {
            Vector3 initScale = center.transform.localScale;
            initScale.x = xScale;
            center.transform.localScale = initScale;

            //LEFT CAP
            Vector3 leftCapPos = new Vector3(center.transform.position.x - (xScale / 2), leftCap.transform.position.y, leftCap.transform.position.z);
            leftCap.transform.position = leftCapPos;
            //RIGHT CAP
            Vector3 rightCapPos = new Vector3(center.transform.position.x + (xScale / 2), rightCap.transform.position.y, rightCap.transform.position.z);
            rightCap.transform.position = rightCapPos;

            //COLLIDER SCALE
            Vector3 colScale = initScale;
            colScale.x += 1.0f;
            col.size = colScale;
        }

    public void ResetPaddle()
    {
        transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, 0);
        Resize(newSize);
    }

}