using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
	// Use this for initialization
	float life = 3f;

	void Start () {
		this.rigidbody.AddRelativeForce (Vector3.forward * -5000);
	}
	
	// Update is called once per frame
	void Update () {
		life -= Time.deltaTime;
		if (life < 0) {
						Destroy (this.gameObject);
				}	
		life -= Time.deltaTime;
	}

	void OnTriggerEnter (Collider other) {
		this.rigidbody.velocity = Vector3.zero;
		this.particleSystem.Stop ();
	}
}
