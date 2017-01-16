using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class BlockScrollObject : MonoBehaviour
{

    public float speed = 1.0f;
    public float startPosition;
    public float endPosition;

    public CSVFileReader csvFileReader;
    public CSVController csvController;

    private string[] blockName = {
        "Block1",
        "Block2",
        "Block3",
        "Block4",
        "Block5",
        "Block6",
        "Block7",
        "Block8",
        "Block9",
        "Block10",
        "Block11",
        "Block12",
        "Block13",
        "Block14",
        "Block15",
        "Block16"
    };
    
    void Start()
    {
        SetBlocksActive();
        csvController.addCount();
        //for (int i = 0; i < 16; i++)
        //    transform.FindChild(blockName[i]).gameObject.SetActive(false);
    }

    void Update()
    {
        //일정시간마다 오브젝트를 왼쪽으로 이동
        transform.Translate(-1 * speed * Time.deltaTime, 0, 0);

        if (transform.position.x <= endPosition) ScrollEnd();

        if (Input.GetKeyDown(KeyCode.C))
            ClearCheck();

    }

    void ScrollEnd()
    {
        print(transform.name);

        //시작지점으로 다시 이동
        SetBlocksActive();
        csvController.addCount();

        transform.Translate(-1 * (endPosition - startPosition), 0, 0);

        SendMessage("OnScrollEnd", SendMessageOptions.DontRequireReceiver);
    }



    void SetBlocksActive()
    {
        //그룹.transform.parent = 비활성화게임오브젝트.transform;
        //Transform[] AllData = transform.GetComponentsInChildren<Transform>();

        int line = csvController.getCount();
        
        print(line);
        /*
        print(csvFileReader.notes[line].note[3]);
        print(csvFileReader.notes[line].note.Length);

        print("오브젝트 갯수"+ AllData.Length);
        */
        if(line >= csvFileReader.notes.Count) return;

//        print(transform.FindChild(blockName[0]).name);
 
//        transform.FindChild(blockName[0]).gameObject.SetActive(false);

//        transform.gameObject.SetActive(false);
//        AllData[0].gameObject.SetActive(false);

        //블록을 전부 비활성화
        for (int i = 0; i < 16; i++)
            transform.FindChild(blockName[i]).gameObject.SetActive(false);

        string sss = "";
        for (int i = 0; i < csvFileReader.notes[line].note.Length; i++)
        {
            sss += csvFileReader.notes[line].note[i] + " ";
        }

        print(sss);

        for (int i = 0; i < csvFileReader.notes[line].note.Length; i++)
        {
            if (csvFileReader.notes[line].note[0].Equals("E")) ClearCheck();
            else
            {
                int exist = Convert.ToInt32(csvFileReader.notes[line].note[i]);
                if (exist == 1)
                    transform.FindChild(blockName[i]).gameObject.SetActive(true);
                //                AllData[i + 1].gameObject.SetActive(true);
            }
        }
    }

    void ClearCheck()
    {
        int data = GameController.GetScore();
        PlayerPrefs.SetInt("Score", data);
        SceneManager.LoadScene(5);
    }

}
