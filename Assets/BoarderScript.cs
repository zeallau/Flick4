using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoarderScript : MonoBehaviour {

    private int discCount; //どうやってdiscCountの値をMainControlと共有できますか？


    private GameObject Disc;
    private GameObject DiscText;
    
    private Vector3 discSpawnPos;

    private float targetRadius;
    private GameObject Target;
    private Vector3 targetSpawnPos;


    bool getDTC;
    bool scoring;
    bool scoreUp;
    bool scoreUpdate;

    // Use this for initialization
    void Start () {
        DiscText = GameObject.Find("DiscText");
        Disc = GameObject.Find("Disc");
        Target = GameObject.Find("Target");

        //これでdiscCountの値はMainControlのdiscCountに一致していますか
        discCount = Disc.GetComponent<MainControl>().discCount;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        discSpawnPos = new Vector3(Random.Range(-2.6f, 2.6f), -4f, 0.0f);
        Disc.transform.position = discSpawnPos;

        targetSpawnPos = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(0.0f, 2.0f), 0.0f);
        Target.transform.position = targetSpawnPos;
        targetRadius = Target.GetComponent<CircleCollider2D>().radius;


        getDTC = true;
        scoring = false;
        scoreUp = false;
        scoreUpdate = false;


        discCount -= 1;
        DiscText.GetComponent<Text>().text = "Disc: " + discCount + " / 10";


        //いろんな方法を試したが、Discを止まりません
        Disc.GetComponent<MainControl>().isMove = false;
        Disc.transform.Translate(0.0f, 0.0f, 0.0f);
        Disc.GetComponent<Transform>().Translate(0.0f, 0.0f, 0.0f);
        Disc.GetComponent<MainControl>().clickDistance = new Vector3(0.0f, 0.0f, 0.0f);
        Disc.GetComponent<MainControl>().movedDistance = 100.0f;
        //Disc.GetComponent<MainControl>().clickDistance.magnitude;
        
    }

}
