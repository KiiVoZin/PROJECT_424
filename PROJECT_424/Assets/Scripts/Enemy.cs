using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Player;

    public float moveSpeed           = 1.5f;
    public float turnAroundSpeed     = 2.0f;
    public float dashSpeed           = 5.0f;
    public float dashTime            = 2.0f;
    public float dashTimeCurrent     = 2.0f;
    public float dashCooldown        = 5.0f;
    public float dashCooldownCurrent = 5.0f;
    public bool  dashReady           = false;
    public Vector3 dashTarget;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {   
        
        //and distance is greater than some d
        if(dashReady ){
            dashTimeCurrent -= Time.deltaTime;
            
            if(dashTimeCurrent < 0){
                dashReady = false;
                dashTimeCurrent = dashTime;
            }else{
                //dash through to player's last position
                //transform.position=Vector3.MoveTowards(transform.position, dashTarget, dashSpeed*Time.deltaTime);
               
                transform.Translate(Vector3.forward * dashSpeed * Time.deltaTime);

            }
        }else{
            //move towards to player normally
            transform.position=Vector3.MoveTowards(transform.position,Player.transform.position ,(moveSpeed*dashCooldownCurrent/dashCooldown)*Time.deltaTime);

            dashCooldownCurrent -= Time.deltaTime;
            if(dashCooldownCurrent < 0){
                dashReady = true;
                dashTarget = Player.transform.position;
                dashCooldownCurrent = dashCooldown;
            }
        }
        

        
        

        //slowly look at player
        if(!dashReady){
            Vector3 relativePos = Player.transform.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp( transform.rotation, toRotation, turnAroundSpeed * Time.deltaTime );
        }
        
        
        
    }
}
