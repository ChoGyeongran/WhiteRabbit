using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour {

    
    void OnMouseDown()
    {
        SceneManager.LoadScene(1);
    }
}
