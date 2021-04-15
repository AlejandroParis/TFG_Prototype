using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTarget : MonoBehaviour
{
    public CinemachineFreeLook cm;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        cm = this.GetComponent<CinemachineFreeLook>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cm.LookAt == null || cm.Follow == null)
        {
            player = GameObject.Find("Player");
            cm.Follow = player.transform;
            cm.LookAt = player.transform;
        }
    }
}
