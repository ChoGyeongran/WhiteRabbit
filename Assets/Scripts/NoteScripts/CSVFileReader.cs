using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

using UnityEngine.UI;

using System.Text;

public class CSVFileReader : MonoBehaviour {

    void Awake()
    {
        LoadCSVFile("Assets\\CSV\\test.csv");
    }

    void Update()
    {
    }

    void LoadCSVFile(string fileName)
    {


        StreamReader sr = new StreamReader(fileName, System.Text.Encoding.Default);

        string strLineValue = null;
        string[] keys = null;
        string[] values = null;

        while ((strLineValue = sr.ReadLine()) != null)
        {
            // Must not be empty.
            if (string.IsNullOrEmpty(strLineValue)) return;
            values = strLineValue.Split(',');
        }
    }
}

