using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Player;

    public float moveSpeed=1.5f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=Vector3.MoveTowards(transform.position,Player.transform.position,moveSpeed*Time.deltaTime);
        //transform.up=Player.transform.position-transform.position;

        
        
    }
}
