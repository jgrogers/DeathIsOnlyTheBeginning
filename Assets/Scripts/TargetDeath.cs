using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetDeath : MonoBehaviour
{
    public bool isDying = false;
    public bool isGhost = false;
    [SerializeField] ParticleSystem harvestPS;
    [SerializeField] ParticleSystem ghostPS;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Ghost() {
        if (isGhost) return;
        isGhost = true;
        GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
        GetComponent<Animator>().SetTrigger("Die");
        GetComponent<CapsuleCollider>().height = 0.01f;
        StartCoroutine(GhostEffect());
 
    }
    IEnumerator Harvest() {
        harvestPS.Play();
        yield return new WaitForSeconds(2.3f);
        harvestPS.Stop();
    }
    IEnumerator GhostEffect() {
        ghostPS.Play();
        yield return new WaitForSeconds(2.3f);
        ghostPS.Stop();
    }
    public void OnDeath() {
        isDying = true;
        GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
        GetComponent<Animator>().SetTrigger("Die");
        GetComponent<CapsuleCollider>().height = 0.01f;
        StartCoroutine(Harvest());

    }
}
