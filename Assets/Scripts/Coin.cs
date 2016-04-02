using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
	private int points = 10;
	public bool triggerSet=false;

	void OnTriggerEnter2D(Collider2D other)
	{
		
		if (triggerSet==false && other.tag=="Player")
		{
			triggerSet=true;
			GameManager.Manager.addCoins(1);
			GameManager.Manager.addPoints(points);
			this.gameObject.SetActive(false);
		}
	}
}
