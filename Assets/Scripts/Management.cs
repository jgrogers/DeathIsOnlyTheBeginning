using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.SceneManagement;
using NavMesh = UnityEngine.AI.NavMesh;

public class Management : MonoBehaviour
{
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI soulsText;
    [SerializeField] GameObject lifetimerPrefab;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] List<GameObject> targetPrefab;
    [SerializeField] GameObject ghostPrefab;
    [SerializeField] GameObject playerHealthBar;

    public Canvas canvas;
    [SerializeField] int maxTargets = 3;
    [SerializeField] float lifetimerOffset = 100.0f;
    [SerializeField] float playerOriginSampleRadius = 100f;
    [SerializeField] Cinemachine.CinemachineVirtualCamera playerCamera;
    [SerializeField] MinimapCameraControl minimapCameraScript;
    [SerializeField] float minLifetime = 30.0f;
    [SerializeField] float maxLifetime = 120.0f;
    [SerializeField] int soulsNeededToWin = 4;
    private int numTargets = 0;
    private List<GameObject> lifetimers = new List<GameObject>();
    private List<GameObject> targets = new List<GameObject>();
    private List<GameObject> ghosts  = new List<GameObject>();
    private List<float> lifetimes = new List<float>();
    private GameObject player;
    PersistantData data;
    private bool isTransitioning = false;
    private List<Color> targetHighlightColors = new List<Color>{Color.red, Color.blue, Color.green, Color.yellow, Color.cyan};
    // Start is called before the first frame update
    void Awake()
    {
        dayText.text = "Day: " + PersistantData.Instance.day;
        soulsText.text = "Souls: " + PersistantData.Instance.souls;
        dayText = GameObject.FindGameObjectWithTag("DayText").GetComponent<TextMeshProUGUI>();
        soulsText = GameObject.FindGameObjectWithTag("SoulsText").GetComponent<TextMeshProUGUI>();
        canvas = GameObject.FindObjectOfType<Canvas>();
        dayText.gameObject.SetActive(false);
        player = Instantiate<GameObject>(playerPrefab, PersistantData.Instance.origin.transform.position, Quaternion.identity);
        playerCamera.Follow = player.transform;
        playerCamera.LookAt = player.transform;
        minimapCameraScript.SetTarget(player);
        numTargets = Random.Range(1, maxTargets+1);
        for (int i = 0; i < numTargets; i++) {
            float lifetime = Random.Range(minLifetime, maxLifetime);
            lifetimes.Add(lifetime);
            GameObject lifetimer =
                Instantiate<GameObject>(lifetimerPrefab, canvas.transform);
            Vector3 currentLifetimerPosition = lifetimer.transform.localPosition;
            lifetimer.transform.localPosition = new Vector3(-lifetimerOffset * i, 0.0f, 0.0f) + currentLifetimerPosition;
            lifetimer.GetComponent<Lifetimer>().SetFilledProp(lifetime / maxLifetime);
            lifetimer.GetComponent<Lifetimer>().SetBackgroundColor(targetHighlightColors[i]); 
            lifetimers.Add(lifetimer);
            int targetPrefabChosen = Random.Range(0, targetPrefab.Count);

            GameObject target = Instantiate<GameObject>(targetPrefab[targetPrefabChosen], RandomNavmeshLocation(PersistantData.Instance.origin.transform.position, PersistantData.Instance.targetRadius), Quaternion.identity);
            target.GetComponent<TargetDeath>().SetHaloColor(targetHighlightColors[i]);
            targets.Add(target);
        }
        playerHealthBar.GetComponent<HealthDisplay>().target = player;
    }
    void Start() {
        StartCoroutine(animateDayText());
    }
    public void IncrementSouls() {
        PersistantData.Instance.souls++;
        soulsText.text = "Souls: " + PersistantData.Instance.souls;
    }
    private IEnumerator animateDayText() {
        yield return new WaitForSeconds(0.5f);
        dayText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        dayText.gameObject.SetActive(false);

    }
    void Update() {
        if (!isTransitioning) {
            int numDead = 0;
            int ghostsDead = 0;
            for (int ind = 0; ind < targets.Count; ind++ ) {
                if (targets[ind].GetComponent<TargetDeath>().isDying) {
                    numDead ++;
                    lifetimers[ind].GetComponent<Lifetimer>().SetFilledProp(0.0f);
                } else {
                    if (!targets[ind].GetComponent<TargetDeath>().isGhost) {
                        lifetimes[ind] -= Time.deltaTime;
                        if (lifetimes[ind] <= 0.0f) {
                            targets[ind].GetComponent<TargetDeath>().Ghost();
                            object[] parms = new object[1]{targets[ind]};
                            StartCoroutine("SpawnGhost", parms);
                        }
                        lifetimers[ind].GetComponent<Lifetimer>().SetFilledProp(Mathf.Max(0.0f, lifetimes[ind] / maxLifetime));
                    }
                } 
             }
             for (int ind = 0; ind < ghosts.Count; ind++) {
                 if (ghosts[ind].GetComponent<GhostDeath>().isDying) {
                     ghostsDead ++;
                 }
             }
            if (numDead + ghostsDead == targets.Count) {
                isTransitioning = true;
                StartCoroutine(EndOfDay());
            }
        }
        if (Input.GetKeyDown(KeyCode.N)) {
            isTransitioning = true;
            NextDay();
        }
        if (player.transform.position.y < -20f) { // Fell off the world?
            NextDay();
        }
        if (PersistantData.Instance.souls >= soulsNeededToWin) {
            YouWin();
        }
        if (PersistantData.Instance.playerHealth <= 0f) {
            GameOver();
        }
        playerHealthBar.GetComponent<HealthDisplay>().healthProp = PersistantData.Instance.playerHealth / PersistantData.Instance.playerMaxHealth;
     }
    IEnumerator SpawnGhost(object[] parms) {
        
        yield return new WaitForSeconds(1.5f);
        ghosts.Add(Instantiate(ghostPrefab, ((GameObject)parms[0]).transform.position, ((GameObject)parms[0]).transform.rotation));

    }
    IEnumerator EndOfDay() {
        yield return new WaitForSeconds(6.0f);
        NextDay();
    }
    void NextDay() {
        PersistantData.Instance.day ++;
        PersistantData.Instance.origin.transform.position = RandomNavmeshLocation(PersistantData.Instance.origin.transform.position, playerOriginSampleRadius);
        SceneManager.LoadScene(1);
    }
    void GameOver() {
        Debug.Log("Game Over!");
    }
    void YouWin() {
        Debug.Log("You Win!");
    }
    public Vector3 RandomNavmeshLocation(Vector3 origin, float radius) {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += origin;
        UnityEngine.AI.NavMeshHit hit;
        Vector3 finalPosition = origin;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
            finalPosition = hit.position;            
        } else {
            finalPosition = PersistantData.Instance.origin.transform.position;
        }
        return finalPosition;
    }
 
}
