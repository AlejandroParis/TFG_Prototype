using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    PlayerStats player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (gameObject.name == "Sword")
            {
                player.seconds -= 300;
                player.ActiveSword();
                Destroy(this.gameObject);
            }
            else if (gameObject.name == "Attack")
            {
                player.attackPowerUp += 1;
                player.seconds -= 20;
                player.RecalculateDmg();
                Destroy(this.gameObject);
            }
            else if (gameObject.name == "Defense")
            {
                player.defensePowerUp += 1;
                player.seconds -= 20;
                Destroy(this.gameObject);
            }
        }
    }
}
