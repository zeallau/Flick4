using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBoarderScript : MonoBehaviour {

    private GameObject Disc;
    private GameObject DiscText;

    private Vector3 discSpawnPos;

    private float targetRadius;
    private GameObject Target;
    private Vector3 targetSpawnPos;

    // Use this for initialization
    void Start () {
        Disc = GameObject.Find("Disc");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        discSpawnPos = new Vector3(Random.Range(-2.6f, 2.6f), -4f, 0.0f);
        Disc.transform.position = discSpawnPos;

       
        //いろんな方法を試したが、Discを止まりません
        Target.GetComponent<MainControl>().isMove = false;

        //Disc.transform.Translate(0.0f, 0.0f, 0.0f);
        //Disc.GetComponent<Transform>().Translate(0.0f, 0.0f, 0.0f);
        //Disc.GetComponent<MainControl>().clickDistance = new Vector3(0.0f, 0.0f, 0.0f);
        //Disc.GetComponent<MainControl>().movedDistance = 100.0f;
        //Disc.GetComponent<MainControl>().clickDistance.magnitude;

    }
}
