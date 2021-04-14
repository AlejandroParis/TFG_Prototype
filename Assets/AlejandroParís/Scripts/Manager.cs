using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    GameObject Player;
    public GameObject Map;
    // Start is called before the first frame update
    void Start()
    {
        Map = GameObject.Find("Map");
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.transform.position = Map.transform.GetChild(0).transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
