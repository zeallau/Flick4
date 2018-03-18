using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBoarder : MonoBehaviour {
    
    private GameObject Disc;
    

    private Vector3 discSpawnPos;

    private float targetRadius;
    private GameObject Target;
    private Vector3 targetSpawnPos;




    // Use this for initialization
    void Start()
    {
        
        Disc = GameObject.Find("Disc");
        Target = GameObject.Find("Target");
        
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("is OnTriggerEnter");

        discSpawnPos = new Vector3(Random.Range(-2.6f, 2.6f), -4f, 0.0f);
        Disc.transform.position = discSpawnPos;

        targetSpawnPos = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(0.0f, 2.0f), 0.0f);
        Target.transform.position = targetSpawnPos;
        targetRadius = Target.GetComponent<CircleCollider2D>().radius;


        


        //いろんな方法を試したが、Discを止まりません
        Target.GetComponent<TimeAttackScript>().isMove = false;

        //Disc.transform.Translate(0.0f, 0.0f, 0.0f);
        //Disc.GetComponent<Transform>().Translate(0.0f, 0.0f, 0.0f);
        //Disc.GetComponent<MainControl>().clickDistance = new Vector3(0.0f, 0.0f, 0.0f);
        //Disc.GetComponent<MainControl>().movedDistance = 100.0f;
        //Disc.GetComponent<MainControl>().clickDistance.magnitude;

    }
}
