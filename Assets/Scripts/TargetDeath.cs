using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDeath() {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        //GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<CapsuleCollider>().material = null;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().enabled =false;
        GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().enabled = false;
        GetComponent<Animator>().enabled = false;
    }
}
