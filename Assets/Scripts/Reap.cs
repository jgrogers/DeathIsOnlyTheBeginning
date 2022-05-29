using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reap : MonoBehaviour
{
    [SerializeField] GameObject reapEffect;
    // Start is called before the first frame update
    [SerializeField] bool reapEffectActive = false;
    private Animator animator;
    void Start()
    {
       reapEffect.SetActive(false);
       animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.R) && reapEffectActive == false)
       {
           reapEffectActive = true;
           StartCoroutine(ReapEffect());
       } 
    }
    private IEnumerator ReapEffect() {
        reapEffect.SetActive(true);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1.6f);
        reapEffect.SetActive(false);
        reapEffectActive = false;
    }
}
