﻿using UnityEngine;
using System.Collections;

public class Walk : MonoBehaviour {
	public static Walk instance { get; private set; }
	private float cooldown = 0.05f;
	private float left = 1;
	public Transform leftsound;
	public Transform rightsound;

	public Transform missile;
	public bool fired = false;


	void Awake(){
				instance = this;
		}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var lr = 0;
		var fb = 0;
		fb += Input.GetKey (KeyCode.Comma) ? -1 : 0;
		fb += Input.GetKey (KeyCode.O) ? 1 : 0;
		lr += Input.GetKey (KeyCode.A) ? 1 : 0;
		lr += Input.GetKey (KeyCode.E) ? -1 : 0;
		var vel = 1500 * Time.deltaTime;
		transform.rigidbody.velocity = transform.TransformDirection (Vector3.ClampMagnitude(new Vector3 (vel*lr, 0, vel*fb),vel));
		Fire ();
	}

	void Fire(){
		if (Input.GetMouseButton(0) && cooldown <= 0) {
			fired = true;
						cooldown = 0.2f;
			left*=-1;
			Instantiate(missile,transform.position+this.transform.rotation*Vector3.left*left*2,this.transform.rotation);
			if(left>0)leftsound.GetComponent<AudioSource>().Play();
			else rightsound.GetComponent<AudioSource>().Play();
				
				} else {
						cooldown -= Time.deltaTime;
				}	

	}
	void OnCollisionEnter (Collision c) {
		if (c.collider.CompareTag ("Burning") || c.collider.CompareTag ("Enemy")) {
			Game.instance.over=true;
		}
	}
}
