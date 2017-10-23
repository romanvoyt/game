using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateEffects : MonoBehaviour {

    playermovement pm;
  
    float mod = 0.1f;
    float zVal = 0.0f;


	// Use this for initialization
	void Start () {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<playermovement>();
	}
	
	// Update is called once per frame
	void Update () {
        if (pm.moving == true)
        {

            Vector3 rotation = new Vector3(0, 0, zVal);
            this.transform.eulerAngles = rotation;

            zVal += mod;
            
            if (transform.eulerAngles.z >= 5.0f && transform.eulerAngles.z < 10.0f)
            {
                mod = -0.1f;
            }
            else if (transform.eulerAngles.z < 355.0f && transform.eulerAngles.z > 350.0f)
            {
                mod = 0.1f;
            }
        }

	}
}
