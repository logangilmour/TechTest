using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {
	Quaternion startRot;
	// Use this for initialization

	private int _uvTieX = 3;
	private int _uvTieY = 2;
	private int _fps = 10;
	
	private Vector2 _size;
	private Renderer _myRenderer;
	private int _lastIndex = -1;

	private bool burning = false;
	private Vector3 myvel = Vector3.zero;
	private float cooldown = 0.25f;
	float burned = 0.01f;

	void Start () {
		gameObject.tag = "Enemy";
		startRot = transform.rotation;
		foreach (Material material in this.renderer.materials){
			material.SetFloat ("_Cutoff", 0.01f);
		}

		_size = new Vector2 (1.0f / _uvTieX , 1.0f / _uvTieY);
		_myRenderer = renderer;
		if(_myRenderer == null)
			enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		cooldown -= Time.deltaTime;
		if(cooldown<0) cooldown = 0;
			this.renderer.material.SetFloat ("_Dist", Vector3.Distance(gameObject.transform.position,Walk.instance.gameObject.transform.position));
		transform.LookAt (Walk.instance.transform);
		transform.rotation = Quaternion.Euler (new Vector3(-90,transform.rotation.eulerAngles.y,0));


		if (!burning && Walk.instance.fired) {
			var vel = 1000 * Time.deltaTime;
			transform.rigidbody.velocity = transform.TransformDirection (Vector3.down * vel);
		} else if (burning) {
			transform.rigidbody.velocity = myvel;
			foreach (Material material in this.renderer.materials){
				material.SetFloat ("_Cutoff", burned);
			}
			burned += Time.deltaTime * 0.2f;
			if (burned > 0.8f) burned = 0.8f;
		}
		int index = 0;
		if(transform.rigidbody.velocity!=Vector3.zero)index = (((int)(Time.timeSinceLevelLoad * _fps)) % 4)+1;

		if(index != _lastIndex)
		{
			// split into horizontal and vertical index
			int uIndex = index % _uvTieX;
			int vIndex = index / _uvTieX;
			Debug.Log (uIndex+", "+vIndex);
			
			// build offset
			// v coordinate is the bottom of the image in opengl so we need to invert.
			Vector2 offset = new Vector2 (uIndex * _size.x, vIndex*_size.y);//1.0f - _size.y - vIndex * _size.y);
			
			_myRenderer.material.SetTextureOffset ("_MainTex", offset);
			//_myRenderer.material.SetTextureScale ("_MainTex", _size);
			
			_lastIndex = index;
		}


	}
	void StartFire(){
		burning = true;
		gameObject.tag = "Burning";
		foreach (Transform child in transform) {
			child.particleSystem.Play ();
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag ("Missile")) {
			if(!burning){
			myvel = transform.rigidbody.velocity;
						StartFire ();
			}
		}
	}
	void OnCollisionEnter(Collision c){
		if (cooldown <= 0) {
			cooldown = 0.25f;
						myvel = transform.TransformDirection (new Vector3 (0, 1f-Mathf.Round(Random.value)*2f, 1f-Mathf.Round (Random.value)*2f).normalized * (1000 * Time.deltaTime));
				}
	}
}
