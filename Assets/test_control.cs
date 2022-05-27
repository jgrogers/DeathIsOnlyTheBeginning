using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_control : MonoBehaviour
{
    public Animator animator;
    void Awake() {
        animator = GetComponent<Animator>();
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            animator.SetTrigger("Jump");
        }
        animator.SetBool("Walking", Input.GetKey(KeyCode.W));
        animator.SetBool("Crouching", Input.GetKey(KeyCode.C));
        if (Input.GetKeyDown(KeyCode.R)) {
            animator.SetTrigger("Attack");
        }
    }
}
