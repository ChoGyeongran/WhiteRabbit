using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

using UnityEngine.UI;

using System.Text;

public class CSVFileReader : MonoBehaviour {

    public List<Note> notes = new List<Note>();

    String strLineValue = null;
    String[] values = null;

    string[] noteList = null;

    void Awake()
    {
        LoadCSVFile("Assets\\CSV\\test.csv");
    }

    void Update()
    {
    }

    void LoadCSVFile(String fileName)
    {
        
        
        TextAsset _txtFile = (TextAsset)Resources.Load("CSV\\test") as TextAsset;
        string fileFullPath = _txtFile.text;
        string[] stringList = fileFullPath.Split('\n');

        for (int i = 0; i < stringList.Length; i++)
        {
            if (String.IsNullOrEmpty(stringList[i])) return;

            noteList = stringList[i].Split(',');
            Note temp = new Note(noteList);
            notes.Add(temp);
        }


        /*
        StreamReader sr = new StreamReader(fileName, System.Text.Encoding.Default);

        while ((strLineValue = sr.ReadLine()) != null)
        {
            // Must not be empty.
            if (String.IsNullOrEmpty(strLineValue)) return;

            values = strLineValue.Split(',');
            Note temp = new Note(values);
            notes.Add(temp);
            //values[x]; 의 형태로 접근할 수 있습니다.
        }

        */

        //        print(notes.Count);
        //        print(notes[0].note.Length);
        //        print(notes[0].note[0]);
/*
        string sss = "";
        for (int i = 0; i < notes[1].note.Length; i++)
            sss += notes[1].note[i];

        print(sss);
*/
        
    }
}

