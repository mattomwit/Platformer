using UnityEngine;
using System.Collections;

public class Enemy : Character {
	private Transform playerTransform; 	
	private Animator thisAnimator;
	private Rigidbody2D rb2D;
	private float distanceToTarget = 10f; //range at wihich the enemy will spot the target and start moving towards it

	void Awake () {
		//in this case it is time between attacks
		timeOfDuration=1.5f;
		points=20;
		characterAudioSource = this.GetComponent<AudioSource>();
		rb2D = GetComponent<Rigidbody2D>();
		//animator of enemy to animate enemy
		thisAnimator= GetComponent<Animator>();
		//set target for enemy to go towards
		playerTransform= GameObject.FindWithTag("Player").transform;
		maxSpeed = 5f;
		health = 5;
		maxHealth = health;
	}
	
	// Update is called once per frame
	void Update () {
		checkIfPlayerClose();
		checkTooLowCondition();
	}


	void checkIfPlayerClose()
	{
		try{
			//calculate distance to the enemy in float value
			float distance = playerTransform.position.x-transform.position.x;
			if(isDead==false)
			{
				//i'll use timeOfdeactivation to make timer for delay between attacks
				//if absolute value of distance between enemy and player is less than 2 make enemy play hit animation
				if (Mathf.Abs(distance)<8 && timeOfDeactivation<Time.time)
				{
					timeOfDeactivation = Time.time+timeOfDuration;
					thisAnimator.SetTrigger("Hit");
					//if distance is greater than declared do nothing
				}else if(Mathf.Abs(distance)>distanceToTarget)
				{
					return;
				}
				//in this case only distance absolute value is more than 2 and less then declared value 
				//we check if distance is more than 0 or less than 0
				else
				{
					if (distance>0)
					{
						thisAnimator.SetFloat("Speed", 1f);
						rb2D.velocity = new Vector2(maxSpeed, rb2D.velocity.y); 
					}
					else if (distance<0)
					{
						thisAnimator.SetFloat("Speed", 1f); 
						rb2D.velocity = new Vector2(-maxSpeed, rb2D.velocity.y);
					}
					if(distance>0 &&  !facingRight)
						Flip();
					else if(distance<0 && facingRight)
						Flip();
				}
			}
		}
		// Ignore the exception when player dies
		catch(System.Exception e){
			
		}
	}
}
