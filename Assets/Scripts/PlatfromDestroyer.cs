using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfromDestroyer : MonoBehaviour {

	public GameObject platfromDestructionPoint;


	void Start () {

		platfromDestructionPoint = GameObject.Find ("PlatformDestructionPoint"); //find any object with this name



		
	}
	
	void Update () {

		if (transform.position.x < platfromDestructionPoint.transform.position.x) {

			//Destroy (gameObject);	//destroy on which script it is attach to


			gameObject.SetActive (false);

		}
		
	}
}
