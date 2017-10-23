using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeponContoller : MonoBehaviour {

    public GameObject oneHandSpawn, twoHandSpawn, bullet;
    GameObject curWeapon;
    public bool gun = false;
    float timer = 0.1f, timerReset = 0.1f;

    spriteContainers sc;

    float weaponChange = 0.5f;
    bool changingWeapon = false;
    bool oneHanded = false;

    EnemyAI eA;
    GameObject player;
    EnemyAnimate ea;

    SpriteRenderer sr;
	// Use this for initialization
	void Start () {
        eA = this.GetComponent<EnemyAI>();
        player = GameObject.FindGameObjectWithTag("Player");
        sr = this.GetComponent<SpriteRenderer>();
        sc = GameObject.FindGameObjectWithTag("Player").GetComponent<spriteContainers>();
	}
	
	// Update is called once per frame
	void Update () {

        if(gun == true)
        {
            eA.hasGun = true;

        }
        else
        {        
            eA.hasGun = false;
        }

        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (eA.hasGun == false && gun == true && eA.pursuingPlayer == false && timer <= 0 && Vector3.Distance(this.transform.position, player.transform.position) <= 1.6f)
        {
            attack();
            ea.setAttacking();
        }else if(eA.hasGun == true && eA.pursuingPlayer == true && timer <= 0 && Vector3.Distance(this.transform.position, player.transform.position) <= 5.0f){
            attack();
            ea.setAttacking();
        }
        if(changingWeapon == true)
        {
            weaponChange -= Time.deltaTime;
            if(weaponChange <= 0)
            {
                changingWeapon = false;
            }
        }
	}

    public void setWeapon(GameObject cur, string name, float FireRate, bool gun, bool oneHanded)
    {
        changingWeapon = true;
        curWeapon = cur;
        this.gun = gun;
        timerReset = FireRate;
        timer = timerReset;
        this.oneHanded = oneHanded;
    }

    public void attack()
    {

        //pa.Attack();
        if (gun == true)
        {
            //pa.Attack();
            Bullet bul = bullet.GetComponent<Bullet>();
            Vector3 direction;
            direction.x = Vector2.right.x;
            direction.y = Vector2.right.y;
            direction.z = 0;

            bul.SetValues(direction, "Enemy");

            if (oneHanded == true)
            {
                Instantiate(bullet, oneHandSpawn.transform.position, this.transform.rotation);
            }
            else
            {
                Instantiate(bullet, twoHandSpawn.transform.position, this.transform.rotation);
            }
            timer = timerReset;
        }
        else
        {
            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            //pa.Attack();
            RaycastHit2D rayCast = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(transform.right.x, transform.right.y), 1.5f, layerMask);

            Debug.DrawRay(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(transform.right.x, transform.right.y), Color.green);

            Debug.Log("attempting melee attack");

            if (curWeapon == null && rayCast.collider.gameObject.tag == "Player")
            {
                Debug.Log("melee attack");
                PlayerHealth.dead = true;
                // EnemyAttacked enemyAttacked = rayCast.collider.gameObject.GetComponent<EnemyAttacked>();
                //  enemyAttacked.KnockDownEnemy();
            }
            timer = timerReset;
        }
    }
        public GameObject getCur()
        {
            return curWeapon;
        }

        public void dropWeapon()
        {
            if (curWeapon == null) {
            }   
            else
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                //curWeapon.AddComponent<ThrowWeapon>();
                Vector3 direction;
                direction.x = mousePos.x - this.transform.position.x;
                direction.y = mousePos.y - this.transform.position.y;
                direction.z = 0;

                curWeapon.GetComponent<Rigidbody2D>().isKinematic = false;
                //curWeapon.GetComponent<ThrowWeapon>().SetDirection(direction);
                curWeapon.transform.position = oneHandSpawn.transform.position;
                curWeapon.transform.eulerAngles = this.transform.eulerAngles;

                curWeapon.SetActive(true);
                setWeapon(null, "", 0.5f, false, false);
               // pa.resetSprites();
            }
        }
}
