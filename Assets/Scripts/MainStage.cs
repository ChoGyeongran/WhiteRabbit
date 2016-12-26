using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainStage : MonoBehaviour {

    public void Select()
    {
        SceneManager.LoadScene(1);
    }
}
