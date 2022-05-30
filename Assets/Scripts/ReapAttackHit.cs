using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReapAttackHit : MonoBehaviour
{
    private Management management;
    // Start is called before the first frame update
    void Start()
    {
        management = FindObjectOfType<Management>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            if (!other.gameObject.GetComponentInParent<TargetDeath>().isDying && !other.gameObject.GetComponentInParent<TargetDeath>().isGhost) {

                Debug.Log("Hit " + other.gameObject.name);
                management.IncrementSouls();
                other.gameObject.GetComponentInParent<TargetDeath>().OnDeath();
            }
        }
        if (other.gameObject.tag == "Ghost") {
            if (!other.gameObject.GetComponentInParent<GhostDeath>().isDying) {

                Debug.Log("Hit " + other.gameObject.name);
                management.IncrementSouls();
                other.gameObject.GetComponentInParent<GhostDeath>().OnDeath();
            }
 
        }
    }
}
