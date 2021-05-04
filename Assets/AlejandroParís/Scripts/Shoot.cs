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
                int dmg = (int)(10 + ((target.GetComponent<PlayerStats>().floor - (target.GetComponent<PlayerStats>().defensePowerUp / 2)) * 2));
                if(dmg <= 0)
                    target.GetComponent<PlayerStats>().seconds -= 0;
                else
                    target.GetComponent<PlayerStats>().seconds -= dmg;
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
