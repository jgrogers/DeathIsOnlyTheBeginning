using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostDeath : MonoBehaviour
{
    public bool isDying = false;
    [SerializeField] ParticleSystem harvestPS;
    [SerializeField] float shrinkRate = 0.5f;
    private Rigidbody mRigidbody;
    [SerializeField] AudioClip deathSound;
    void Start() {
        mRigidbody = GetComponent<Rigidbody>();
    }
    void Update() {
        if (isDying) {
            
            Vector3 forceVector = Quaternion.Euler(Random.Range(0f, 0.1f), Random.Range(0f, 0.1f), Random.Range(0f, 0.1f)) * Vector3.up * 50f;
            mRigidbody.AddForce(forceVector, ForceMode.Impulse);
            if (transform.localScale.x > 0) transform.localScale -= Vector3.one * shrinkRate * Time.deltaTime;
            else {
                //Destroy(this.gameObject);
            }
        }
    }
    
    IEnumerator Harvest() {
        harvestPS.Play();
        yield return new WaitForSeconds(2.3f);
        harvestPS.Stop();
        PersistantData.Instance.playerHealth += 10f;
        if (PersistantData.Instance.playerHealth > PersistantData.Instance.playerMaxHealth)
            PersistantData.Instance.playerHealth = PersistantData.Instance.playerMaxHealth;
    }
    
    public void OnDeath() {
        isDying = true;
        GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
        GetComponentInChildren<Animator>().enabled = false;
        GetComponent<AudioSource>().PlayOneShot(deathSound);
        StartCoroutine(Harvest());

    }
}
