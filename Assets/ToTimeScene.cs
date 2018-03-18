using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTimeScene : MonoBehaviour {

    public void ToTime()
    {
        SceneManager.LoadScene("TimeAttackScene");
    }

}
