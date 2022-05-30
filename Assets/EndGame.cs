using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(endGame()); 
    }
    IEnumerator endGame() {
        yield return new WaitForSeconds(20.0f);
        Application.Quit();
    }

}