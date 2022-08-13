using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{

    [SerializeField] GameObject Player;
    [SerializeField] GameObject Skull;
    [SerializeField] 
    private Text _titleSword;
    [SerializeField] 
    private Text _titleSatellite;
    [SerializeField] 
    private Text _titleMissile;
    public Expbar expbar;

    //Player variables
    public int level                     = 1;
    public float xp                      = 0;
    public int xp2Next                   = 100;
    public int health                    = 100;
    public float regen                   = 1;
 
    // passive
    public float damageMultiplier        = 1;
    public float rotationSpeedMultiplier = 1;
    public float cooldownReduction       = 0;  // 15 = %15 cooldown reduction
    public float damageReduction         = 0;
    public int bonusProjectile           = 0;
    public float xpMultiplier            = 1;
 
    //world variables
    public float time                    = 0;
    public float spawnSpeed              = 5;
    public int spawnMultiplier           = 3;
    public float spawnInterval           = 1;
    public float spawnIntervalCurrent    = 1;

 
    //weapon variables
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
    public float enemyDamageMultiplier   = 1;
    public float enemyHealthMultiplier   = 1;

    public int skullLevel                = 1;
    public float skullBaseHealth         = 100;
    public float skullBaseDamage         = 10;
    public float xpPrize                 = 26;


    void gainXp(float amount){
        xp += amount * xpMultiplier;
        while(xp >= xp2Next){
            levelUp();
            xp -= xp2Next;
        }

    }
    void levelUp(){
        level ++;
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
        _titleSword.text = ""+0;
        _titleMissile.text=""+0;
        _titleSatellite.text=""+0;
        spawnInterval = spawnInterval / spawnSpeed;
    }


    void spawnSkull(){
        for(var i = 0; i<spawnMultiplier; i++){
            Debug.Log("skull spawned");  
            //spawn skull
            spawnIntervalCurrent = spawnInterval;
            GameObject newSkull = SkullPool.instance.GetPooledObj(); 

            Enemy e = newSkull.GetComponent<Enemy>();
            //e.speed = b.baseSpeed * speedMultiplier;
            newSkull.SetActive(true);
            e.maxHealth    = skullBaseHealth * enemyHealthMultiplier;
            e.currentHealth = skullBaseHealth * enemyHealthMultiplier;
            e.Player = Player;  
            
            float randx = Random.Range(4, 20);
            float randz = Random.Range(4, 20);
            float rands = 0; // random sign
            float randb = 0; // random sign
            if(Random.value<0.5f)
                rands=-1;
            else
                rands=1;
            if(Random.value<0.5f)
                randb=-1;
            else
                randb=1;

            newSkull.transform.position  = new Vector3(Player.transform.position.x + rands*randx, Player.transform.position.y, randb*randz);
        }
        

        
    }
    // Update is called once per frame
    void Update()
    {
        _titleSword.text = ""+swordLevel;
        _titleMissile.text=""+misilleLevel;
        _titleSatellite.text=""+satelliteLevel;
        time += Time.deltaTime;
        spawnIntervalCurrent -= Time.deltaTime;
        if(spawnIntervalCurrent < 0){
            spawnSkull();
        }

        
    }
}
