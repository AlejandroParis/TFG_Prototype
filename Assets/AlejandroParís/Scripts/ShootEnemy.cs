using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPGCharacterAnimsFREE.Actions;

public class ShootEnemy : MonoBehaviour
{
    public EnemyStats stats;
    public GameObject detectionZone;
    public int life;
    public GameObject bullet;
    public bool move = true;
    Rigidbody rbd;
    public bool shoot = true;
    public bool knock = false;
    public float range;
    NavMeshAgent agent;
    public GameObject originalFather;
    Vector3 originalScale;
    Quaternion originalRotation;

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
        if (detectionZone.GetComponent<PlayerEnterZone>().playerInside)
        {
            float distance = Vector3.Distance(stats.Target.transform.position, this.transform.position);
            if (distance <= range && shoot && !knock)
            {
                move = false;
                GameObject clone = Instantiate(bullet, transform.position, Quaternion.identity);
                Vector3 targetshoot = new Vector3(stats.Target.transform.position.x, stats.Target.transform.position.y + 2f, stats.Target.transform.position.z);
                clone.GetComponent<Rigidbody>().AddForce((targetshoot - clone.transform.position).normalized * 800);
                shoot = false;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HitBox")
        {
            transform.parent = null;
            agent.enabled = false;
            rbd.isKinematic = false;
            shoot = false;
            //move = false;
            life -= stats.Target.GetComponent<PlayerStats>().dmg;
            Vector3 knokback = (other.transform.position - this.transform.position).normalized * 800;
            other.enabled = false;
            GetComponent<Rigidbody>().AddForce(knokback);
            StartCoroutine(KnokBack());
            if (life <= 0)
            {
                stats.Target.GetComponent<PlayerStats>().seconds += 20;
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator MoveEnemy()
    {
        if (agent.enabled && detectionZone.GetComponent<PlayerEnterZone>().playerInside)
        {
            if (move)
            {
                agent.SetDestination(stats.Target.transform.position);
                //yield return new WaitForSeconds(0.5f);
            }
            else
            {
                yield return new WaitForSeconds(1.5f);
                move = true;
                shoot = true;
            }
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MoveEnemy());
    }

    IEnumerator KnokBack()
    {
        knock = true;
        yield return new WaitForSeconds(1.3f);
        knock = false;
        transform.parent = originalFather.transform;
        transform.localScale = originalScale;
        transform.localRotation = originalRotation;
        //shoot = true;
        rbd.isKinematic = true;
        agent.enabled = true;
        //move = true;
    }
}
