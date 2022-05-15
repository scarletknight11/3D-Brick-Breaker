using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

    public int health = 1;
    public int score = 50;

    // Start is called before the first frame update
    void Start()
    {
        //ADD THE BRICK TO GAME MANAGER
        
    }

    public void TakeDamage()
    {
        health--;
        if(health <= 0)
        {
            //CREATE Particles

            //REPORT TO THE GAME MANAGER

            //REPORT TO THE SCORE MANAGER

            //DESTROY BRICK
            Destroy(gameObject);
        }
    }
    
}
