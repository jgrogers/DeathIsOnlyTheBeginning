using System;
using UnityEngine;
using Random = UnityEngine.Random;
using NavMesh = UnityEngine.AI.NavMesh;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    public class GhostControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public GameObject target;                                    // target to aim for
        [SerializeField] AudioClip[] birthClips;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            target = GameObject.FindGameObjectWithTag("Player");
	        agent.updateRotation = false;
	        agent.updatePosition = true;
            if (birthClips.Length != 0)
                GetComponent<AudioSource>().PlayOneShot(birthClips[Random.Range(0,birthClips.Length)]);
        }


        private void Update()
        {
            if (target != null) {
                agent.SetDestination(target.transform.position);
                transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up);
           }
        }
   }
}
