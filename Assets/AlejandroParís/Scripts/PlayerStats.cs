using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float seconds = 100;
    public float minutes;
    public Text life;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        seconds -= Time.deltaTime;
        minutes = (int)(seconds / 60);
        life.text = minutes.ToString() + " : " +  ((int)(seconds - (minutes*60))).ToString();
    }
}
