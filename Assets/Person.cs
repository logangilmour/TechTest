using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {
	Quaternion startRot;
	// Use this for initialization
	void Start () {
		startRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		this.renderer.material.SetFloat ("_Dist", Vector3.Distance(gameObject.transform.position,Walk.instance.gameObject.transform.position));
		transform.LookAt (Walk.instance.transform);
		transform.rotation = Quaternion.Euler (new Vector3(-90,transform.rotation.eulerAngles.y,0));

	}
}
