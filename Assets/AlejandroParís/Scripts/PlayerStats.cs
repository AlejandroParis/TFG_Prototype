using RPGCharacterAnimsFREE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
    {
        public float seconds = 100;
        public float minutes;
        public Text life;
        public Text Floor;
        public int dmg;
        public int floor = 0;
        public GameObject CMCamera;
        //public bool sword = true;
        public int attackPowerUp = 0;
        public int defensePowerUp = 0;
        public GameObject map;

        // Start is called before the first frame update

        void Awake()
        {
            if (GameObject.FindGameObjectWithTag("Player") != this.gameObject)
            {
                Destroy(this.gameObject);
            }
            DontDestroyOnLoad(this.gameObject);
        }

        void Start()
        {
            //CMCamera = GameObject.Find("CM FreeLook");
            //CMCamera.GetComponent<CinemachineVirtualCamera>().LookAt = this.gameObject.transform;
            //CMCamera.GetComponent<CinemachineVirtualCamera>().Follow = this.gameObject.transform;
            GameObject canvas = GameObject.Find("Canvas");
            life = canvas.transform.Find("Life").GetComponent<Text>();
            Floor = canvas.transform.Find("Floor").GetComponent<Text>();
            map = GameObject.Find("Map");
            this.transform.position = map.transform.position;
            //GetComponent<RPGCharacterInputController>().SwitchWeapons();
        }

        // Update is called once per frame
        void Update()
        {
            if(map == null)
            {
                map = GameObject.Find("Map");
            }
            if(map.transform.childCount <= 0)
            {
                SceneManager.LoadScene("TestProceduralMap");
            }
            if (this.gameObject.transform.position.y < -0.1f)
            {
                this.transform.position = map.transform.position;
            }
            seconds -= Time.deltaTime;
            minutes = (int)(seconds / 60);
            life.text = minutes.ToString() + " : " + ((int)(seconds - (minutes * 60))).ToString();
            Floor.text = "Floor: " + floor.ToString();

            if (seconds <= 0 && minutes <= 0)
            {
                Destroy(this.gameObject);
            }

        }

        public void RecalculateDmg()
        {
            dmg = dmg + attackPowerUp + ((3 + attackPowerUp)/2);
        }
        
        public void ActiveSword()
        {
            GetComponent<RPGCharacterInputController>().SwitchWeapons();
        }
    }