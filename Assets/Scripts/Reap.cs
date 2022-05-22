using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reap : MonoBehaviour
{
    [SerializeField] GameObject reapEffect;
    public Animator anim;
    // Start is called before the first frame update
    [SerializeField] bool reapEffectActive = false;
    void Start()
    {
       anim = GetComponent<Animator>(); 
       reapEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.R) && reapEffectActive == false)
       {
           reapEffectActive = true;
           //anim.SetTrigger("Reap");
           StartCoroutine(ReapEffect());
       } 
    }
    private IEnumerator ReapEffect() {
        reapEffect.SetActive(true);
        reapEffect.GetComponent<Animator>().SetTrigger("Reap");
        yield return new WaitForSeconds(1.0f);
        reapEffect.SetActive(false);
        reapEffectActive = false;
    }
}
