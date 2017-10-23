using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimate : MonoBehaviour
{

    public SpriteRenderer torso, legs;
    EnemyAI eAI;
    spriteContainers sc;

    Sprite[] torsoSpr;
    Sprite[] attackingSpr;
    Sprite[] legsSpr;

    float torsoTimer = 0.15f, legsTimer = 0.15f, legReset = 0.15f, torsoReset = 0.15f;
    int tCounter = 0, lCounter = 0;
    string weapon;
    EnemyWeponContoller ewc;
    public bool attacking = false;
    // Use this for initialization
    void Start()
    {
        eAI = this.GetComponent<EnemyAI>();
        sc = GameObject.FindGameObjectWithTag("Player").GetComponent<spriteContainers>();
        torsoSpr = sc.getEnemyWalk(name);
        attackingSpr = sc.getEnemyWeapon(name);
        ewc = this.GetComponent<EnemyWeponContoller>();
        legsSpr = sc.getPlayerLegs();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (eAI.moving == true)
        {
            animateLegs();
        }
        if (attacking == true)
        {
            animateAttack();
        }
        if(eAI.moving == true)
        {
            animateWalk();
        }
        legResetSpeed();
        valueCheck();
    }

    void animateWalk()
    {

        /*if (tCounter > torsoSpr.Length - 1)
        {
            tCounter = 0;
        }*/
       /* Debug.Log("Walking" + tCounter);*/
        torsoTimer = Time.deltaTime;
        if (torsoSpr.Length > 1)
        {
            torso.sprite = torsoSpr[tCounter];
        }
        else
        {
            torso.sprite = torsoSpr[0];
        }
        if (torsoTimer <= 0)
        {
            if (tCounter < torsoSpr.Length - 1)
            {
                tCounter++;
            }
            else
            {
                tCounter = 0;
            }
            torsoTimer = torsoReset;
        }
        

    }
    void animateAttack()
    {
        if (tCounter > attackingSpr.Length - 1)
        {
            tCounter = 0;
        }
        Debug.Log("Attacking" + tCounter);
        torsoTimer = Time.deltaTime;
        torso.sprite = attackingSpr[tCounter];
        if (torsoTimer <= 0)
        {
            if (tCounter < attackingSpr.Length - 1)
            {
                tCounter++;
            }
            else
            {
                attacking = false;
                tCounter = 0;
            }
            torsoTimer = torsoReset;
        }
    }
    void animateTorso()
    {

        /*torso.sprite = torsoSpr[tCounter];
        torsoTimer = Time.deltaTime;*/
        if (attacking == false)
        {
            /*if (torsoSpr.Length > 1)
            {
                torso.sprite = torsoSpr[tCounter];
            }
            else
            {
                torso.sprite = torsoSpr[0];
            }*/
            torso.sprite = torsoSpr[tCounter];
            torsoTimer = Time.deltaTime;

            if (torsoTimer <= 0)
            {
                if (tCounter < torsoSpr.Length - 1)
                {
                    tCounter++;
                }
                else
                {
                    tCounter = 0;
                }
                torsoTimer = 0.1f;
            }
        }
        else
        {
            torso.sprite = attackingSpr[tCounter];
            torsoTimer = Time.deltaTime;
            if (torsoTimer <= 0)
            {
                if(tCounter < attackingSpr.Length - 1)
                {
                    tCounter++;
                }
                else
                {
                    attacking = false;
                    tCounter = 0;
                }
            }
            torsoTimer = 0.1f;
        }
        
    }
    void animateLegs()
    {
        legs.sprite = legsSpr[lCounter];
        legsTimer = Time.deltaTime;
        if(legsTimer <= 0)
        {
            if(lCounter < legsSpr.Length - 1)
            {
                lCounter++;
            }
            else
            {
               lCounter = 0;
            }
        }
        legsTimer = 0.1f;
    }
    void legResetSpeed()
    {
        if(eAI.getSpeed() > 2.1f)
        {
            legReset = 0.03f;
            torsoReset = 0.03f;
        }
        else
        {
            legReset = 0.05f;
            torsoReset = 0.05f;
        }
    }
    public void setAttacking()
    {
        attacking = true;
    }
    void valueCheck()
    {
        if(gameObject.tag == "Dead")
        {
            legs.enabled = false;
        }
        if(eAI.enabled == false)
        {
            legs.enabled = false;
        }
        else
        {
            legs.enabled = true;
        }

    }
    public void setTorsoSpr(string name)
    {
        torsoSpr = sc.getEnemyWalk(name);
        attackingSpr = sc.getEnemyWeapon(name);
        tCounter = 0;
    }

    void resetCounter()
    {
        tCounter = 0;
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
