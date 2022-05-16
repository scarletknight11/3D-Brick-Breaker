using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.IO;

public class BarControl : MonoBehaviour {

    //public float speed = 5f;
    //Arduino Communication variable
    SerialPort sp;

    // Start is called before the first frame update
    void Start()
    {
        //Arduino COM port number
        sp = new SerialPort("COM3", 9600);
        sp.ReadTimeout = 10;
        sp.Open();
    }

    // Update is called once per frame
    void Update()
    {
        if (sp.IsOpen) {
            // try-catch statement prevents our entire game
            //   from stopping in case the serial read function does not work, 
            // because no data is being sent by the Arduino
            try
            {
                //read the by tes that are sent from the Arduino and store them locally
                int value = sp.ReadByte();
                //maps input into unity cordinate system
                float positionUnity = (20 - ((float)value / 5));
                //moves position of bar object through exoskeleton control. Updates game object’s x-position with the new value at every frame
                transform.position = new Vector3(positionUnity, transform.position.y,
                   transform.position.z);
            }
            //catch statements will be activated and provide an error handling message in case the serial communication does not work
             catch (System.Exception e) {

            }
        }
    }
}