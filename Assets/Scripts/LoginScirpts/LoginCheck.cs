﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginCheck : MonoBehaviour {

    public InputField inputId;

    string CheckIdURL = "localhost/CheckID.php";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoginButton()
    {
        StartCoroutine( CheckId(inputId.text));
    }

    IEnumerator CheckId(string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("idPost", id);

        WWW www = new WWW(CheckIdURL, form);

        yield return www;
        Debug.Log(www.text);

        if (www.text.Equals("y")) {
            Debug.Log("successful");
            SceneManager.LoadScene(0);
        }
        else {
            Debug.Log("fail");
        }
    }
}
