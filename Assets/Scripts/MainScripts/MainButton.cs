using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainButton : MonoBehaviour {
    public void EpisodeButton()
    {
        SceneManager.LoadScene(2);
    }

    public void SettingButton()
    {
        SceneManager.LoadScene(3);
    }

    public void BackButton()
    {
        Debug.Log("게임종료");
    }
}
