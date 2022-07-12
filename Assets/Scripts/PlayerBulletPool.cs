using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletPool : MonoBehaviour
{

    public static PlayerBulletPool instance;
    public GameObject prefab;
    public List<GameObject> pooledObjects;
    public int poolVolume = 24;

    void Awake(){
        if (instance == null){
            instance = this;
        }

        pooledObjects = new List<GameObject>();
        for(int i = 0; i < poolVolume; i++){
            GameObject o = Instantiate(prefab, transform);
            o.SetActive(false);
            pooledObjects.Add(o);
            
        }
    }

    public GameObject GetPooledObj(){
        for(int i = 0; i < poolVolume; i++){
            if(! pooledObjects[i].activeInHierarchy){
                return pooledObjects[i];
            }
        }
        return null;
    }
}
