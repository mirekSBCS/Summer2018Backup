using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
        public Animator animator;
        public int AttackDistance;

		private void Awake(){
			animator = GetComponent<Animator>();
			animator.SetBool("IsAttacking", false);
		}

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;
        }


        private void Update()
        {
            if (target != null)
            {
                agent.SetDestination(target.position);
            }

            agent.isStopped = true;

            if (agent.remainingDistance <= AttackDistance)
            {
                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    agent.isStopped = false;
                    character.Move(agent.desiredVelocity, false, false);
                    animator.SetBool("IsAttacking", false);
                }
                else
                {
                    character.Move(Vector3.zero, false, false);
                    animator.SetBool("IsAttacking", false);
                }
            }
        }



        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
