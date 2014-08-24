using UnityEngine;
using System.Collections.Generic;

public class FireManager : MonoBehaviour {

	public static FireManager instance { get; private set; }
	private List<Flammable> fuel;

	
	//When the object awakens, we assign the static variable
	void Awake() 
	{

		instance = this;
		fuel = new List<Flammable> ();
	}

	// Use this for initialization
	void Start () {
		
	}

	public void Add(Flammable f){
		foreach(Flammable other in fuel){
			other.AddNeighbour(f);
			f.AddNeighbour(other);
		}
		fuel.Add(f);
	
	}
	
	// Update is called once per frame
	void Update () {
	}
}
