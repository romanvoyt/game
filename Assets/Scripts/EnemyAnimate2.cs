using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimate2 : MonoBehaviour {

    Sprite[] walking;
    Sprite[] attacking;
    Sprite[] legsSpr;
    
    int counter = 0;
    int legCount = 0;

    public bool attackingB = false;
    EnemyAI eAI;
    EnemyWeponContoller ewc;

    float timer = 0.1f;
    float legTimer = 0.1f;

    public SpriteRenderer torso;
    public SpriteRenderer legs;

    spriteContainers sc;

    // Use this for initialization
    void Start() {
        eAI = this.GetComponent<EnemyAI>();
        sc = GameObject.FindGameObjectWithTag("Player").GetComponent<spriteContainers>();
        walking = sc.enemyDoubleBwalk;
        attacking = sc.getEnemyWeapon(name);
        ewc = this.GetComponent<EnemyWeponContoller>();
        legsSpr = sc.getPlayerLegs();
    }

    // Update is called once per frame
    void Update() {

        AnimateLegs();
        if (eAI.moving == true)
        {
            animateWalk();
        }
        else
        {
            animateAttack();
        }

        if(eAI.pursuingPlayer == true)
        {
            animateAttack();
        }
        valueCheck();

    }

    void animateWalk()
    {
        if (eAI.moving == true)
        {
            torso.sprite = walking[counter];
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                if (counter < walking.Length - 1)
                {
                    counter++;
                }
                else
                {
                    counter = 0;
                }
                timer = 0.1f;
            }
        }
    }
    void AnimateLegs()
    {
        if (eAI.moving == true)
        {
            legs.sprite = legsSpr[legCount];
            legTimer -= Time.deltaTime;

            if (legTimer <= 0)
            {
                if (legCount < legsSpr.Length - 1)
                {
                    legCount++;
                }
                else
                {
                    legCount = 0;
                }
                legTimer = 0.1f;
            }
        }
        else
        {
            if (legCount < 6)
            {
                legs.sprite = legsSpr[0];
            }
            else
            {
                legs.sprite = legsSpr[5];
            }
        }
    }

    void animateAttack()
    {
        if (eAI.hasGun == true)
        {
            torso.sprite = attacking[counter];
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                if (counter < attacking.Length - 1)
                {
                    counter++;
                }
                else
                {
                    if (attackingB)
                    {
                        attackingB = false;
                    }
                    counter = 0;
                }
                timer = 0.05f;
            }
        }
    }

    void attackCheck()
    {
        if (!attackingB)
        {
            torso.sprite = walking[0];
        }
    }
    void valueCheck()
    {
        if (this.gameObject.tag == "Dead")
        {
            legs.enabled = false;
        }
        if (eAI.enabled == false)
        {
            legs.enabled = false;
        }
        else if(eAI.pursuingPlayer == true){
            legs.enabled = false;
        }
        else
        {
            legs.enabled = true;
        }

    }
    public void disableLegs()
    {
        legsSpr = null;
        legs.enabled = false;
    }
    public void enableLegs()
    {
        legs.enabled = true;
    }
} 
