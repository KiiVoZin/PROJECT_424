using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float maxSpeed            = 100;
    public float speed               = 0;
    public float baseSpeed           = 10;
    public float lifeTime            = 10;
    public float lifeTimeLeft        ;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    /* public void setTarget(Transform v){
        this.target = v;
        return;
    } */
    void OnDisable()
    {   
        
        Debug.Log("PrintOnDisable: script was disabled");
    }

    void OnEnable()
    {
       
        Debug.Log("PrintOnEnable: script was enabled");
        lifeTimeLeft = lifeTime;
       
    }

    // Update is called once per frame
    void Update()
    {

        
        lifeTimeLeft -= Time.deltaTime;
        if(lifeTimeLeft < 0 ){
            gameObject.active = false;
        }

        transform.localPosition += transform.forward * speed * Time.deltaTime;
        //transform.localPosition = Vector3.MoveTowards (transform.localPosition, target, Time.deltaTime * speed);
        //transform.position += (target - transform.position).normalized * speed * Time.deltaTime;


    }
}
