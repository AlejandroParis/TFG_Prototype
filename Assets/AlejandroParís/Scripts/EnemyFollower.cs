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
        public GameObject Children;
        public bool move = true;
        public bool damage = false;
        public int life;
        Rigidbody rbd;
        NavMeshAgent agent;
        public GameObject originalFather;
        Vector3 originalScale;
        Quaternion originalRotation;
        bool detected = false;

        // Start is called before the first frame update
        void Start()
        {
            detectionZone = transform.parent.transform.Find("Zone").gameObject;
            originalScale = transform.localScale;
            originalRotation = transform.localRotation;
            originalFather = transform.parent.gameObject;
            life = stats.life;
            stats.Target = GameObject.Find("Player");
            rbd = GetComponent<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();
            StartCoroutine(MoveEnemy());
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void OnTriggerEnter(Collider other)
        {
            if (detectionZone.GetComponent<PlayerEnterZone>().playerInside)
            {
                if (other.tag == "Player")
                {
                    other.GetComponent<RPGCharacterController>().StartAction("Knockback", new HitContext(1, Vector3.back));
                    Children.GetComponent<BoxCollider>().enabled = false;
                    damage = true;
                    move = false;
                }
                if (other.tag == "HitBox")
                {
                    transform.parent = null;
                    agent.enabled = false;
                    rbd.isKinematic = false;
                    Children.GetComponent<BoxCollider>().enabled = false;
                    move = false;
                    life -= stats.Target.GetComponent<PlayerStats>().dmg;
                    Vector3 knokback = (other.transform.position - this.transform.position).normalized * 800;
                    other.enabled = false;
                    GetComponent<Rigidbody>().AddForce(knokback);
                    StartCoroutine(KnokBack());
                    if (life <= 0)
                    {
                        stats.Target.GetComponent<PlayerStats>().seconds += 30;
                        Destroy(this.gameObject);
                    }
                }
            }
        }
        IEnumerator MoveEnemy()
        {
            if (detectionZone.GetComponent<PlayerEnterZone>().playerInside)
            {
                detected = true;
            }
            if (detected && agent.enabled)
            {
                if (move)
                {
                    agent.SetDestination(stats.Target.transform.position);
                }
                else if (damage)
                {
                    stats.Target.GetComponent<PlayerStats>().seconds -= 10;
                    agent.enabled = false;
                    yield return new WaitForSeconds(1.5f);
                    Children.GetComponent<BoxCollider>().enabled = true;
                    agent.enabled = true;
                    move = true;
                    damage = false;
                }
            }
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(MoveEnemy());
        }

        IEnumerator KnokBack()
        {
            yield return new WaitForSeconds(1f);
            transform.parent = originalFather.transform;
            transform.localScale = originalScale;
            transform.localRotation = originalRotation;
            //move = true;
            rbd.isKinematic = true;
            agent.enabled = true;
            Children.GetComponent<BoxCollider>().enabled = true;
            move = true;
        }
    }
}
