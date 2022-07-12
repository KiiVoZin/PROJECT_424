using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
{

    public float cooldown;
    public float cooldownCurrent;
    public int misilleCount;
    
    

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
            for(var i = 0; i < misilleCount; i++){
                
                GameObject o = Instantiate(prefab,Player.transform.position,Player.transform.rotation,PoolParent.transform);
                
                HomingMissile hm=o.GetComponent<HomingMissile>();
                hm.target=closestEnemy;

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
