using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OKButton : MonoBehaviour {

    public void OkButton()
    {
        SceneManager.LoadScene(0);
    }
}
