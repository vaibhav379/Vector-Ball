﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {



	public Transform platformGenerator;
	private Vector3 platformStartPoint;

	public PlayerController thePlayer;
	private Vector3 playerStartPoint;

	private PlatfromDestroyer[] platformList;

	private ScoreManager theScoreManager;

	public DeathMenu thedeathscreen;



	// Use this for initialization
	void Start () {

		platformStartPoint = platformGenerator.position;
		playerStartPoint = thePlayer.transform.position;


		theScoreManager = FindObjectOfType<ScoreManager> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RestartGame()
	{
		//StartCoroutine ("RestartGameCo");
		theScoreManager.scoreIncreasing = false;

		thePlayer.gameObject.SetActive (false);
		thedeathscreen.gameObject.SetActive (true);


	}

	public void Reset(){
	
		thedeathscreen.gameObject.SetActive (false);
		platformList = FindObjectsOfType<PlatfromDestroyer> ();
		for (int i = 0; i < platformList.Length; i++) {

			platformList [i].gameObject.SetActive (false);

		}

		thePlayer.transform.position = playerStartPoint;
		platformGenerator.position = platformStartPoint;

		thePlayer.gameObject.SetActive(true);

		theScoreManager.scoreCount = 0;
		theScoreManager.scoreIncreasing = true;
	//	thePlayer.gravity = -10;
	
	}



	/*public IEnumerator RestartGameCo()
	{


		theScoreManager.scoreIncreasing = false;

		thePlayer.gameObject.SetActive (false);

		yield return new WaitForSeconds(0.5f);
		platformList = FindObjectsOfType<PlatfromDestroyer> ();
		for (int i = 0; i < platformList.Length; i++) {
				
			platformList [i].gameObject.SetActive (false);
		
		}

		thePlayer.transform.position = playerStartPoint;
		platformGenerator.position = platformStartPoint;

		thePlayer.gameObject.SetActive(true);

		theScoreManager.scoreCount = 0;
		theScoreManager.scoreIncreasing = true;

	}*/


}
