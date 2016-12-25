using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

	void Start () {
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

        rabbit.SetSteerAcitve(true);
        blocks.SetActive(true);

        rabbit.DoJump();
    }
    void GameOver()
    {
        state = State.GameOver;

        ScrollObject[] scrollObjects = GameObject.FindObjectsOfType<ScrollObject>();
        foreach (ScrollObject so in scrollObjects) so.enabled = false;
    }
    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
