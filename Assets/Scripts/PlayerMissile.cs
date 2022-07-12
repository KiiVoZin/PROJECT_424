using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
{

    private GameObject[] multipleEnemy;
    public Transform closestEnemy;
    public bool EnemyContact;
    // Start is called before the first frame update
    void Start()
    {
        closestEnemy=null;
        EnemyContact=false;
    }

    // Update is called once per frame
    void Update()
    {
        closestEnemy = getClosestEnemy();
        
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
