using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Net;
using System.IO;

public class Server : MonoBehaviour
{

    //클라이언트 리스트
    private List<ServerClient> clients;
    private List<ServerClient> disconnectList;

    //포트, TcpListener 연결을 받는애, 서버 시작 여부
    public int port = 6321;
    private TcpListener server;
    private bool serverStartecd;

    private void Start()
    {
        clients = new List<ServerClient>();
        disconnectList = new List<ServerClient>();

        //ip와 포트로 받을 생성 및 준비
        try
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();

            StartListening();
            //서버가 연결된 상태
            serverStartecd = true;
            Debug.Log("Server has been started on port " + port.ToString());
        }
        //서버 연결에 실패할 경우
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e.Message);
        }
    }

    private void Update()
    {
        //서버가 연결되 있지 않으면 리턴
        if (!serverStartecd)
            return;

        //모든 클라이언트에 대한 정보를 하나하나 검사한다.
        foreach (ServerClient c in clients)
        {
            // Is the client still connected?
            //클라이언트가 연결 되어있지 않으면 해당 클라이언트의 tcp를 닫고
            //disconnectList에 추가
            if (!IsConnected(c.tcp))
            {
                c.tcp.Close();
                disconnectList.Add(c);
                continue;
            }
            //클라이언트가 연결 되어있을 경우
            //Check for message from the client
            else
            {
                //클라이언트에서 스트림을 읽어온다.
                NetworkStream s = c.tcp.GetStream();
                //스트림에 데이터가 있을 경우
                if (s.DataAvailable)
                {
                    //스트림리더로 데이터를 모두 가져와
                    //한줄씩 data에 읽는다.
                    //data가 null이 아닐 경우
                    StreamReader reader = new StreamReader(s, true);
                    string data = reader.ReadLine();

                    if (data != null)
                        OnIncomingData(c, data);
                }
            }
        }

        for (int i = 0; i < disconnectList.Count - 1; i++)
        {
            Broadcast(disconnectList[i].clientName + " has disconnected", clients);

            clients.Remove(disconnectList[i]);
            disconnectList.RemoveAt(i);
        }
    }

    private void StartListening()
    {
        server.BeginAcceptSocket(AcceptTcpClient, server);
    }

    private bool IsConnected(TcpClient c)
    {
        try
        {
            if (c != null && c.Client != null && c.Client.Connected)
            {
                if (c.Client.Poll(0, SelectMode.SelectRead))
                {
                    return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                }
                return true;
            }
            else
                return false;
        }
        catch
        {
            return false;
        }

    }

    private void AcceptTcpClient(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener)ar.AsyncState;

        clients.Add(new ServerClient(listener.EndAcceptTcpClient(ar)));
        StartListening();

        //Send a message to everyone, say someone has connected
        Broadcast("%NAME", new List<ServerClient>() { clients[clients.Count - 1] });
    }


    private void OnIncomingData(ServerClient c, string data)
    {
        //Debug.Log("서버 : 클라 연결을 받을때 뜨는 메세지");
        //&NAME가  data에 포함되어 있을 경우
        if (data.Contains("&NAME"))
        {
            c.clientName = data.Split('|')[1];
            //접속한 클라에 따라 다른 접속 메세지
            //Broadcast("전송!",clients);
            Broadcast(c.clientName + " has connected!", clients);
            return;
        }
        //접속한 클라가 어떤 채팅을 치는지를 모든 클라에 전송
        //Broadcast(c.clientName + " : " + data, clients);
        Debug.Log(data);
        Broadcast("전송 " + data, clients);
    }

    private void Broadcast(string data, List<ServerClient> cl)
    {
        foreach (ServerClient c in cl)
        {
            try
            {
                StreamWriter writer = new StreamWriter(c.tcp.GetStream());
                writer.WriteLine(data);
                writer.Flush();
            }
            catch (Exception e)
            {
                Debug.Log("Write error : " + e.Message + " to client " + c.clientName);
            }
        }
    }

}

// 클라이언트 클래스
// TcpClient 연결을 보내는 애, 클라이언트 이름을 저장
public class ServerClient
{
    public TcpClient tcp;
    public string clientName;

    public ServerClient(TcpClient clientSocket)
    {
        clientName = "Guest";
        tcp = clientSocket;
    }

}