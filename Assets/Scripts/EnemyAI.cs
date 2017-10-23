using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public GameObject player;
    public bool patrol = true, guard = false, clockwise = false;
    public bool moving = true;
    public bool pursuingPlayer = false, goingToLasLoc = false;
    Vector3 target;
    Rigidbody2D rid;
    public Vector3 playerLastPos;
    RaycastHit2D hit;
    float speed = 2.0f;
    int layerMask = 1<<8;

    FindObjects findObj;
    GameObject[] weapons;
    EnemyWeponContoller ewc;
    public GameObject weaponGo;
    public bool goingToWeapon = false;
    public bool hasGun = false;
    

    // Use this for initialization

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLastPos = this.transform.position;
        findObj = GameObject.FindGameObjectWithTag("Player").GetComponent<FindObjects>();
        ewc = this.GetComponent<EnemyWeponContoller>();
        rid = this.GetComponent<Rigidbody2D>();
        layerMask = ~layerMask;
	}
	
	// Update is called once per frame
	void Update () {
        movement();
        playerDetect();
        canEnemyFindWeapon();
	}

    void movement()
    {
        float dist = Vector3.Distance(player.transform.position, this.transform.position);
        Vector3 dir = player.transform.position - transform.position;
        hit = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(dir.x, dir.y), dist, layerMask);
        Debug.DrawRay(transform.position, dir, Color.red);

        Vector3 fwt = this.transform.TransformDirection(Vector3.right);

        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2 (this.transform.position.x, this.transform.position.y), new Vector2(fwt.x, fwt.y), 0.5f, layerMask);

        Debug.DrawRay(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(fwt.x, fwt.y), Color.green);

        if(moving == true)  
        {
            if(hasGun == false)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }else
            {
                if(Vector3.Distance (this.transform.position, player.transform.position) < 5f && pursuingPlayer == true)
                {

                }
                else
                {
                    transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
            }
            
        }


        if(patrol == true)
        {
            Debug.Log("Patrolling normally");
            speed = 0.5f;
            if(hit2.collider != null)
            {
                if (hit.collider.gameObject.tag == "Wall")
                {
                    //Quaternion rot = this.transform.rotation;

                    if (clockwise == false)
                    {
                        transform.Rotate(0, 0, 90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, -90);
                    }

                }
            }

            if(weaponGo != null)
            {
                patrol = false;
                goingToWeapon = true;
            }
        }

        if(pursuingPlayer == true)
        {
            Debug.Log("Pursuing player");
            speed = 1f;
            rid.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((playerLastPos.y - transform.position.y), (playerLastPos.x - transform.position.x)) * Mathf.Rad2Deg);
            
            if (hit.collider.gameObject.tag == "Player")
            {
                playerLastPos = player.transform.position;
            }
        }
        if(goingToLasLoc == true)
        {
            Debug.Log("Checking last loc");
            speed = 0.7f;
            rid.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((playerLastPos.y - transform.position.y), (playerLastPos.x - transform.position.x)) * Mathf.Rad2Deg);
            if (Vector3.Distance(this.transform.position, playerLastPos) < 1.5f)
            {
                patrol = true;
                goingToLasLoc = false;

            }
        }

        if(goingToWeapon == true)
        {
            speed = 0.5f;
            rid.transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2((weaponGo.transform.position.y - transform.position.y), (weaponGo.transform.position.x - transform.position.x)) * Mathf.Rad2Deg);

            if(ewc.getCur() != null)
            {
                weaponGo = null;
                patrol = true;
                goingToWeapon = false;
                pursuingPlayer = false;
                goingToLasLoc = false;

            }
            if(/*weaponGo.activeInHierarchy == false ||*/ weaponGo == null)
            {
                weaponGo = null;
                patrol = true;
                goingToWeapon = false;
                pursuingPlayer = false;
                goingToLasLoc = false;
            }
        }
    }
    public void playerDetect()
    {
        Vector3 pos = this.transform.InverseTransformPoint(player.transform.position);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Player" /*&& pos.x > 1.2f*/ && Vector3.Distance(this.transform.position, player.transform.position) < 9)
            {
                patrol = false;
                pursuingPlayer = true;
                goingToWeapon = false;
                
            }
            else
            {
                if (pursuingPlayer == true)
                {
                    goingToLasLoc = true;
                    pursuingPlayer = false;
                    goingToWeapon = false;
                }
            }
        }
    }

    void setWeaponToGoTo(GameObject weapon)
    {
        weaponGo = weapon;
        goingToWeapon = true;
        patrol = false;
        pursuingPlayer = false;
        goingToLasLoc = false;
    }

    void canEnemyFindWeapon()
    {
        if(ewc.getCur() == null && weaponGo == null && goingToWeapon == false)
        {
            weapons = findObj.getWeapon();
            for(int x = 0; x < weapons.Length; x++)
            {
                float distance = Vector3.Distance(this.transform.position, weapons[x].transform.position);
                if (distance < 10)
                {
                    Vector3 direction = weapons[x].transform.position - transform.position;
                    RaycastHit2D weapCheck = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(direction.x, direction.y), distance, layerMask);
                    if(weapCheck.collider.gameObject.tag == "Weapon")
                    {
                        setWeaponToGoTo(weapons[x]);
                    }
                }
            }
        }
    }
    public float getSpeed()
    {
        return speed;
    }
}
