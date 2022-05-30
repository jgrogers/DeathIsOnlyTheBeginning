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
    [SerializeField] Light haloLight;
    [SerializeField] GameObject uiHighlightMesh;
    [SerializeField] AudioClip deathSound;
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
        PersistantData.Instance.playerHealth += 5f;
        if (PersistantData.Instance.playerHealth > PersistantData.Instance.playerMaxHealth)
            PersistantData.Instance.playerHealth = PersistantData.Instance.playerMaxHealth;
    }
    public void OnDeath() {
        isDying = true;
        GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
        GetComponent<Animator>().SetTrigger("Die");
        GetComponent<CapsuleCollider>().height = 0.01f;
        uiHighlightMesh.GetComponent<MeshRenderer>().enabled = false;
        haloLight.enabled = false;
        GetComponent<AudioSource>().PlayOneShot(deathSound);
        StartCoroutine(Harvest());

    }
    public void SetHaloColor(Color color) {
        haloLight.color = color;
        uiHighlightMesh.GetComponent<MeshRenderer>().material.color = color;
        uiHighlightMesh.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", color);
        uiHighlightMesh.GetComponent<MeshRenderer>().enabled = true;
    }
}
