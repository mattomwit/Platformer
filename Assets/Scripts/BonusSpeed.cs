using UnityEngine;
using System.Collections;

public class BonusSpeed : MonoBehaviour {
	private int points = 5;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag.Equals("Player"))
		{
			other.GetComponent<Player>().doubleSpeed();
			pickedUp();			
		}
	}

	private void pickedUp()
	{
		GameManager.Manager.addPoints(points);
		this.gameObject.SetActive(false);
	}

}
