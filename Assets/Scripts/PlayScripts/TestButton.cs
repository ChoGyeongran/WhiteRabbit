using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestButton : MonoBehaviour {

    void ClearCheck()
    {
        int data = GameController.GetScore();
        PlayerPrefs.SetInt("Score", data);
        SceneManager.LoadScene(5);
    }

    public void ClearButton()
    //임시 버튼
    {
        ClearCheck();
    }
}
