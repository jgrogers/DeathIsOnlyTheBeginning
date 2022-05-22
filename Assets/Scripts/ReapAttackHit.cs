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
            Debug.Log("Hit");
            management.IncrementSouls();
            other.gameObject.GetComponentInParent<TargetDeath>().OnDeath();
        }
    }
}
