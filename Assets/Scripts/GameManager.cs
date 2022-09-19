using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Windows.Speech;
using System;


public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public GameObject ballPrefab;

    List<GameObject> ballList = new List<GameObject>();
    List<GameObject> brickList = new List<GameObject>();

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    public GameObject winPanel;
    public GameObject losePanel;

    bool gameEnded;

    int lifes = 3;
    public Text lifesText;

    void Awake()
    {
        instance = this;    
    }

    void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        ResetGame();
        actions.Add("launch", Launch);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke(); ;
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
        lifesText.text = lifes.ToString("D2");
    }

    //-------------------------LIFES------------------------
    void RemoveLife()
    {
        lifes--;
        //UPDATE UI
        UpdateUI();

        //LOSE CONDITION
        if (lifes == 0 && !gameEnded)
        {
            gameEnded = true;
            losePanel.SetActive(false);
            print("Game OVER");
            
            return;
        }
        if(!gameEnded)
        {
            CreateBall();
        }
       
        //RESET PADDLE POSITION
        Paddle.instance.ResetPaddle();
    }

    public void LostBall(GameObject ball)
    {
        ballList.Remove(ball);
        Destroy(ball);

        //check how much life left
        if(ballList.Count == 0 && !gameEnded)
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
        if(brickList.Count == 0 && !gameEnded)
        {
            gameEnded=true;
            winPanel.SetActive(true);
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

    private void Launch()
    {
        if (ballList[0] != null && ballList[0].GetComponent<Ball>())
        {
            StartBall();
        }
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

    public void Multiball()
    {

        int amount = 2; //HOW MANY BALLS PER ACTIVE BALL
        int ballLimit = 12; //HOW MANY MAXIMUM BALLS IN BALL LIST
        for (int i = ballList.Count-1; i >= 0; i--)
        {
            Vector3 ballPos = ballList[i].transform.position;
            if(ballList.Count <= 12)
            {
                for (int j = 0; j < 2; j++)
                {
                    GameObject newBall = Instantiate(ballPrefab, ballPos, Quaternion.identity);
                    newBall.GetComponent<Rigidbody>().AddForce(Ball.initialForce, Ball.initialForce, 0);
                    ballList.Add(newBall);
                    newBall.GetComponent<Ball>().SetBall();
                }
            }
            
        }
    }
}