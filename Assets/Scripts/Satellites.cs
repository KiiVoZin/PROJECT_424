using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellites : MonoBehaviour
{
    public GameObject myPrefab;
    [SerializeField] GameObject Player;

    
    public int satCount = 3;
    public int speed = 180;
    public float radius = 5.0f;

    // Start is called before the first frame update
    void Start()
    {

        var sats = new GameObject[satCount];
        for(var i  = 0; i < sats.Length; i++){
            var clone = Instantiate(myPrefab, transform);
            sats[i] = clone;
            
            sats[i].transform.position = new Vector3(0,1,radius)+transform.position;
            sats[i].transform.RotateAround(transform.position, Vector3.up, 360/satCount * i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = Player.transform.position;
        //transform.eulerAngles = new Vector3(0,  rotationSpeed * Time.deltaTime, 0);
        //transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        transform.RotateAround(transform.position, Vector3.up, -speed * Time.deltaTime );
    }
}
