using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsDirection : MonoBehaviour {

    Vector3 rot;

	// Use this for initialization
	void Start () {
        rot = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            rot = new Vector3(0, 0, 90);
            transform.eulerAngles = rot;
        }

        if (Input.GetKey(KeyCode.S))
        {
            rot = new Vector3(0, 0, 270);
            transform.eulerAngles = rot;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rot = new Vector3(0, 0, 0);
            transform.eulerAngles = rot;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rot = new Vector3(0, 0, 180);
            transform.eulerAngles = rot;
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            rot = new Vector3(0, 0, 45);
            transform.eulerAngles = rot;
        }

        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
        {
            rot = new Vector3(0, 0, 315);
            transform.eulerAngles = rot;
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            rot = new Vector3(0, 0, 235);
            transform.eulerAngles = rot;
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            rot = new Vector3(0, 0, 135);
            transform.eulerAngles = rot;
        }
    }
}
