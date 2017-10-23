using UnityEngine;
using System.Collections;

public class EnemyAttacked : MonoBehaviour
{

    public Sprite knockedDown;
    public Sprite stabbed;
    public Sprite bulletWound;
    public Sprite backUp;

    SpriteRenderer spriteRenderer;

    bool enemyKnockedDown = false;

    float knockDownTimer = 3.0f;

    GameObject player;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyKnockedDown == true)
        {
            KnockDown();
        }
    }

    public void KnockDownEnemy()
    {
        enemyKnockedDown = true;
    }

    public void KnockDown()
    {
        this.GetComponent<EnemyWeponContoller>().dropWeapon();
        this.GetComponent<EnemyWeponContoller>().enabled = false;
        knockDownTimer -= Time.deltaTime;   
        spriteRenderer.sprite = knockedDown;
        this.GetComponent<CircleCollider2D>().enabled = false;
        this.GetComponent<EnemyAnimate2>().disableLegs();
        this.GetComponent<EnemyAnimate2>().enabled = false;
        spriteRenderer.sortingOrder = 2;
        this.GetComponent<EnemyAI>().enabled = false;

        if (knockDownTimer <= 0)
        {
            enemyKnockedDown = false;
            spriteRenderer.sprite = backUp;
            this.GetComponent<CircleCollider2D>().enabled = true;
            this.GetComponent<EnemyAI>().enabled = true;
            this.GetComponent<EnemyWeponContoller>().enabled = true;
            this.GetComponent<EnemyAnimate2>().enableLegs();
            this.GetComponent<EnemyAnimate2>().enabled = true;
            spriteRenderer.sortingOrder = 5;
            knockDownTimer = 3.0f;
        }
    }

    public void KillBullet()
    {
        this.GetComponent<EnemyWeponContoller>().dropWeapon();
        this.GetComponent<EnemyWeponContoller>().enabled = false;
        spriteRenderer.sprite = bulletWound;
        spriteRenderer.sortingOrder = 2;
        this.GetComponent<CircleCollider2D>().enabled = false;
        this.GetComponent<EnemyAI>().enabled = false;
        this.GetComponent<EnemyAnimate2>().disableLegs();
        this.GetComponent<EnemyAnimate2>().enabled = false;
        this.gameObject.tag = "Dead";
    }

    public void KillMelee()
    {
        spriteRenderer.sprite = stabbed;
        spriteRenderer.sortingOrder = 2;
        this.GetComponent<CircleCollider2D>().enabled = false;
        this.GetComponent<EnemyAI>().enabled = false;
        this.gameObject.tag = "Dead";
    }
}