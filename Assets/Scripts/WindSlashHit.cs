using UnityEngine;
using System.Collections;

public class WindSlashHit : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag.Equals("Enemy")){
			other.GetComponentInParent<Enemy>().dealDamage(Random.Range(1,4));
		}
	}
}
