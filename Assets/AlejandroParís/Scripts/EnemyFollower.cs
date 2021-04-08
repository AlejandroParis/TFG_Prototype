using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollower : MonoBehaviour
{
    public EnemyStats stats;
    public GameObject detectionZone;
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

    IEnumerator MoveEnemy()
    {
        while (true)
        {
            agent.SetDestination(stats.Target.transform.position);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
