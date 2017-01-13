using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{

    public GameObject chatContainer;
    public GameObject messagePrefab;

    public string clientName;

    private bool socketReady; //서버 연결 여부
    private TcpClient socket;
    private NetworkStream stream;
    private StreamWriter writer;
    private StreamReader reader;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void ConnectToServer()
    {
        //이미 Connect된 상태일 경우 이 버튼은 작동되지 않는다.
        if (socketReady)
            return;

        string host = "192.168.0.15";
        int port = 6321;

        /* 사용자에게 접속할 ip와 포트를 직접 입력받는 코드
        string h;
        int p;
        h = GameObject.Find("HostInput").GetComponent<InputField>().text;
        if (h != "")
            host = h;
        int.TryParse(GameObject.Find("PortInput").GetComponent<InputField>().text, out p);
        if (p != 0)
            port = p;
        */

        try
        {
            socket = new TcpClient(host, port);
            stream = socket.GetStream(); //소켓 스트림을 받아온다.
            writer = new StreamWriter(stream); //스트림 쓰는애
            reader = new StreamReader(stream); //스트림 읽는애
            socketReady = true;
        }
        catch (Exception e)
        {
            Debug.Log("Socket error : " + e.Message);
        }

    }

    private void Update()
    {
        // Connect상태 일때만 검사
        if (socketReady)
        {
            //소켓 버퍼에 읽어 들여야 할 데이터가 있는지 확인
            //1. 자기자신 클라이언트에서 쓸 때
            //2. 다른 클라이언트의 메세지를 서버가 쓸 때 
            if (stream.DataAvailable)
            {
                //스트림을 문자열 data로 읽어온다.
                string data = reader.ReadLine();
                //data가 null이 아닌 경우 
                if (data != null)
                    OnIncomingData(data);
            }
        }
    }

    //프리팹이 추가되는 모든 순간에 호출
    private void OnIncomingData(string data)
    {
        //Debug.Log("클라 : 서버에 접속할때 뜨는 메세지");

        if (data == "%NAME")
        {
            Send("&NAME|" + clientName);
            return;
        }

        //Debug.Log("Server : " + data);
        GameObject go = Instantiate(messagePrefab, chatContainer.transform) as GameObject;
        go.GetComponentInChildren<Text>().text = data;
        //Debug.Log("채팅 추가");
    }

    //서버로 데이터를 날리는 함수
    public void Send(string data)
    {
        if (!socketReady)
            return;

        writer.WriteLine(data);
        writer.Flush();
    }

    //Send() 함수를 호출 할 뿐인 버튼
    public void OnSendButton()
    {
        string message = GameObject.Find("SendInput").GetComponent<InputField>().text;
        Send(message);
    }

    private void CloseSocket()
    {
        if (!socketReady)
            return;

        writer.Close();
        reader.Close();
        socket.Close();
        socketReady = false;
    }

    private void OnApplicationQuit()
    {
        CloseSocket();
    }

    private void OnDisable()
    {
        CloseSocket();
    }

}
