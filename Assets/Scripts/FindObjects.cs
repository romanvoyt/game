using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindObjects : MonoBehaviour {
    public GameObject[] weapons;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        weapons = GameObject.FindGameObjectsWithTag ("Weapon");
        check();

    }
    public GameObject[] getWeapon()
    {
        return weapons;
    }

    void check()
    {
        if(weapons.Length == 0)
        {
            Debug.Log("0 weapons on map");
        }
    }
    
}
