using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    public GameObject Player;
    public float swingAngle;
    public float swingAngleCurrent;
    public float cooldown;
    public float cooldownCurrent;
    public float swingSpeedMax; 
    public float swingSpeed;
    public float radiusMax;
    public float radius;
    public bool active;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameManager = GameObject.Find("GM");
        GM  gm = (GM) gameManager.GetComponent(typeof(GM));
        swingAngle = gm.swordSwingAngle;
        swingSpeed = gm.swordSwingSpeed;
        radius     = gm.swordRadius;
        cooldown   = gm.swordCooldown;
        transform.Translate(0,0,radius);
    }

    

    // Update is called once per frame
    void Update()
    {

        GameObject gameManager = GameObject.Find("GM");
        GM  gm = (GM) gameManager.GetComponent(typeof(GM));
        swingAngle = gm.swordSwingAngle;
        swingSpeed = gm.swordSwingSpeed;
        radius     = gm.swordRadius;
        cooldown   = gm.swordCooldown;
        
        float sinFrequency = 1000.0f;
        cooldownCurrent -= Time.deltaTime;
        if(cooldownCurrent < 0 ){
            active = true;
            
        }
        sinFrequency = (swingAngle / swingSpeed) ;

        if(active){
            transform.position = Player.transform.position;
            transform.Translate(0,0,radius);
            transform.Rotate( Vector3.up, swingSpeed * Time.deltaTime);
            transform.position += new Vector3( 0.0f,0.5f*Mathf.Sin(Time.time * sinFrequency), 0.0f);
            swingAngleCurrent += swingSpeed * Time.deltaTime;
            if(swingAngleCurrent >= swingAngle){
                active = false;

                cooldownCurrent = cooldown;
                swingAngleCurrent = 0;
                
            }
        }



        

    }
}
