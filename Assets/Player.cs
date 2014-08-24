using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player : MonoBehaviour {
public float jumpSpeed = 10;
private float distToGround;
public bool falling = false;
	private List<GameObject> list = new List<GameObject>();
	private int maxblox = 1;

void Start(){
  // get the distance to ground
  distToGround = collider.bounds.extents.y;
  Physics.gravity = new Vector3(0,-30,0);
  
}

bool IsGrounded(){
		Vector3 left = transform.position + new Vector3 (-collider.bounds.extents.x, 0, 0);
		Vector3 right = transform.position+new Vector3(collider.bounds.extents.x,0,0);
  return Physics.Raycast(left, -Vector3.up, distToGround + 0.1f) ||
  	Physics.Raycast(right, -Vector3.up, distToGround + 0.1f);
}
 
	void Update () {
		Vector3 vel = rigidbody.velocity;

		if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()){
			vel.y=jumpSpeed;
		}

  		if(!Input.GetKey(KeyCode.Space) && rigidbody.velocity.y>0){
			vel.y=0;
		rigidbody.velocity=vel;
  	}
  if (Input.GetKey(KeyCode.RightArrow) && !falling){
  	vel.x=10;
  }else if(Input.GetKey(KeyCode.LeftArrow) && !falling){
  	vel.x=-10;
  }else{
  	vel.x=0;
 	 	}
	if(Input.GetKeyDown(KeyCode.LeftShift)){
			if(vel.y>0)vel.y=0;
			float height = collider.bounds.extents.y;
			GameObject obj = (GameObject) GameObject.Instantiate(Resources.Load<GameObject>("Temp"),this.transform.position, Quaternion.identity);
			obj.transform.position+=Vector3.up*(this.collider.bounds.extents.y+obj.collider.bounds.extents.y);
			list.Add(obj);
			if(list.Count>maxblox){
				Destroy (list[0]);
				list.RemoveAt(0);
			}	
	}
		rigidbody.velocity=vel;  

	}
}