using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Player;
    Collider m_ObjectCollider;
    
   
    public float moveSpeed           = 2.2f;
    public float turnAroundSpeed     = 3.3f;
    public float dashSpeed           = 6.0f;
    public float dashTime            = 0.5f;
    public float dashTimeCurrent     = 0.5f;
    public float dashCooldown        = 5.0f;
    public float dashCooldownCurrent = 5.0f;
    public bool  dashReady           = false;
    public Vector3 dashTarget;
    void Start()
    {

        
        //Fetch the GameObject's Collider (make sure they have a Collider component)
        m_ObjectCollider = GetComponent<Collider>();
        //Here the GameObject's Collider is not a trigger
        m_ObjectCollider.isTrigger = false;
        //Output whether the Collider is a trigger type Collider or not
        Debug.Log("Trigger On : " + m_ObjectCollider.isTrigger);
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        //and distance is greater than some d
        if(dashReady ){
            dashTimeCurrent -= Time.deltaTime;
            
            if(dashTimeCurrent <= 0){
                dashReady = false;
                dashTimeCurrent = dashTime;
            }else{
                //dash through to player's last position
                transform.position = Vector3.MoveTowards(transform.position, dashTarget, dashSpeed * Time.deltaTime);
                //transform.Translate(Vector3.forward * dashSpeed * Time.deltaTime);
            }
        }else{
            //move towards to player normally
            transform.position=Vector3.MoveTowards(transform.position,Player.transform.position , (moveSpeed) * Time.deltaTime);

            dashCooldownCurrent -= Time.deltaTime;
            if(dashCooldownCurrent <= 0){
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
        //transform.LookAt(Player.transform);

    }
}
