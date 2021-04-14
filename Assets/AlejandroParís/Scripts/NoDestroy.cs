using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find(this.gameObject.name) != this.gameObject)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
