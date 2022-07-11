using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Player;
    Collider m_ObjectCollider;
    Rigidbody m_Rigidbody;
   
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

        //Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponent<Rigidbody>();
        //Fetch the GameObject's Collider (make sure they have a Collider component)
        m_ObjectCollider = GetComponent<Collider>();
        //Here the GameObject's Collider is not a trigger
        m_ObjectCollider.isTrigger = false;
        //Output whether the Collider is a trigger type Collider or not
    }

    // Update is called once per frame
    void Update()
    {   
        
        if(dashReady ){
            dashTimeCurrent -= Time.deltaTime;
            
            if(dashTimeCurrent < 0){
                dashReady = false;
                dashTimeCurrent = dashTime;
            }else{
                //dash through to player's last position
                //transform.position = Vector3.MoveTowards(transform.position, dashTarget, dashSpeed * Time.deltaTime);
                m_Rigidbody.AddForce(transform.forward * dashSpeed * Time.deltaTime);
                //transform.Translate(Vector3.forward * dashSpeed * Time.deltaTime);
            }
        }else{
            //move towards to player normally
            //transform.position=Vector3.MoveTowards(transform.position,Player.transform.position , (moveSpeed) * Time.deltaTime);
            m_Rigidbody.AddForce(transform.forward * moveSpeed/(dashCooldownCurrent+1.0f) * Time.deltaTime);

            dashCooldownCurrent -= Time.deltaTime;
            if(dashCooldownCurrent <= 0){
                dashReady = true;
                dashTarget = Player.transform.position;
                dashCooldownCurrent = dashCooldown;
            }
        }


        //slowly look at player
        if(!dashReady){
            //fly above to observe
            m_Rigidbody.AddForce(transform.up * Random.Range(12.0f / dashCooldown, 240.0f / dashCooldown) * Time.deltaTime);

            Vector3 relativePos = Player.transform.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp( transform.rotation, toRotation, turnAroundSpeed * Time.deltaTime );
        }
        //transform.LookAt(Player.transform);
    }
}
