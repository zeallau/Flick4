using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusScript : MonoBehaviour {
    private GameObject Disc;
    private GameObject Target;
    private GameObject Boarder;　//tagを認識するのにこれは必要？

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

    private bool isTrigger = false;

    private bool isMove = false;
    
    

    // Use this for initialization
    void Start () {
        Disc = GameObject.Find("Disc");
        Target = GameObject.FindGameObjectWithTag("Target");
        Boarder = GameObject.FindGameObjectWithTag("Boarder");  //Tagを認識するのに余計？
        

        Disc.transform.Translate(0.0f, 0.0f, 0.0f);

        discSpawnPos = new Vector3(Random.Range(-2.6f, 2.6f), -4.2f, 0.0f);
        Disc.transform.position = discSpawnPos;


        isTrigger = false;
        isMove = false;
}

    void FingerFlick()
    {
        

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("is OnTrigger");
        isMove = false;

        //DestroyItem();

        //どうしたら当たったObjectのTagを認識できますか？
        
        if (collision.gameObject.tag == "Boarder")
        {
            Start();
        }

        if (collision.gameObject.tag == "Target")
        {
            Destroy(collision.gameObject);  //これはTargetというTagをつけたら、Destroyしますが、当たったObjectだけDestroyしたいならどうすればいいですか？
            Start();
            
        }
        

    }
    

    // Update is called once per frame
    void Update () {
        FingerFlick();
    }
}
