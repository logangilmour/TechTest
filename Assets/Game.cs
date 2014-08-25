using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public GameObject guiObject;
	public float temp = 0f;
	public bool over = false;
	public static Game instance;

	void Awake(){
		instance = this;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI () {
		float localtemp = 0;
		foreach (Flammable f in FireManager.instance.fuel) {
			if(f.burning){
				Vector3 p1 = f.collider.ClosestPointOnBounds (transform.position);
				float dist = Vector3.Distance (p1,transform.position)+1;
				float heat = 1/(dist*dist);
				localtemp += heat;
			}
	
		}
		temp += ((localtemp - temp)/10)*Time.deltaTime;
		Rect newSize = new Rect(0,0, Screen.width, Screen.height); //Pixel Inset - Rect (x, y, width, height)
		guiObject.guiTexture.pixelInset = newSize;
		Color c = guiObject.guiTexture.color;
		if (temp > 0.1)
						c.a = (temp-0.1f)/0.3f;
				else
						c.a = 0f;

		if (c.a >= 1f) {
						over = true;
				}

		if (over) {
						c.a = 1f;

				}
		guiObject.guiTexture.color = c;

		if(Input.GetKey(KeyCode.Space)){
			Application.LoadLevel(Application.loadedLevel);
		}	
		//guiObject.SetActiveRecursively(false);
	}
}
