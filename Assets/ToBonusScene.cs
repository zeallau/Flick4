using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToBonusScene : MonoBehaviour {

    public void ToBonus()
    {
        SceneManager.LoadScene("BonusScene");
    }
}
