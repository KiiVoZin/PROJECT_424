using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{

    [SerializeField] GameObject Player;
    [SerializeField] GameObject Skull;
    [SerializeField] GameObject KingSkull;
    [SerializeField] GameObject Chest;
    [SerializeField] 
    private Text _titleSword;
    [SerializeField] 
    private Text _titleSatellite;
    [SerializeField] 
    private Text _titleMissile;
    [SerializeField] 
    private Text _titleHealth;
    [SerializeField] 
    private Text _titleArmor;
    [SerializeField] 
    private Text _titleProjectileNum;
    [SerializeField] 
    private Text _titlexpbonus;
    [SerializeField] 
    private Text _titleDamage;
    [SerializeField] 
    private Text _titleCooldown;

    public Expbar expbar;

    //Player variables
    public int level                     = 1;
    public float xp                      = 0;
    public int xp2Next                   = 100;
    public int health                    = 100;
 
    // passive
    public float damageMultiplier        = 1;
    public float regen                   = 1;
    public float cooldownReduction       = 0;  // 15 = %15 cooldown reduction
    public float damageReduction         = 0;
    public int bonusProjectile           = 0;
    public float xpMultiplier            = 1;
    public float rotationSpeedMultiplier = 1;
 
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
    public float misilleCooldown         = 3;
 
    public int   satelliteLevel          = 1;
    public int   satelliteCount          = 1;
    public int   satelliteBaseSpeed      = 360;
    public float satelliteSpeed          = 360;
    public int   satelliteBaseDamage     = 100;
    public float satelliteDamage         = 100;
    public float satelliteBaseRadius     = 5;
    public float satelliteRadius         = 5;


    public int   swordLevel              = 1;
    public int   swordSwingBaseAngle     = 360;
    public float swordSwingAngle         = 360;
    public int   swordSwingBaseSpeed     = 360;
    public float swordSwingSpeed         = 360;
    public int   swordBaseDamage         = 200;
    public float swordDamage             = 200;
    public float swordBaseRadius         = 3;
    public float swordRadius             = 3;
    public float swordBaseCooldown       = 5;
    public float swordCooldown           = 5;


    
    //Enemy variables
    public float enemyDamageMultiplier   = 1;
    public float enemyHealthMultiplier   = 1;

    public int skullLevel                = 1;
    public float skullBaseHealth         = 100;
    public float skullBaseDamage         = 10;
    public float skullXpPrize            = 16;

    public int kingLevel                 = 1;
    public float kingSkullBaseHealth     = 10000;
    public float kingSkullBaseDamage     = 5;
    public float kingSkullXpPrize        = 1000;


    public void gainXp(float amount){
        xp += amount * xpMultiplier;
        while(xp >= xp2Next){
            levelUp();
            xp -= xp2Next;
        }

    }
    public void levelUp(){
        level ++;
        //open levelUp ui
        Debug.Log("level up");
        xp2Next = level * 100;


        var rUp = Random.Range(1,4);
        if(rUp == 1) upgradeMisille();
        if(rUp == 2) upgradeSatellite();
        if(rUp == 3) upgradeSword();


        if(level % 10 == 0)spawnKingSkull();
        
    }
    public void spawnKingSkull(){
        float randx = Random.Range(4, 8);
        float randz = Random.Range(4, 8);
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
        
        
        Vector3 randomPosition = new Vector3(Player.transform.position.x + rands*randx, -0.25f,  Player.transform.position.z+ randb*randz);

        GameObject newKing = KingPool.instance.GetPooledObj(); 

        KingSkull e = newKing.GetComponent<KingSkull>();
        newKing.SetActive(true);
        e.maxHealth     = kingSkullBaseHealth * enemyHealthMultiplier;
        e.currentHealth = kingSkullBaseHealth * enemyHealthMultiplier;
        e.Player = Player;    

        e.transform.position = randomPosition;   
    }
    public void getRandomRune(){
        var rUp = Random.Range(1,7);
        if(rUp == 1) upgradeDamageMultiplier(0.15f);
        if(rUp == 2) upgradeRegen(1);
        if(rUp == 3) upgradeCooldownReduction(5);
        if(rUp == 4) upgradeDamageReduction(3);
        if(rUp == 5) upgradeBonusProjectile(1);
        if(rUp == 6) upgradeXpMultiplier(0.25f);
    }


    public void upgradeDamageMultiplier(float amount){
        damageMultiplier += amount;
    }
    public void upgradeRegen(float amount){
        regen += amount;
    }
    public void upgradeCooldownReduction(float amount){
        cooldownReduction += amount;
    }
    public void upgradeDamageReduction(float amount){
        damageReduction += amount;
    }
    public void upgradeBonusProjectile(int amount){
        bonusProjectile += amount;
    }
    public void upgradeXpMultiplier(float amount){
        xpMultiplier += amount;
    }

    
    public void upgradeMisille(){
        misilleLevel ++;
        Debug.Log("Misilles upgraded!");
        misilleDamage   = misilleBaseDamage * (3 + misilleLevel)/3.0f  * damageMultiplier;
        misilleCount    = misilleLevel + bonusProjectile;
        misilleCooldown = misilleBaseCooldown * (100 - cooldownReduction)/100.0f;
    
    }
    public void upgradeSatellite(){
        satelliteLevel ++;
        Debug.Log("Satellite upgraded!");
        satelliteDamage = satelliteBaseDamage * (3 + satelliteLevel)/3.0f  * damageMultiplier;
        satelliteSpeed  = satelliteBaseSpeed  * (1 + satelliteLevel)/10.0f  * rotationSpeedMultiplier;
        satelliteCount  = satelliteLevel + bonusProjectile;
        satelliteRadius = satelliteBaseRadius * (12 + satelliteLevel)/12.0f;
        Debug.Log(satelliteCount);
        GameObject go = GameObject.Find("Satellites");
        Satellites  satellites = (Satellites) go.GetComponent(typeof(Satellites));
        satellites.increaseSatCount();
    
    }

    public void upgradeSword(){
        swordLevel ++;
        Debug.Log("Sword upgraded!");
        swordDamage     = swordBaseDamage     * (3+swordLevel)/3.0f *  damageMultiplier;
        swordSwingAngle = swordSwingBaseAngle * swordLevel;
        swordSwingSpeed = swordSwingBaseSpeed * (5 + swordLevel)/5.0f * rotationSpeedMultiplier;
        swordRadius     = swordBaseRadius * (6 + swordLevel)/6.0f;
        swordCooldown   = swordBaseCooldown * (100 - cooldownReduction)/100.0f;

    
    }
    // Start is called before the first frame update
    void Start()
    {   
        _titleSword.text = ""+0;
        _titleMissile.text=""+0;
        _titleSatellite.text=""+0;
        spawnInterval = spawnInterval / spawnSpeed;
    }
    void spawnChest(){

        float randx = Random.Range(4, 32);
        float randz = Random.Range(4, 32);
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

        Instantiate(Chest, new Vector3(Player.transform.position.x + rands*randx, -1,  Player.transform.position.z+ randb*randz), Quaternion.identity);
    }

    void spawnSkull(){
        for(var i = 0; i<spawnMultiplier; i++){
             
            //spawn skull
            spawnIntervalCurrent = spawnInterval;
            GameObject newSkull = SkullPool.instance.GetPooledObj(); 

            Enemy e = newSkull.GetComponent<Enemy>();
            //e.speed = b.baseSpeed * speedMultiplier;
            newSkull.SetActive(true);
            e.maxHealth    = skullBaseHealth * enemyHealthMultiplier;
            e.currentHealth = skullBaseHealth * enemyHealthMultiplier;
            e.Player = Player;
            
            float randx = Random.Range(4, 32);
            float randz = Random.Range(4, 32);
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

            newSkull.transform.position  = new Vector3(Player.transform.position.x + rands*randx, 1, Player.transform.position.z + randb*randz);
        }
        

        
    }
    // Update is called once per frame
    void Update()
    {
        _titleSword.text = ""+swordLevel;
        _titleMissile.text=""+misilleLevel;
        _titleSatellite.text=""+satelliteLevel;

        _titleArmor.text = ""+damageReduction;
        _titleCooldown.text=""+cooldownReduction;
        _titleDamage.text=""+damageMultiplier;
        _titleProjectileNum.text=""+bonusProjectile;
        _titleHealth.text=""+health;
        _titlexpbonus.text=""+xpMultiplier;
        
        time += Time.deltaTime;
        spawnIntervalCurrent -= Time.deltaTime;

        if(spawnIntervalCurrent < 0){
            spawnSkull();
            if(Random.value<0.01f){
                spawnChest();
            }
            
        }

        
    }
}
