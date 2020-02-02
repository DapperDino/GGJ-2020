using DapperDino.GGJ2020.Parts;
using UnityEngine;
using UnityEngine.AI;

namespace DapperDino.GGJ2020.Combat
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private float aggroRange = 10f;
        [SerializeField] private float attackRange = 5f;
        [SerializeField] private NavMeshAgent navMeshAgent = null;
        [SerializeField] private WeaponSetupHandler leftArmWeapon = null;
        [SerializeField] private WeaponSetupHandler rightArmWeapon = null;

        private bool rightFire;
        private float nextFire;

        private enum State { Idle, Chasing, Attacking }

        private GameObject player;
        private State state = State.Idle;

        private void Update()
        {
            switch (state)
            {
                case State.Idle:
                    HandleIdle();
                    break;

                case State.Chasing:
                    HandleChasing();
                    break;

                case State.Attacking:
                    HandleAttacking();
                    break;
            }
        }

        private void HandleIdle()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, aggroRange);

            foreach (var collider in colliders)
            {
                if (!collider.CompareTag("Player")) { continue; }

                player = collider.gameObject;

                state = State.Chasing;

                return;
            }
        }

        private void HandleChasing()
        {
            var distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer > aggroRange)
            {
                state = State.Idle;
                player = null;

                navMeshAgent.ResetPath();
            }
            else if (distanceToPlayer <= attackRange)
            {
                state = State.Attacking;
            }

            navMeshAgent.SetDestination(player.transform.position);

            Debug.DrawLine(transform.position, player.transform.position);
        }

        private void HandleAttacking()
        {
            if (player == null) { return; }

            var distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer > attackRange)
            {
                state = State.Chasing;
            }

            navMeshAgent.SetDestination(player.transform.position);

            nextFire -= Time.deltaTime;

            if (nextFire <= 0)
            {
                if (rightFire)
                {
                    rightArmWeapon.Fire();
                }
                else
                {
                    leftArmWeapon.Fire();
                }

                rightFire = !rightFire;

                nextFire = 0.5f;
            }
        }
    }
}
