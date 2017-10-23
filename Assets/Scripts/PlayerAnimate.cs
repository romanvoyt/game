using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimate : MonoBehaviour {

    Sprite[] walking;
    Sprite[] attacking;
    Sprite[] legsSpr;

    int counter = 0;
    int legCount = 0;

    bool attackingB = false;

    playermovement pm;

    float timer = 0.1f;
    float legTimer = 0.1f;

    public SpriteRenderer torso;
    public SpriteRenderer legs;

    spriteContainers spriteContainer;

    // Use this for initialization
    void Start()
    {
        pm = this.GetComponent<playermovement>();
        spriteContainer = GameObject.FindGameObjectWithTag("Player").GetComponent<spriteContainers>();
        walking = spriteContainer.getPlayerUnarmedWalk();
        legsSpr = spriteContainer.getPlayerLegs();
        attacking = spriteContainer.getPlayerPunch();

    }

    // Update is called once per frame
    void Update()
    {
        AnimateLegs();
        if (!attackingB)
        {
            animateTorso();
        }
        else
        {
            animateAttack();
        }
    }

    void AnimateLegs()
    {
        if (pm.moving == true)
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
            if (legCount < 9)
            {
                legs.sprite = legsSpr[0];
            }
            else
            {
                legs.sprite = legsSpr[9];
            }
        }
    }

    void animateTorso()
    {
        if (pm.moving == true)
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

    void animateAttack()
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

    public void SetNewTorso(Sprite[] walk, Sprite[] attack)
    {
        counter = 0;
        attacking = attack;
        walking = walk;
        torso.sprite = walking[0];
    }

    public bool GetAttack()
    {
        return attackingB;
    }

    public void Attack()
    {
        attackingB = true;
    }

    void attackCheck()
    {
        if (!attackingB)
        {
            torso.sprite = walking[0];
        }
    }

    public void resetSprites()
    {
        counter = 0;
        walking = spriteContainer.getPlayerUnarmedWalk();
        attacking = spriteContainer.getPlayerPunch();
        torso.sprite = walking[0];
    }

    public void resetCounter()
    {
        counter = 0;
        attackCheck();
    }
}
