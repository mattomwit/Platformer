using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {
	float attackDelay = 0f;
	float timeTillNextAttack=0;

	void OnTriggerEnter2D(Collider2D other){
		if(Time.time>timeTillNextAttack){
			timeTillNextAttack+=attackDelay;
			if (other.tag.Equals("Enemy")){
				other.GetComponentInParent<Enemy>().dealDamage(Random.Range(1,4));
			}
			else if (other.tag.Equals("Player")){
				other.GetComponentInParent<Player>().dealDamage(Random.Range(1,4));
			}
		}
	}
}
