using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

public class Management : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dayText;
    [SerializeField] TextMeshProUGUI soulsText;
    [SerializeField] GameObject lifetimerPrefab;
    [SerializeField] Canvas canvas;
    [SerializeField] int day = 1;
    [SerializeField] int maxTargets = 3;
    [SerializeField] float lifetimerOffset = 100.0f;
    private int souls = 0;
    private int numTargets = 0;
    private List<GameObject> lifetimers = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        dayText.text = "Day: " + day;
        soulsText.text = "Souls: " + souls;
        dayText.gameObject.SetActive(false);
        numTargets = Random.Range(1, maxTargets);
        for (int i = 0; i < numTargets; i++) {
            GameObject lifetimer =
                Instantiate<GameObject>(lifetimerPrefab, canvas.transform);
            Vector3 currentLifetimerPosition = lifetimer.transform.localPosition;
            lifetimer.transform.localPosition = new Vector3(-lifetimerOffset * i, 0.0f, 0.0f) + currentLifetimerPosition;
            lifetimer.GetComponent<Lifetimer>().SetFilledProp(Random.Range(0.0f, 1.0f));
        }
    }
    void Start() {
        StartCoroutine(animateDayText());
    }
    public void IncrementSouls() {
        souls++;
        soulsText.text = "Souls: " + souls;
    }
    private IEnumerator animateDayText() {
        yield return new WaitForSeconds(0.5f);
        dayText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        dayText.gameObject.SetActive(false);

    }
}
