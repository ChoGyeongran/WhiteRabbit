using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class OKButton : MonoBehaviour {

    int score;

    private void Start()
    {
        score = PlayerPrefs.GetInt("Score");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Debug.Log("Score : " + score);
    }

    public void OkButton()
    {
        SceneManager.LoadScene(0);
    }
}
