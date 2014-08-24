using UnityEngine;
using System.Collections;

public class burn : MonoBehaviour {
	float burned = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.renderer.material.SetFloat ("_Cutoff", burned);
		burned += Time.deltaTime * 0.1f;
		if (burned > 0.99f)
						burned = 0.99f;


	}
}
