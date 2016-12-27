using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StopMenu : MonoBehaviour {

    public Canvas canvas;
    public RabbitController rabbit;
    public GameObject blocks;
    public AudioSource audioSource;

    public void StopContinue()
    {
        //Stop 메뉴를 감춘다
        canvas.enabled = false;

        //캐릭터를 움직인다
        rabbit.SetSteerAcitve(true);

        //Ground를 움직인다
        ScrollObject[] scrollObjects = GameObject.FindObjectsOfType<ScrollObject>();
        foreach (ScrollObject so in scrollObjects) so.enabled = true;

        //음악 재생
        audioSource.Play();
    }

    public void StopRetry()
    {
        //재 로딩한다
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StopHome()
    {
        SceneManager.LoadScene(0);
    }
}
