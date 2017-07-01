   using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

	public GameObject thePlatform;		//the platform that going to be generated ahead
	public Transform generationPoint;	//the platformGenerationPonit
	public float distanceBetween;		//b/w the platforms

	private float platformWidth;

	public float distanceBetweenMin;
	public float distanceBetweenMax;

	public ObjectPooler[] theObejctPools;

	//public GameObject[] thePlatforms;
	private int platformSelector;
	private float[] platformWidths;


	private float minHeight;
	public Transform maxHeightPoint;
	private float maxHeight;
	public float maxHeightChange;
	private float heightChange;

	private CoinGenerator theCoinGenerator;

	public float randomCoinThreshold;


	void Start () {

		//platformWidth = thePlatform.GetComponent<BoxCollider2D> ().size.x; //when using only one platform width

		platformWidths = new float[theObejctPools.Length];

		for (int i = 0; i < theObejctPools.Length; i++) {

			platformWidths [i] = theObejctPools[i].pooledObject.GetComponent<BoxCollider2D> ().size.x;
		
		}


		minHeight = transform.position.y;
		maxHeight = maxHeightPoint.position.y;


		theCoinGenerator = FindObjectOfType<CoinGenerator> ();
	

	}
	
	void Update () {

		distanceBetween = Random.Range (distanceBetweenMin, distanceBetweenMax);			//if you have 2 numbers it will pick any random no of those two



		if (transform.position.x < generationPoint.position.x) {

			platformSelector = Random.Range (0, theObejctPools.Length); 


			heightChange = transform.position.y + Random.Range (maxHeightChange, -maxHeightChange);

			if (heightChange > maxHeight) {
			
				heightChange = maxHeight;
			} else if (heightChange < minHeight) {
			
				heightChange = minHeight;
			}



			transform.position = new Vector3 (transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, heightChange, transform.position.z);


			//Instantiate (/*thePlatform*/ thePlatforms[platformSelector], transform.position, transform.rotation);		 //creates the copy of the object

			GameObject newPlatform = theObejctPools[platformSelector ].getPooledObject ();

			newPlatform.transform.position = transform.position;
			newPlatform.transform.rotation = transform.rotation;
			newPlatform.SetActive (true);


			if (Random.Range (0f, 100f) < randomCoinThreshold) {
				theCoinGenerator.SpawnCoins (new Vector3 (transform.position.x, transform.position.y + 1f, transform.position.z));
			}

			transform.position = new Vector3 (transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);

			 
		}
	

	}
}
