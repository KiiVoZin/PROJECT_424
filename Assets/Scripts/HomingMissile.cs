using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HomingMissile : MonoBehaviour {

	public Transform target;
	
	public float speed = 5f;
	public float rotateSpeed = 200f;

	private Rigidbody rb;
	private Transform trans;
	

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		trans=GetComponent<Transform>();

	}
	
	void FixedUpdate () {
		rb.velocity=trans.forward*speed;

		var rocketlock=Quaternion.LookRotation(target.position - trans.position);

		rb.MoveRotation(Quaternion.RotateTowards(trans.rotation,rocketlock,rotateSpeed));


	}

	
}