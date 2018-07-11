using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HSController : MonoBehaviour
{
	private static HSController instance6;
	
	public static HSController Instance
	{
	   	get { return instance6; }
	}
	void Awake() {
		DontDestroyOnLoad (gameObject);
	    if (instance6 == null)
	    	instance6 = this;
	    else if (instance6 != this) {
	    	Destroy (gameObject);
	    	return;
	    }
	}
	void Start(){
		startGetScores ();
		startPostScores ();
	}

	private string secretKey = "123456789";
	string addScoreURL = "lightadventure.bplaced.net/addscore.php?"; 
	string highscoreURL = "lightadventure.bplaced.net/display.php";

	//for testing
	public string name3;
	int score;


	public string[] onlineHighscore;

	public void startGetScores() {
		StartCoroutine(GetScores());
	}

	public void startPostScores() {	
		StartCoroutine(PostScores());
	}

	public void updateOnlineHighscoreData() {
		name3 = PlayerPrefs.GetString("playerName"); ;
		score = PlayerPrefs.GetInt("playerPoints");
    }

	public  string Md5Sum(string strToEncrypt) {
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);
		
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);
		
		string hashString = "";
		
		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}
		
		return hashString.PadLeft(32, '0');
	}
	
	IEnumerator PostScores() {
		updateOnlineHighscoreData ();
		string hash = Md5Sum(name3 + score + secretKey);
		string post_url = addScoreURL + "&name=" + WWW.EscapeURL (name3) + "&score=" + score+ "&hash=" + hash;
		WWW hs_post = new WWW("http://"+post_url);
		yield return hs_post;
		
		if (hs_post.error != null)
		{
			print("There was an error posting the high score: " + hs_post.error);
		}
	}

	IEnumerator GetScores() {
        Scrolllist.Instance.loading = true;
		WWW hs_get = new WWW("http://"+highscoreURL);

		yield return hs_get;
		
		if (hs_get.error != null)
		{
			//Debug.Log("There was an error getting the high score: " + hs_get.error);
		}
		else
		{
			string help = hs_get.text;
			onlineHighscore  = help.Split(";"[0]);
		}
		Scrolllist.Instance.loading = false;
		Scrolllist.Instance.getScrollEntrys ();
	}
	
}
