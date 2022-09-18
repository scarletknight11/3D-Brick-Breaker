using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

    public int health = 1;
    public int score = 50;
    public GameObject particlePrefab;

    // Start is called before the first frame update
    void Start()
    {
        //ADD THE BRICK TO GAME MANAGER
        GameManager.instance.AddBrick(this.gameObject);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            //CREATE Particles
            Instantiate(particlePrefab, transform.position, Quaternion.identity);
            //REPORT TO THE GAME MANAGER
            GameManager.instance.RemoveBrick(this.gameObject);
            //REPORT TO THE SCORE MANAGER
            ScoreManager.instance.AddScore(score);
            //DROP a POWER UP?
            PowerUpManager.instance.DropPowwerUp(transform.position);
            //DESTROY BRICK
            Destroy(gameObject);
        }
    }
    
}