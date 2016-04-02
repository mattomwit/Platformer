using UnityEngine;
using System.Collections;

public class Effect_Wind : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag.Equals("Player") || other.tag.Equals("Enemy"))
		other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,400));
	}
}