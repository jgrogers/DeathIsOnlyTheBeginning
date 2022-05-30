using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteController : MonoBehaviour
{
    public GameObject target;                                    // target to aim for
    public Animator animator;
    [SerializeField] float biteRange = 4.0f;
    [SerializeField] float biteDamage = 20.0f;

    private void Start()
    {
        // get the components on the object we need ( should not be null due to require component so no need to check )
        target = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (target != null) {
            if (Vector3.Distance(transform.position, target.transform.position) < biteRange) {
                animator.SetTrigger("Bite");
            }
        }
    }
    public void BiteEvent() {
        target.GetComponent<HealthManagement>().TakeDamage(biteDamage);
    }
}
