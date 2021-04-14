using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Map;
    // Start is called before the first frame update
    void Start()
    {
        Map = GameObject.Find("MapGeneration");
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.transform.position = new Vector3(0, 0, 0);
     }

    // Update is called once per frame
    void Update()
    {
        
    }
}
