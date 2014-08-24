using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
	// Use this for initialization
	bool stopped = false;
	float life = 3f;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		life -= Time.deltaTime;
		if (life < 0) {
						Destroy (this.gameObject);
				}	
		life -= Time.deltaTime;
		if(stopped)		this.rigidbody.velocity = Vector3.zero;
	}

	void OnTriggerEnter (Collider other) {
		this.rigidbody.velocity = Vector3.zero;
		stopped = true;
		this.particleSystem.Stop ();
	}
}
