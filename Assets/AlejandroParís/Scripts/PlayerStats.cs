using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float seconds = 100;
    public float minutes;
    public Text life;
    public Text Floor;
    public int dmg;
    public int floor = 0;
    // Start is called before the first frame update

    void Awake()
    {
        if(GameObject.FindGameObjectWithTag("Player") != this.gameObject)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.transform.position.y < 0)
        {
            this.transform.position = new Vector3(0, 0, 0);
        }
        seconds -= Time.deltaTime;
        minutes = (int)(seconds / 60);
        life.text = minutes.ToString() + " : " +  ((int)(seconds - (minutes*60))).ToString();
        Floor.text = "Floor: " + floor.ToString();
    }
}
