using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Singleton : MonoBehaviour {

    private static Singleton _instance = null;

    public TcpClient socket;

    public static Singleton Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("cSingleton == null");
            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
        _instance = this;
    }

}
