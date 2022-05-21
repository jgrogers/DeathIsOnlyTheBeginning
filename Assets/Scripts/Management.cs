using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Management : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dayText;
    [SerializeField] TextMeshProUGUI soulsText;
    [SerializeField] int day = 1;
    private int souls = 0;
    // Start is called before the first frame update
    void Awake()
    {
        dayText.text = "Day: " + day;
        soulsText.text = "Souls: " + souls;
        dayText.gameObject.SetActive(false);
    }
    void Start() {
        StartCoroutine(animateDayText());
    }
    private IEnumerator animateDayText() {
        yield return new WaitForSeconds(0.5f);
        dayText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        dayText.gameObject.SetActive(false);

    }
}
