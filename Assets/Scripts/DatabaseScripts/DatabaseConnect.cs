using System;
using System.Data;
using System.ComponentModel;
using System.Linq;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;

public class DatabaseConnect {

	// Use this for initialization
	void Start () {
        
        MySqlConnection Con = new MySqlConnection();
        Con.ConnectionString = "Data Source=localhost;Database=TEST;"
                                                              + "User Id=root;Password=tlfcjs42" + ";charset=euckr";
        Con.Open();
        string Rec;
        MySqlCommand Com = new MySqlCommand("SELECT * FROM scoretable", Con);
        MySqlDataReader R;
        R = Com.ExecuteReader();


        while (R.Read())
        {
            Rec = string.Format("ID: {0}, SCORE: {1}", R["id"], R["score"]);
        }

        Debug.Log("로딩 성공");

        R.Close();
        Con.Close();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
