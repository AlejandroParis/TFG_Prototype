using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGCharacterAnimsFREE.Actions;

namespace RPGCharacterAnimsFREE
{
    public class Shoot : MonoBehaviour
    {
        public GameObject target;
        // Start is called before the first frame update
        void Start()
        {
            target = GameObject.Find("Player");
            StartCoroutine(DestroyBullet());
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<RPGCharacterController>().StartAction("GetHit", new HitContext(1, Vector3.back));
                target.GetComponent<PlayerStats>().seconds -= 10;
                Destroy(this.gameObject);
            }
        }
        IEnumerator DestroyBullet()
        {
            yield return new WaitForSeconds(15f);
            Destroy(this.gameObject);
        }
    }
}
