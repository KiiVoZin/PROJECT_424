using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
{

    public float cooldown;
    public float cooldownCurrent;
    public int missileCount;
    
    

    private GameObject[] multipleEnemy;
    public Transform closestEnemy;
    public bool EnemyContact;
    public GameObject prefab;
    [SerializeField] GameObject Player;

    public GameObject PoolParent;


   
    // Start is called before the first frame update
    void Start()
    {

        
        closestEnemy=null;
        EnemyContact=false;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Player.transform.position;
        cooldownCurrent -= Time.deltaTime;
        closestEnemy = getClosestEnemy();
        fireMissile();
        
        
        
    }

    void fireMissile (){
        if(cooldownCurrent <= 0){
            
            for(var i = 0; i < missileCount; i++){
                
                GameObject o = Instantiate(prefab,Player.transform.position,Player.transform.rotation,PoolParent.transform);
                
                HomingMissile hm=o.GetComponent<HomingMissile>();
                hm.target=closestEnemy;
                hm.transform.Translate(0,0,-0.3f);
                hm.transform.Rotate(-90,0,0);
                hm.transform.Rotate(new Vector3(0, 0 , i*180/missileCount), Space.World);

                
                //hm.GetComponent<Rigidbody>().AddForce(hm.transform.forward* 100.0f);
                //arrange angle correctly
                if(missileCount % 2 == 1){
                    if(missileCount != 1){
                        hm.transform.Rotate(new Vector3(0, 0 , -(180/missileCount)*(missileCount/2)), Space.World);
                    }
                }else{
                    hm.transform.Rotate(new Vector3(0, 0 , -(180/missileCount)*((missileCount-2)/2 +0.5f)), Space.World);
                }
            }
            

            cooldownCurrent = cooldown;
            
        }
    }

    
    public Transform getClosestEnemy(){
        multipleEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        float closestdis = Mathf.Infinity;
        Transform trans = null;

        foreach (GameObject go in multipleEnemy)
        {
            float currentDis;
            currentDis = Vector3.Distance(transform.position, go.transform.position);
            if (currentDis < closestdis)
            {
                closestdis = currentDis;
                trans = go.transform;
            }
        }
        return trans;

    }

}
