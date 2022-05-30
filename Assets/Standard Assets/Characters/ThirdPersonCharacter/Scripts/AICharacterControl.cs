using System;
using UnityEngine;
using Random = UnityEngine.Random;
using NavMesh = UnityEngine.AI.NavMesh;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Vector3 target = new Vector3();

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;
            target = RandomNavmeshLocation(20f);
        }


        private void Update()
        {
            if (target != null)
                agent.SetDestination(target);

            if (agent.remainingDistance > agent.stoppingDistance)
                character.Move(agent.desiredVelocity, false, false);
            else {
                character.Move(Vector3.zero, false, false);
                target = RandomNavmeshLocation(100f);
            }
            agent.speed = 0.5f;
        }


        public void SetTarget(Vector3 newtarget)
        {
            target = newtarget;
        }
        public Vector3 RandomNavmeshLocation(float radius) {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += transform.position;
            UnityEngine.AI.NavMeshHit hit;
            Vector3 finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
                finalPosition = hit.position;            
            }
            return finalPosition;
        }
    }
}
