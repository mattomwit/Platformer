using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {
	#region variables and initial declaration
	//movement variables
	public float maxSpeed =10f;
	public bool facingRight =false;
	public bool grounded = false;
	public Transform groundCheck;
	public float groundRadius =0.2f;
	public LayerMask whatIsGround;
	public int jumpForce =100;
	// stats and combat values
	public int health =15;
	public int maxHealth;
	public float percentageHealth;
	public int points;
	//character status and timers
	public bool doubleSpeedSet = false;
	public float timeOfDeactivation;
	public float timeOfDuration =5f;
	private bool colorIsRed=false;
	public RectTransform greenHealth;
	public bool isDead;
	//audio sources and files
	public AudioSource characterAudioSource;
	public AudioClip attackAudio;
	public AudioClip idleAudio;
	public AudioClip hurtAudio;
	public AudioClip jumpAudio;
	public AudioClip dieAudio;
	#endregion

	#region Audio methods
	public void playClip(AudioClip clip,bool isLooping)
	{
		characterAudioSource.clip=clip;
		characterAudioSource.loop=isLooping;
		characterAudioSource.Play();
	}
	//this method is called from code (Player class in update function after pressing space) later probably in enemy
	public void playJumpAudio()
	{
		playClip(jumpAudio,false);
	}
	//this method is called from animation
	public void playAttackAudio()
	{
		playClip(attackAudio,false);
	}
	//this method is called from animation
	public void playIdleAudio()
	{
		if(idleAudio !=null && characterAudioSource.clip!=idleAudio)
		playClip(idleAudio,true);
	}
	//this method is called from code (dealDamage method in Character class)
	public void playHurtAudio()
	{
		playClip(hurtAudio,false);
	}
	public void playDieAudio()
	{
		playClip(dieAudio,false);
	}
	#endregion

	//calculate percentage health of character before displaying it in healthbar
	public void calculateHealth()
	{
	// we calculate health percentage to do it we need to set maxHealth in subclass 
		percentageHealth=(float)health/(float)maxHealth;
		percentageHealth = (float)health/(float)maxHealth;
		// this has to pass percentageHealth to the RectTransform of UI player helthBar or enemy healthBar
		if(percentageHealth<0)
			greenHealth.localScale = ( new Vector3(0,1,1));
		else
			greenHealth.localScale = ( new Vector3(percentageHealth,1,1));
	}

	//activated when colliders of enemy or player and hit interact with each other
	// deals damage to character it is attached to
	// is called from different script by reference (from script Hit
	public void dealDamage(int dmg)
	{
		//this audio is played when character is damaged
		playHurtAudio();
		health -=dmg;
		calculateHealth();
		checkDeathCondition();
		blinkRed();
	}


	public void Destroy_Character()
	{
		GameManager.Manager.addPoints(points);
		if (this.tag =="Enemy"){
			GameManager.Manager.enemies.Remove(this.gameObject);
		} else if (this.tag =="Player"){
			GameManager.Manager.gameOver();
		}
		Destroy(this.gameObject);
		GameManager.Manager.checkWinCondition();
	}

	public void blinkRed()
	{
		changeColor();
		Invoke("changeColor",0.3f);
		Invoke("changeColor",0.6f);
		Invoke("changeColor",0.9f);
	}
	public void changeColor()
	{
		SpriteRenderer tempSprite = this.GetComponent<SpriteRenderer>();
		if (colorIsRed)
		{
			colorIsRed=false;
			tempSprite.color = Color.white;
		}else
		{
			colorIsRed = true;
			tempSprite.color = Color.red;
		}
	}

	public void checkDeathCondition()
	{	
		//characters life is below 0 or it falls of a cliff (I will later give every character 
		if(health<=0)
		{
			isDead = true;
			//set animation die if it is finished set deathAnimationPlayed to true else return and check again
			Animator anim = GetComponent<Animator>();
			anim.SetBool("Die",true);
		}
	}

	public void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale=theScale;
	}
	public void checkTooLowCondition()
	{
		if(this.gameObject.transform.position.y<-80)
			Destroy_Character();
	}


}
