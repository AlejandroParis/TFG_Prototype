using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextFloor : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            SceneManager.LoadScene("TestProceduralMap");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerStats>().floor += 1;
            player.transform.position = new Vector3(0, 0, 0);
            SceneManager.LoadScene("TestProceduralMap");
        }
    }
}
