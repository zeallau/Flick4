using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainControl : MonoBehaviour {
    private GameObject Disc;

    public GameObject GoodButton;
    public GameObject BadButton;

    //Click Down pos
    private Vector3 touchStartPos;
    private Vector3 touchStartworldPos;

    //Click Up Pos
    private Vector3 touchEndPos;
    private Vector3 touchEndworldPos;

    //clickDistance
    public Vector3 clickDistance;

    public float movedDistance = 0.0f;
    private const float MOVE_SPEED_PER_SECOND = 1.0f;
    
    private Vector3 discSpawnPos;


    //Get Target Pos
    private Vector3 targetSpawnPos;
    private float lastDTC;
    private float currentDTC;
    private float finalDTC;

    private Vector3 updatedPos;
    private Vector3 curPos;
    private Vector3 oldPos;

    //Setup target score
    private float discToCenter;
    private float targetRadius;
    private float scoreYellow;
    private float scoreRed;
    private float scoreBlue;

    //To force script run step by step
    bool getDTC;
    bool scoring;
    bool scoreUp;
    bool scoreUpdate;

    public bool isMove = false;

    private GameObject scoreText;
    public int score = 0;

    public GameObject DiscText;
    public int discCount = 10;

    // Use this for initialization
    void Start () {
        Disc = GameObject.Find("Disc");
        scoreText = GameObject.Find("ScoreText");
        DiscText = GameObject.Find("DiscText");

        discSpawnPos = new Vector3(Random.Range(-2.6f, 2.6f), -4f, 0.0f);
        Disc.transform.position = discSpawnPos;

        targetSpawnPos = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(0.0f, 2.0f), 0.0f);
        this.gameObject.transform.position = targetSpawnPos;
        targetRadius = GetComponent<CircleCollider2D>().radius;

        
        getDTC = true;
        scoring = false;
        scoreUp = false;
        scoreUpdate = false;

        isMove = false;
    }



    void FingerFlick()
    {
        scoreYellow = targetRadius * 0.162f;
        scoreRed = targetRadius * 0.5f;
        scoreBlue = targetRadius * 0.83f;

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchStartPos = new Vector3(Input.GetTouch(0).position.x,
                                        Input.GetTouch(0).position.y, -10.0f);
                touchStartworldPos = Camera.main.ScreenToWorldPoint(touchStartPos);
                //Debug.Log("touchStartworldPos is " + touchStartworldPos);
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                touchEndPos = new Vector3(Input.GetTouch(0).position.x,
                                      Input.GetTouch(0).position.y, -10.0f);

                touchEndworldPos = Camera.main.ScreenToWorldPoint(touchEndPos);
                //Debug.Log("touchEndworldPos is" + touchEndworldPos);


                //Get click Distance
                clickDistance = (touchEndworldPos - touchStartworldPos) * 2.5f;
                //Debug.Log("clickDistance is" + clickDistance.magnitude);

                movedDistance = 0.0f;

                isMove = true;
            }

        }

        if (movedDistance < clickDistance.magnitude && isMove == true)
        {
            //moveDistance to count every secound the object move.
            float moveDistance = (clickDistance * (MOVE_SPEED_PER_SECOND * Time.deltaTime)).magnitude;
            Disc.transform.Translate(clickDistance * (MOVE_SPEED_PER_SECOND * Time.deltaTime));
            movedDistance += moveDistance;

        }

    }

    public void OnTriggerStay2D(Collider2D collider)
    {
        updatedPos = Disc.transform.position;
        curPos = updatedPos;

        //Distance between Disc and center point of target
        discToCenter = Vector3.Distance(Disc.transform.position, targetSpawnPos);

        //Debug.Log("discToCenter is" + discToCenter);

        //To compare currentDTC and lastDTC
        currentDTC = discToCenter;

        Debug.Log("currentDTC subposed is" + currentDTC);
        Debug.Log("lastDTC subposed is" + lastDTC);
        Debug.Log("curPos subposed is" + curPos.magnitude);
        Debug.Log("oldPos subposed is" + oldPos.magnitude);

        if (currentDTC == lastDTC && getDTC == true || curPos == oldPos && getDTC == true)
        {
            finalDTC = discToCenter;
            Debug.Log("finalDTC subposed is" + finalDTC);
           
            getDTC = false;
            scoring = true;
            scoreUp = true;
        }
        lastDTC = currentDTC;
        oldPos = curPos;

        if (scoring == true)
        {
            if (finalDTC <= scoreYellow && scoreUp == true)
            {
                this.score += 100;
                this.scoreText.GetComponent<Text>().text = "Score: " + this.score + "pt";

                Debug.Log("+ 100 Score.");
                scoreUp = false;


            }
            else if (finalDTC > scoreYellow && finalDTC <= scoreRed && scoreUp == true)
            {
                this.score += 50;
                this.scoreText.GetComponent<Text>().text = "Score: " + this.score + "pt";

                Debug.Log("Get + 50 Score.");
                scoreUp = false;

            }
            else if (finalDTC > scoreRed && finalDTC <= scoreBlue && scoreUp == true)
            {
                this.score += 20;
                this.scoreText.GetComponent<Text>().text = "Score: " + this.score + "pt";
                Debug.Log(" + 20 Score.");
                scoreUp = false;

            }
            else if (finalDTC > scoreBlue && finalDTC <= targetRadius && scoreUp == true)
            {
                Debug.Log("Get + 0 Score. Missing");
                scoreUp = false;

            }
            scoreUpdate = true;

            
        }


    }

    void Respawn()
    {
        if (scoreUpdate)
        {

            Start();

            this.discCount -= 1;
            DiscText.GetComponent<Text>().text = "Disc: " + discCount + " / 10";
            scoreUpdate = false;
        }
    }

    // Update is called once per frame
    void Update () {
        FingerFlick();

        Respawn();

        Result();
    }

    //情景になると、ボタンを現れる方法を教えてくだい。
    void Result()
    {
        if(discCount == 0 && score >= 600)
        {
            GoodButton.SetActive(true);
        }
        if(discCount == 0 && score < 600)
        {
            BadButton.SetActive(true);
        }
    }

}
