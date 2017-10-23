using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour {

    public GameObject bullet;
    public GameObject shotgunBullet;
    GameObject curWeapon;
    public GameObject oneHandSpawn;
    public GameObject twoHandSpawn;
    bool oneHanded = false;
    bool gun = false;
    float timer = 0.1f, timerReset = 0.1f;
    PlayerAnimate pa;
    spriteContainers sc;
    float weaponChange = 0.5f;
    public bool changingWeapon = false;
    Rigidbody2D rid;


    float originalWdth = 1920.0f;
    float originalHeight = 1080.0f;
    Vector3 scale;
    public GUIStyle text;
    public Texture2D bg;
    public WeaponPickUp curWepScr;

    public bool shotgun = false;
    // Use this for initialization
    void Start () {
        pa = GetComponent<PlayerAnimate>();
        sc = GameObject.FindGameObjectWithTag("Player").GetComponent<spriteContainers>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            attack();
        }
        if (Input.GetMouseButtonDown(0))
        {
            pa.resetCounter();

        }
        if (Input.GetMouseButtonUp(0))
        {
            pa.resetCounter();
        }
        
        if(Input.GetKeyDown(KeyCode.Q) && changingWeapon == false)
        {
            dropWeapon();
        }

        if (changingWeapon == true)
        {
            weaponChange -= Time.deltaTime;
            if (weaponChange <= 0)
            {
                changingWeapon = false;
            }
        }
    }

    public void setWeapon(GameObject cur, string name, float FireRate, bool gun, bool oneHanded, bool shotgun)
    {
        changingWeapon = true;
        curWeapon = cur;
        pa.SetNewTorso(sc.getWeaponWalk(name), sc.getWeapon(name));
        this.gun = gun;
        timerReset = FireRate;
        timer = timerReset;
        this.oneHanded = oneHanded;
        this.shotgun = shotgun;
        curWepScr = curWeapon.GetComponent<WeaponPickUp>();
    }

    public void attack()
    {

        pa.Attack();
        if (gun==true && curWepScr.ammo > 0)
        {
            pa.Attack();
            Bullet bul = bullet.GetComponent<Bullet>();
            Bullet shotGunBul = bullet.GetComponent<Bullet>();
            Vector3 direction;
            direction.x = Vector2.right.x;
            direction.y = Vector2.right.y;
            direction.z = 0;

            bul.SetValues(direction, "Player");

            if (oneHanded)
            {
                Instantiate(bullet, oneHandSpawn.transform.position, this.transform.rotation);
                curWeapon.GetComponent<WeaponPickUp>().ammo--;
            }
            else
            {
                Instantiate(shotgunBullet, twoHandSpawn.transform.position, this.transform.rotation);
                curWeapon.GetComponent<WeaponPickUp>().ammo--;
            }
            timer = timerReset;
        }
        else
        {
            int layerMask = 1 << 9;
            layerMask = ~layerMask;
            pa.Attack();
            RaycastHit2D rayCast = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(transform.right.x, transform.right.y), layerMask);

            Debug.DrawRay(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(transform.right.x, transform.right.y), Color.green);

            
            if (curWeapon == null && rayCast.collider.gameObject.tag == "Enemy")
            {
                Debug.Log("hit");
                EnemyAttacked enemyAttacked = rayCast.collider.gameObject.GetComponent<EnemyAttacked>();
                enemyAttacked.KnockDownEnemy();
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

        if (curWeapon == null)
        {

        }
        else
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            curWeapon.AddComponent<ThrowWeapon>();
            Vector3 direction;
            direction.x = mousePos.x - this.transform.position.x;
            direction.y = mousePos.y - this.transform.position.y;
            direction.z = 0;
            
            curWeapon.GetComponent<Rigidbody2D>().isKinematic = false;
            curWeapon.GetComponent<ThrowWeapon>().SetDirection(direction);
            curWeapon.transform.position = oneHandSpawn.transform.position;
            curWeapon.transform.eulerAngles = this.transform.eulerAngles;

            curWeapon.SetActive(true);
            setWeapon(null, "", 0.5f, false, false, false);
            pa.resetSprites();
        }
    }

    void OnGUI()
    {
        GUI.depth = 0;
        scale.x = Screen.width / originalWdth;
        scale.y = Screen.height / originalHeight;
        scale.z = 1;

        var svMat = GUI.matrix;
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);

        if (gun == true)
        {
            Rect posForAmmo = new Rect(originalWdth - 535, originalHeight -200, 300, 150);
            GUI.DrawTexture(posForAmmo, bg);
            GUI.Box(posForAmmo, "Bullets:" + curWeapon.GetComponent<WeaponPickUp>().ammo, text);
        }
        GUI.matrix = svMat;
    }
}
