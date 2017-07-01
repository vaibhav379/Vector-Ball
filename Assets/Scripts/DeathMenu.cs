using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

	public string mainMenuLevel;
	public string playGameLevel;


	void start(){
	

	}

	public void RestartGame()
	{
		//FindObjectOfType<GameManager> ().Reset ();
		//AdManager.Instance.ShowVideo ();
		SceneManager.UnloadSceneAsync(playGameLevel);
		SceneManager.LoadScene (playGameLevel);
	}

	public void QuitToAMain(){
		
		//AdManager.Instance.ShowVideo ();  //uncomment this to show ads
		SceneManager.LoadScene (mainMenuLevel);
	}

}
