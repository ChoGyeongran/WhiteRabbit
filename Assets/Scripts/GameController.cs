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

    public RabbitController rabbit;
    public GameObject blocks;
    public AudioSource audioSource;

    void Start () {
        audioSource.Pause();
        Ready();		
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
        blocks.SetActive(false);
    }

    void GameStart()
    {
        state = State.Play;

        audioSource.Play();

        rabbit.SetSteerAcitve(true);
        blocks.SetActive(true);

        //rabbit.DoJump();
    }
    void GameOver()
    {
        state = State.GameOver;

        audioSource.Pause();

        ScrollObject[] scrollObjects = GameObject.FindObjectsOfType<ScrollObject>();
        foreach (ScrollObject so in scrollObjects) so.enabled = false;
    }
    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
