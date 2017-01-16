using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Net.Sockets;
using System.IO;

public class OKButton : MonoBehaviour {

    int score;
    string id;

    private NetworkStream stream;
    private StreamWriter writer;
    private StreamReader reader;

    private void Start()
    {
        score = PlayerPrefs.GetInt("Score");
        id = PlayerPrefs.GetString("Id");

        stream = Singleton.Instance.socket.GetStream(); //소켓 스트림을 받아온다.
        writer = new StreamWriter(stream); //스트림 쓰는애
        reader = new StreamReader(stream); //스트림 읽는애
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Score : " + score);
            Debug.Log("Id : " + id);
        }
    }

    public void OkButton()
    {
        writer.WriteLine(id +"," + score);
        writer.Flush();

        SceneManager.LoadScene(1);
    }
}
