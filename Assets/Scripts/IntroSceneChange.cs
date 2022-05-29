using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneChange : MonoBehaviour
{
    public void OnFinishTitle() {
        SceneManager.LoadScene(1);
    }
}
