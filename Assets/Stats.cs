using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public string Statname;
    public bool CONSTANT = true;

    [Header("Main")]
    public float damages;
    public float attackRate;
    public float range;

    [Header("Melee")]
    public float attackSpeed;

    [Header("Distance")]
    public float bulletSpeed;
    public float bulletSize;
    public int bulletPerShot;
    public float bulletSpread;


    public void AddStats(Stats StatsToAdd, bool replace = false){
        if(!CONSTANT){
            if(replace){ 
                damages = StatsToAdd.damages;
                attackRate = StatsToAdd.attackRate;
                range = StatsToAdd.range;
                //
                bulletSpeed = StatsToAdd.bulletSpeed;
                bulletSize = StatsToAdd.bulletSize;
                bulletPerShot = StatsToAdd.bulletPerShot;
                bulletSpread = StatsToAdd.bulletSpread;
                //
                attackSpeed = StatsToAdd.attackSpeed;
            }   
            else{
                damages = damages + StatsToAdd.damages;
                attackRate = attackRate + StatsToAdd.attackRate;
                range = range + StatsToAdd.range;
                //
                bulletSpeed = bulletSpeed + StatsToAdd.bulletSpeed;
                bulletSize = bulletSize + StatsToAdd.bulletSize;
                bulletPerShot = bulletPerShot + StatsToAdd.bulletPerShot;
                bulletSpread = bulletSpread + StatsToAdd.bulletSpread;
                //
                attackSpeed = attackSpeed + StatsToAdd.attackSpeed;
            }
        }
    }

    public void ResetStats(){
        if(!CONSTANT){
            damages = 0;
            attackRate = 1f;
            range = 0;
            //
            bulletSpeed = 0;
            bulletSize = 0;
            bulletPerShot = 0;
            bulletSpread = 0;
            //
            attackSpeed = 0;
        }
    }
}
