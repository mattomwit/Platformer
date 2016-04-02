using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {
	private static GameManager gameManager = null;
	public Text pointsText;
	public Text coinsText;
	public Text winLoseText;
	public GameObject winLosePanel;
	public List<GameObject> enemies = new List<GameObject>();

	private static int points = 0;
	private static int coins = 0;

	public AudioSource managerAudioSource;
	public AudioSource secondAudioSource;
	public AudioClip backgroundAudio;
	public AudioClip winAudio;
	public AudioClip louseAudio;
	public AudioClip coinPickUpEffect;

	// getter for gameManager static so there is only one instance of this class in entire game and can be referenced from every class in this namespace
	public static GameManager Manager
	{
		get 
		{
			return gameManager;
		}
	}
	void Awake()
	{
		//checking if there already is a gameManager in this scene
		createThisGameManager();
		//setting reference to this gameManagers child AudioSource
		managerAudioSource = this.GetComponent<AudioSource>();
		//play background music
		playClip(backgroundAudio,true);
	}

	public void playClip(AudioClip audio, bool isLooping)
	{
		managerAudioSource.clip = audio;
		managerAudioSource.loop = isLooping;
		managerAudioSource.Play();
	}
	//this method is added because first audio source is playing background music so it can't be used to play sounds
	public void playCoinClip(AudioClip audio, bool isLooping)
	{
		secondAudioSource.clip = audio;
		secondAudioSource.loop = isLooping;
		secondAudioSource.Play();
	}


	public void addPoints(int pts)
	{
		points+=pts;
		pointsText.text=points.ToString();
	}
	public void addCoins(int cns)
	{
		coins += cns;
		coinsText.text= coins.ToString();
		playCoinClip(coinPickUpEffect,false);
	}
	//allocating this as gameManager destroy if other GameManager exists
	void createThisGameManager()
	{
		if (gameManager!=null && gameManager!= this)
		{
			Destroy(this.gameObject);
			return;
		}else
		{
			gameManager = this;
		}
	}
	public void checkWinCondition()
	{
		if(enemies.Count==0)
		{
			gameWon();
		}
	}

	public void gameWon()
	{
		
		winLoseText.text = "Congratulations! You Win!";
		playClip(winAudio,false);
		winLosePanel.SetActive(true);
		Time.timeScale=0;//pauses the game
	}

	public void gameOver(){
		winLoseText.text = "I'm sorry for you. It is over. Game Over!";
		playClip(louseAudio,false);
		winLosePanel.SetActive(true);
		Time.timeScale=0;//pauses the game
	}

	public void replayGame(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Time.timeScale=1;
		points=0;
		coins =0;
	}
		
}
