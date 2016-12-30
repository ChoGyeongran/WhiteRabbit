using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Stop : MonoBehaviour {

    public Canvas canvas;
    public RabbitController rabbit;
    public GameObject blocks;
    public AudioSource audioSource;

    void Start()
    {
        canvas.enabled = false;
    }

    public void Click()
    {
        //Stop 메뉴를 보여준다
        canvas.enabled = true;

        //캐릭터를 멈춘다
        rabbit.SetSteerAcitve(false);

        //Ground를 멈춘다
        ScrollObject[] scrollObjects = GameObject.FindObjectsOfType<ScrollObject>();
        foreach (ScrollObject so in scrollObjects) so.enabled = false;

        BlockScrollObject[] blockscrollObjects = GameObject.FindObjectsOfType<BlockScrollObject>();
        foreach (BlockScrollObject so in blockscrollObjects) so.enabled = false;

        //SceneManager.LoadScene(1);

        //음악 정지
        audioSource.Pause();
    }
}
