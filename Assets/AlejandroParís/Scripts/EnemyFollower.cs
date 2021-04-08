using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPGCharacterAnimsFREE.Actions;

namespace RPGCharacterAnimsFREE
{
    public class EnemyFollower : MonoBehaviour
    {
        public EnemyStats stats;
        public GameObject detectionZone;
        public bool move = true;
        NavMeshAgent agent;

        // Start is called before the first frame update
        void Start()
        {
            stats.Target = GameObject.Find("Player");
            agent = GetComponent<NavMeshAgent>();
            StartCoroutine(MoveEnemy());
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<RPGCharacterController>().StartAction("Knockback", new HitContext(1, Vector3.back));
                move = false;
            }
        }
        IEnumerator MoveEnemy()
        {
            if(move)
            {
                agent.SetDestination(stats.Target.transform.position);
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(MoveEnemy());
            }
            else
            {
                stats.Target.GetComponent<PlayerStats>().seconds -= 10;
                yield return new WaitForSeconds(2);
                move = true;
                StartCoroutine(MoveEnemy());
            }
        }
    }
}
