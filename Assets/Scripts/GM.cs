using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{

    //Player variables
    public int level                     = 1;
    public int xp                        = 0;
    public int xp2Next                   = 100;
    public int health                    = 100;
    public float regen                   = 1;
 
    // passive
    public float damageMultiplier        = 1;
    public float rotationSpeedMultiplier = 1;
    public float cooldownReduction       = 0;  // 15 = %15 cooldown reduction
    public float damageReduction         = 0;
    public float bonusProjectile         = 0;
    public float damageReduction         = 0;
    public float xpMultiplier            = 1;
 
 
 
    public int   misilleLevel            = 1;
    public int   misilleCount            = 1;
    public int   misilleBaseDamage       = 60;
    public float misilleDamage           = 60;
    public float misilleBaseCooldown     = 3;
 
    public int   satelliteLevel          = 1;
    public int   satelliteCount          = 1;
    public int   satelliteBaseSpeed      = 360;
    public float satelliteSpeed          = 360;
    public int   satelliteBaseDamage     = 100;
    public float satelliteDamage         = 100;

    public int   swordLevel              = 1;
    public int   swordSwingBaseAngle     = 360;
    public float swordSwingAngle         = 360;
    public int   swordSwingBaseSpeed     = 360;
    public float swordSwingSpeed         = 360;
    public int   swordBaseDamage         = 200;
    public float swordDamage             = 200;


    
    //Enemy variables
    public int skullLevel                = 1;
    public int skullBaseHealth           = 10;
    public int skullBaseDamage           = 10;


    void gainXp(float amount){
        xp += amount * xpMultiplier
        while(xp >= xp2Next){
            levelUp()
            xp -= xp2Next;
        }

    }
    void levelUp(){
        level ++
        //open levelUp ui


    }
    void upgradeDamageMultiplier(float amount){
        damageMultiplier += amount;
    }
    void upgradeRotationSpeedMultiplier(float amount){
        rotationSpeedMultiplier += amount;
    }
    void upgradeCooldownReduction(float amount){
        cooldownReduction += amount;
    }

    void upgradeWeapon(string name){
        if      (name == "misille"){
            upgradeMisille();
        }else if(name == "satellite"){
            upgradeSatellite();
        }else if(name == "sword"){
            upgradeSword();
        }
    }
    void upgradeMisille(){
        misilleLevel ++;
        
        misilleDamage = misilleBaseDamage * misilleLevel  * damageMultiplier;
        misilleCount  = misilleLevel + bonusProjectile;
    
    }
    void upgradeSatellite(){
        satelliteLevel ++;
        
        satelliteDamage = satelliteBaseDamage * satelliteLevel  * damageMultiplier;
        satelliteSpeed  = satelliteBaseSpeed  * satelliteLevel  * rotationSpeedMultiplier;
        satelliteCount  = satelliteLevel + bonusProjectile;
    
    }

    void upgradeSword(){
        swordLevel ++;
        
        swordDamage     = swordBaseDamage     * swordLevel *  damageMultiplier;
        swordSwingAngle = swordSwingBaseAngle * swordLevel;
        swordSwingSpeed = swordSwingBaseSpeed * swordLevel * rotationSpeedMultiplier;
    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
