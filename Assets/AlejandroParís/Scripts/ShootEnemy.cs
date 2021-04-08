using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPGCharacterAnimsFREE.Actions;

public class ShootEnemy : MonoBehaviour
{
    public EnemyStats stats;
    public GameObject detectionZone;
    public GameObject bullet;
    public bool move = true;
    public bool shoot = true;
    public float range;
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
        if(stats.Target.transform.position.magnitude - this.transform.position.magnitude <= range && shoot)
        {
            move = false;
            GameObject clone = Instantiate(bullet, transform.position, Quaternion.identity);
            Vector3 targetshoot = new Vector3(stats.Target.transform.position.x, stats.Target.transform.position.y + 2f, stats.Target.transform.position.z);
            clone.GetComponent<Rigidbody>().AddForce((targetshoot - clone.transform.position).normalized * 800);
            shoot = false;
        }
    }
    IEnumerator MoveEnemy()
    {
        if (move)
        {
            agent.SetDestination(stats.Target.transform.position);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(MoveEnemy());
        }
        else
        {
            yield return new WaitForSeconds(2);
            move = true;
            shoot = true;
            StartCoroutine(MoveEnemy());
        }
    }
}
