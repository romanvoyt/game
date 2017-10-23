using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteContainers : MonoBehaviour
{

    public Sprite[] pLegs, pUnarmedWalk, doubleBarrelWalk, pPunch, doubleBarrelAttack, desertAttack, desertWalk, enemyDoubleBAttack, enemyDesertAttack, enemyUnarmedWalk, ePunch, enemyDoubleBwalk, enemyDesertWalk, eWalk;

    public Sprite enemydBarrel, enemyDesert, enemyUnarmed;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Sprite[] getPlayerLegs()
    {
        return pLegs;
    }

    public Sprite[] getPlayerUnarmedWalk()
    {
        return pUnarmedWalk;
    }

    public Sprite[] getPlayerPunch()
    {
        return pPunch;
    }

    public Sprite[] getWeapon(string weapon)
    {
        switch (weapon)
        {
            case "dBarrel":
                return doubleBarrelAttack;
            case "desert":
                return desertAttack;
            default:
                return getPlayerPunch();

        }
    }
    public Sprite[] getWeaponWalk(string weapon)
    {
        switch (weapon)
        {
            case "dBarrel":
                return doubleBarrelWalk;
            case "desert":
                return desertWalk;
            default:
                return getPlayerPunch();
        }

    }

    public Sprite[] getEnemyPunch()
    {
        return ePunch;
    }

    public Sprite[] getEnemyWeapon(string weapon)
    {
        switch (weapon)
        {
            case "dBarrel":
                return enemyDoubleBAttack;
            case "desert":
                return enemyDesertAttack;
            default:
                return getEnemyPunch();
        }
    }

    public Sprite[] getEnemyWalk(string weapon)
    {
        switch (weapon)
        {
            case "dBarrel":
                return enemyDoubleBwalk;
            case "desert":
                return enemyDesertWalk;
            default:
                return eWalk;
        }
    }

    public Sprite getEnemySprite(string weapon)
    {
        if (weapon == "dBarrel")
        {
            return enemydBarrel;
        }
        else if (weapon == "desert")
        {
            return enemyDesert;
        }
        else
        {
            return enemyUnarmed;
        }
    }
}
