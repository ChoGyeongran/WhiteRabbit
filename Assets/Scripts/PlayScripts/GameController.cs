using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    enum State
    {
        Ready,
        Play,
        GameOver
    }

    State state;
    static int score;

    public RabbitController rabbit;
    public GameObject blocks;
    public AudioSource audioSource;
    public CSVFileReader csvController;
    
    public Text scoreLabel;

    void Start () {
        score = 0;
        audioSource.Pause();
        GameStart();
        //        Ready();
	}

    void SetBlocksActive(bool active)
    {
        blocks.SetActive(false);
        Transform[] AllData = blocks.GetComponentsInChildren<Transform>();

//        print(csvController.notes[0].note[0]);

        foreach (Transform Obj in AllData)
        {
//            if (Obj.name == "Blocks" || Obj.name == "Word1" || Obj.name == "Word2" || Obj.name == "Word3") continue;

            Obj.gameObject.SetActive(active); //true 밖에 안됨 
        }
    }

    void LateUpdate()
    {
        switch (state)
        {
            case State.Ready:
                if (Input.GetButtonDown("Fire1")) GameStart(); break;
            case State.Play:
                if (rabbit.IsDead()) GameOver(); break;
            case State.GameOver:
                if (Input.GetButtonDown("Fire1")) Reload(); break;
        }
    }

    void Ready()
    {
        state = State.Ready;

        rabbit.SetSteerAcitve(false);
        //SetBlocksActive(false);
        blocks.SetActive(false);
    }

    void GameStart()
    {
        state = State.Play;

        audioSource.Play();

        rabbit.SetSteerAcitve(true);
        SetBlocksActive(true);
        //blocks.SetActive(true);
        //rabbit.DoJump();
    }
    void GameOver()
    {
        state = State.GameOver;

        audioSource.Pause();

        ScrollObject[] scrollObjects = GameObject.FindObjectsOfType<ScrollObject>();
        foreach (ScrollObject so in scrollObjects) so.enabled = false;

        BlockScrollObject[] blockscrollObjects = GameObject.FindObjectsOfType<BlockScrollObject>();
        foreach (BlockScrollObject so in blockscrollObjects) so.enabled = false;
    }
    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IncreaseScore()
    {
        score+=10;
        scoreLabel.text = "Score : " + score;
    }
    
    static public int GetScore()
    {
        return score;
    }
}
