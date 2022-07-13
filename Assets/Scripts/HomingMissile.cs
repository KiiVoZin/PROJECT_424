using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HomingMissile : MonoBehaviour {

	public Transform target;
	public float lifeTime = 0.0f;
	public float babyStateEnd = 1;
	public float speed = 5f;
	public float rotateSpeed = 1f;
	public float lifeStateEnd = 3;

	private Rigidbody rb;
	private Transform trans;
	

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		trans=GetComponent<Transform>();
	transform.position += transform.forward * 0.3f;// head start hoaming missile with 0.5
	}
	
	void FixedUpdate () {
		lifeTime+=Time.deltaTime;

		if(lifeTime >= lifeStateEnd){
			Destroy(gameObject);
		}
		if(lifeTime < babyStateEnd){
			transform.position += transform.forward * Time.deltaTime * speed / 30;
		}
		if(lifeTime >= babyStateEnd){
			rb.velocity=trans.forward*speed;
			var rocketlock=Quaternion.LookRotation(target.position - trans.position);
			rb.MoveRotation(Quaternion.RotateTowards(trans.rotation, rocketlock, rotateSpeed));
		}
		
	}

	
}