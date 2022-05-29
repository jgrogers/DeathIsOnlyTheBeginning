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

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            target = GameObject.FindGameObjectWithTag("Player");
            Debug.Log("I found target " + target.name);
	        agent.updateRotation = false;
	        agent.updatePosition = true;
        }


        private void Update()
        {
            if (target != null)
                agent.SetDestination(target.transform.position);

        }
    }
}
