using UnityEngine;
using System.Collections;

public class WinGoal : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other)
	{
		//player reached the goal and we call gameWon from gameManager static instance
		if (other.tag.Equals("Player"))
		{
			GameManager.Manager.gameWon();
		}
	}
}
