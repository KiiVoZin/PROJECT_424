using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Player;
    Collider m_ObjectCollider;
    Rigidbody m_Rigidbody;
    [SerializeField] GameObject bullet;
   
    public float moveSpeed               = 2.2f;
    public float moveSpeedMax            = 2.2f;
    public float turnAroundSpeed         = 3.3f;
    public float turnAroundSpeedMax      = 4.0f;
    public float dashSpeed               = 6.0f;
    public float dashTime                = 0.5f;
    public float dashTimeCurrent         = 0.5f;
    public float dashCooldown            = 5.0f;
    public float dashCooldownCurrent     = 5.0f;
    public bool  dashReady               = false;
    public float maxSpeed                = 20f;
    public int state                     = 0;  // 0: shoot from above, 1: Follow and triple dash, 2: sinus hell, 3: fast follow, 4: state 0 but with eyes too
    public float stateDuration           = 5;
    public float stateDurationCurrent    = 5;
    public int dashCount                 = 3;
    public int dashCountCurrent          = 3;
    public float dashReload              = 5;
    public float dashReloadCurrent       = 5;
    public float fireRate                = 8.0f;
    private float timeToNextFire         = 0;
    public float moveFasterRadius        = 3;
    public Vector3 dashTarget;

    public int R = 3;
    private int randomDirection = 1;
    /* public int patternCounter = 0; */
    /* private int [] pattern1    = {1,0,0,1}; */
    /* private int [] pattern2    = {1,0,1,0}; */
    /* private int [] pattern3    = {1,0,0,0,0,0,0,1}; */
    /* private int [] pattern4    = {1,1,1,0,0,1,0,0}; */

    void Start()
    {

        //Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponent<Rigidbody>();
    
        //Fetch the GameObject's Collider (make sure they have a Collider component)
        m_ObjectCollider = GetComponent<Collider>();
        //Here the GameObject's Collider is not a trigger
        m_ObjectCollider.isTrigger = false;
        //Output whether the Collider is a trigger type Collider or not
        //Debug.Log("Trigger On : " + m_ObjectCollider.isTrigger);

        
    }

    void nextState(int currentState){
        int[] statePool         = {0, 0, 1, 1, 1, 1, 2,3,3,3,3,4,4};
        int randomIndex         = UnityEngine.Random.Range(0,statePool.Length);
        int randomDir     = UnityEngine.Random.Range(0,2);
        if(randomIndex == 2){
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeed;
            R = UnityEngine.Random.Range(3, 9);
            Debug.Log(R);
            if(randomDir == 0){
                randomDirection = -1;
            }else{
                randomDirection = 1;
            }
        }
        state =  statePool[randomIndex];
    }
    void fireBullet(int count, float rateMultiplier, float speedMultiplier, float offset){
        timeToNextFire = 1.0f/(fireRate*rateMultiplier);
        //patternCounter++;
        for(int i = 0; i < count; i++){
            GameObject newBullet = EnemyBulletPool.instance.GetPooledObj(); 
            if(newBullet == null){
                return;
            } 
            
            Bullet b = newBullet.GetComponent<Bullet>();
            b.speed = b.baseSpeed * speedMultiplier;
            newBullet.SetActive(true);
            newBullet.transform.position  = transform.position;
            newBullet.transform.rotation  = transform.rotation;
            newBullet.transform.Rotate(0,offset + i * 360/count, 0);
            
        }
        
    }void fireBulletFromEyes(float rateMultiplier, float speedMultiplier){
        timeToNextFire = 1.0f/(fireRate*rateMultiplier);
        //patternCounter++;  
        GameObject newBullet  = EnemyBulletPool.instance.GetPooledObj();
        if(newBullet == null){
            return;
        } 
        Bullet b  = newBullet.GetComponent<Bullet>();
        b.speed   = b.baseSpeed  * speedMultiplier;
        newBullet. SetActive(true);
        newBullet. transform.position  = transform.Find("eyeL").transform.position;
        newBullet. transform.rotation  = transform.Find("eyeL").transform.rotation;
        
        GameObject newBullet2  = EnemyBulletPool.instance.GetPooledObj();
        if(newBullet2 == null){
            return;
        } 
        Bullet b2      = newBullet2.GetComponent<Bullet>();
        b2.speed   = b2.baseSpeed  * speedMultiplier;
        newBullet2. SetActive(true);
        newBullet2. transform.position  = transform.Find("eyeR").transform.position;
        newBullet2. transform.rotation  = transform.Find("eyeR").transform.rotation;
        
        
    }

    // Update is called once per frame
    void Update(){
        timeToNextFire          -= Time.deltaTime;
        stateDurationCurrent    -= Time.deltaTime;

        if(stateDurationCurrent < 0){
            nextState(state);
            stateDurationCurrent = stateDuration;
            
        }
        // recharge dashes when they arent any left
        if(dashCountCurrent == 0){
            dashReloadCurrent -= Time.deltaTime;
        }
        if(dashReloadCurrent<=0){
            print(dashCount - dashCountCurrent + " dash(s) has been reloaded" );
            dashCountCurrent = dashCount;
            dashReloadCurrent = dashReload;
        }
        if(state == 0){
            int x = 1500;
            int sinFrequency = 2;
            turnAroundSpeed = turnAroundSpeedMax / 2;
            transform.position += new Vector3( 0.0f,Mathf.Sin(Time.time * sinFrequency) / (x/sinFrequency), 0.0f);
            //Fire bullets
            if(timeToNextFire <= 0 ){
                fireBullet(1,1.0f,2.0f,0);
            }
        }

        if(state == 4){
            
            turnAroundSpeed = turnAroundSpeedMax / 2;
            //transform.position += new Vector3( 0.0f,0.1f, 0.0f);
            //Fire bullets
            if(timeToNextFire <= 0 ){
                fireBullet(1,1.0f,2.0f,0);
                fireBulletFromEyes(1.0f,2.0f);
                
            }
            
            
        }
    }
    void FixedUpdate()
    {  
        //Limit the velocity
        if(GetComponent<Rigidbody>().velocity.magnitude > maxSpeed)
        {
               GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeed;
        }
        
        if(state == 1){
            float distance = Vector3.Distance(transform.position, dashTarget);
            turnAroundSpeed = turnAroundSpeedMax;
            if(dashReady && dashCountCurrent > 0 && distance < moveFasterRadius){
                dashTimeCurrent -= Time.deltaTime;
                
                if(dashTimeCurrent < 0){
                    dashReady = false;
                    dashCountCurrent -= 1;
                    dashTimeCurrent = dashTime;
                }else{
                    //dash through to player's last position
                    if(dashCountCurrent > 0 ){
                        m_Rigidbody.AddForce(transform.forward * dashSpeed * Time.deltaTime );
                    }
                    
                }
            }else{
                //move towards to player normally
                //transform.position=Vector3.MoveTowards(transform.position,Player.transform.position , (moveSpeed) * Time.deltaTime);
                if(distance < moveFasterRadius)
                    m_Rigidbody.AddForce(transform.forward * moveSpeed/(dashCooldownCurrent+1.0f) * Time.deltaTime);
                    if(dashCountCurrent > 0)
                        dashCooldownCurrent -= Time.deltaTime;
                //if the player is far, move faster
                if(distance >= moveFasterRadius){
                    m_Rigidbody.AddForce(transform.forward * moveSpeed /(dashCooldownCurrent+1.0f) * distance * Time.deltaTime);
                    dashReady = false;
                    //Fire bullets
                    if(timeToNextFire <= 0 && state == 1){
                        fireBullet(1,3f, 3f,0);

                    
                    }
                }
                    

                
                if(dashCooldownCurrent <= 0){
                    dashReady = true;
                    dashTarget = Player.transform.position;
                    dashCooldownCurrent = dashCooldown;
                }
            }
        }

        if(state == 2){
            
            float sinTurnSpeed = 450/R * randomDirection;
            int x = 40;
            int sinFrequency = 2;
           
            transform.position += new Vector3( 0.0f,  (Mathf.Sin(Time.time * sinFrequency)-0.03f)/ (x/sinFrequency), 0.0f);
            //Fire bullets
            if(timeToNextFire <= 0 && stateDuration - stateDurationCurrent > 0.99f){
                fireBullet(R,24/R,1,0);
            }
            transform.Rotate(0,sinTurnSpeed * Time.deltaTime,0);
        }
        
        if(state == 3){
            
            int x = 40;
            int sinFrequency = 2;
            moveSpeed = moveSpeedMax * 15;
            transform.position += new Vector3( (Mathf.Sin(Time.time * sinFrequency)-0.03f)/ (x/sinFrequency),
              (Mathf.Sin(Time.time * sinFrequency)-0.03f)/ (x/sinFrequency), (Mathf.Sin(Time.time * sinFrequency)-0.03f)/ (x/sinFrequency));
            //Fire bullets
            /* if(timeToNextFire <= 0 && stateDuration - stateDurationCurrent > 0.5){
                fireBullet(R,24/R,1);
            } */

            
            m_Rigidbody.AddForce(transform.forward * moveSpeed/(dashCooldownCurrent+1.0f) * Time.deltaTime);
            /* m_Rigidbody.constraints = RigidbodyConstraints.None;
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationY; */
            
            
        }
        if(state != 3)moveSpeed = moveSpeedMax;


        //slowly look at player
        if(! (state == 2 )){
            Vector3 relativePos = Player.transform.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp( transform.rotation, toRotation, turnAroundSpeed * Time.deltaTime );
        }
        
        //transform.LookAt(Player.transform);

        //fly above to observe
        //m_Rigidbody.AddForce(transform.up * UnityEngine.Random.Range(144.0f / dashCooldown, 240.0f / dashCooldown ) * m_Rigidbody.mass * Time.deltaTime);
    }
}
