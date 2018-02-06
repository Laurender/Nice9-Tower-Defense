using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GooglePlayGamesThing : MonoBehaviour {

	public GameObject achButton;

	// Use this for initialization
	void Start () 
	{
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder ().Build ();

		PlayGamesPlatform.DebugLogEnabled = true;

		PlayGamesPlatform.InitializeInstance (config);
		PlayGamesPlatform.Activate ();

		if (!PlayGamesPlatform.Instance.localUser.authenticated) 
		{
			PlayGamesPlatform.Instance.Authenticate (SignInCallback, false);
		}
		else 
		{
			PlayGamesPlatform.Instance.Authenticate (SignInCallback, true);
		}
			
		if (PlayerPrefs.GetInt ("EnemiesKilled") == null) 
		{
			PlayerPrefs.SetInt ("EnemiesKilled", 0);
		}
		if (PlayerPrefs.GetInt ("level1FullHealth") == null) 
		{
			PlayerPrefs.SetInt ("level1FullHealth", 0);
		}
		if (PlayerPrefs.GetInt ("level2FullHealth") == null) 
		{
			PlayerPrefs.SetInt ("level2FullHealth", 0);
		}
		if (PlayerPrefs.GetInt ("level3FullHealth") == null) 
		{
			PlayerPrefs.SetInt ("level3FullHealth", 0);
		}
		if (PlayerPrefs.GetInt ("level4FullHealth") == null) 
		{
			PlayerPrefs.SetInt ("level4FullHealth", 0);
		}
		if (PlayerPrefs.GetInt ("level5FullHealth") == null) 
		{
			PlayerPrefs.SetInt ("level5FullHealth", 0);
		}


	}
	
	// Update is called once per frame
	void Update () {
		achButton.SetActive (Social.localUser.authenticated);
	}

	public void SignInCallback(bool success)
	{
		if (success) 
		{
			Debug.Log ("Signed in!");
		} 
		else 
		{
			Debug.Log ("Sign-in failed...");
		}
	}

	public void ShowAchievements() {
		if (PlayGamesPlatform.Instance.localUser.authenticated) 
		{
			PlayGamesPlatform.Instance.ShowAchievementsUI ();
		}
	}
}
