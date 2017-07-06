using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleRadarScript : MonoBehaviour {

	private EagleIA script;

	// Use this for initialization
	void Start () {
		script = (EagleIA)GetComponentInParent (typeof(EagleIA));
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player") 
		{
			script.lostPlayer = false;
			script.canFly = true;
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			script.BackToHome ();
			script.lostPlayer = true;
			script.canFly = true;
		}
	}
}
