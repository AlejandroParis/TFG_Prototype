using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterZone : MonoBehaviour
{
    public bool playerInside = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            playerInside = true;
        }
    }
}
