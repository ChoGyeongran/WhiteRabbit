using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainButton : MonoBehaviour {
    public void EpisodeButton()
    {
        SceneManager.LoadScene(3);
    }

    public void SettingButton()
    {
        SceneManager.LoadScene(4);
    }

    public void BackButton()
    {
        Debug.Log("게임종료");
    }
}
