using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flammable : MonoBehaviour {
	public float flashpoint = 0f;
	public bool burning = false;
	public bool emitting = false;
	private float burned=0.01f;
	public Dictionary<Flammable,float> neighbours = new Dictionary<Flammable,float>();


	void Start () {
		FireManager.instance.Add (this);
		foreach (Material material in this.renderer.materials){
			material.SetFloat ("_Cutoff", burned);
		}
	}
	
	public void AddNeighbour(Flammable other){
		Vector3 p1 = gameObject.collider.ClosestPointOnBounds (other.transform.position);
		Vector3 p2 = other.collider.ClosestPointOnBounds (transform.position);
		float dist = Vector3.Distance (p1,p2);
		if(dist<25)neighbours.Add (other, 1/(dist*dist));
		}		

	// Update is called once per frame
	void Update () {
		foreach (Material material in this.renderer.materials){
			material.SetFloat ("_Dist", Vector3.Distance(gameObject.transform.position,Walk.instance.gameObject.transform.position));
		}
		if (!burning) {
			foreach (KeyValuePair<Flammable,float> n in neighbours) {
				if (n.Key.burning) {
					flashpoint -= n.Value;
				}
			}
			if (flashpoint < 0 && !burning) {
				StartFire ();
			}	
		} else {
			if(!emitting)StartFire();
			foreach (Material material in this.renderer.materials){
				material.SetFloat ("_Cutoff", burned);
			}
			burned += Time.deltaTime * 0.1f;
			if (burned > 0.8f) burned = 0.8f;
		}
	}

	void StartFire(){
		burning = true;
		emitting = true;
		foreach (Transform child in transform) {
			child.particleSystem.Play ();
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag ("Missile")) {
			StartFire();
		}
	}
}
