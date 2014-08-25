using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public GameObject guiObject;
	public float temp = 0f;

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
				float heat = 1/(dist);
				localtemp += heat;
			}
	
		}
		Debug.Log (temp + ", " + localtemp);
		temp += (localtemp - temp)/500;
		Rect newSize = new Rect(0,0, Screen.width, Screen.height); //Pixel Inset - Rect (x, y, width, height)
		guiObject.guiTexture.pixelInset = newSize;
		Color c = guiObject.guiTexture.color;
		if (temp > 0.1)
						c.a = (temp-0.3f)/0.5f;
				else
						c.a = 0f;
		guiObject.guiTexture.color = c;
		//guiObject.SetActiveRecursively(false);
	}
}
