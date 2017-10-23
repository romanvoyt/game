using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{

    public string name;
    public float fireRate;
    WeaponAttack wa;
    public bool gun;
    public bool oneHanded;
    public bool shotgun;
    public int ammo;

    // Use this for initialization
    void Start()
    {
        wa = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponAttack>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnTriggerStay2D(Collider2D coll)
    {
         Debug.Log("Collision");
        if (coll.gameObject.tag == "Player" && Input.GetMouseButtonDown(1))
        {
            Debug.Log("Player picke up:" + name);
            if (wa.getCur() != null)
            {
                wa.dropWeapon();
            }
            wa.setWeapon(this.gameObject, name, fireRate, gun, oneHanded, shotgun);

            this.gameObject.SetActive(false);
        }
        else if (coll.gameObject.tag == "Enemy" && coll.gameObject.GetComponent<EnemyWeponContoller>().getCur() == null)
        {
            Debug.Log("Enemy picked up " + name);
            EnemyWeponContoller ewc = coll.gameObject.GetComponent<EnemyWeponContoller>();
            ewc.setWeapon(this.gameObject, name, fireRate, gun, oneHanded);
            this.gameObject.SetActive(false);
        }
    }

    }

