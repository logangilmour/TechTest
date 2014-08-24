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
	}

	public void AddNeighbour(Flammable other){
		float dist = Vector3.Distance (gameObject.transform.position, other.gameObject.transform.position);

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
			this.renderer.material.SetFloat ("_Cutoff", burned);

			burned += Time.deltaTime * 0.1f;
			if (burned > 0.99f) burned = 0.99f;
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
