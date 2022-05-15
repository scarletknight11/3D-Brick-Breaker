using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public GameObject ballPrefab;

    List<GameObject> ballList = new List<GameObject>();
    List<GameObject> brickList = new List<GameObject>();

    int lifes = 3;
    public Text lifesText;
    void Awake()
    {
        instance = this;    
    }

    void Start()
    {
        ResetGame();
    }

    void ResetGame()
    {
        lifes = 3;
        CreateBall();
        //UPDATE UI
        UpdateUI();
    }

    void UpdateUI()
    {
        lifesText.text = "Lifes: " + lifes.ToString("D2");
    }

    //-------------------------LIFES------------------------
    void RemoveLife()
    {
        lifes--;
        //UPDATE UI
        UpdateUI();

        //LOSE CONDITION
        if (lifes == 0)
        {
            print("Game OVER");
            return;
        }
        CreateBall();
        //RESET PADDLE POSITION
        Paddle.instance.ResetPaddle();
    }

    public void LostBall(GameObject ball)
    {
        ballList.Remove(ball);
        Destroy(ball);

        //check how much life left
        if(ballList.Count == 0)
        {
            RemoveLife();
        }
    }

    //-------------------------BRICKS------------------------

    public void AddBrick(GameObject brick)
    {
        brickList.Add(brick);
    }

    public void RemoveBrick(GameObject brick)
    {
        brickList.Remove(brick);
        //WINNING CONDITION
        if(brickList.Count == 0)
        {
            print("You Win");
        }
    }
    
    //-------------------------CREATE-BALL------------------------

    void CreateBall()
    {
        GameObject newBall = Instantiate(ballPrefab);
        newBall.transform.position = Paddle.instance.gameObject.transform.position + new Vector3(0,1.5f,0);
        newBall.transform.SetParent(Paddle.instance.gameObject.transform);
        newBall.gameObject.GetComponent<Rigidbody>().isKinematic = true;

        ballList.Add(newBall);
    }

    void StartBall()
    {
        ballList[0].GetComponent<Ball>().StartBall();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && ballList.Count > 0)
        {
            if (ballList[0] != null && ballList[0].GetComponent<Ball>())
            {
                StartBall();
            }
        } 
    }
}