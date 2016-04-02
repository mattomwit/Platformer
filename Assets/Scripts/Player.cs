using UnityEngine;
using System.Collections;

public class Player : Character {
	private Rigidbody2D rb2D;
	private Animator anim;
	//reference to wind slash objects
	public GameObject windSlashParent;
	public GameObject windSlashObject;
	private Animator windSlashAnim;
	//wind attack delay values
	private float nextAttackTime=0;
	private float hitDelay= 1.5f;
	//melee attack delay values
	private float melee_hitDelay = 0.6f;
	private float nextMeleeAttackTime=0;
	public ParticleSystem speedBuff;

	// Use this for initialization
	void Awake () {
		health=20;
		characterAudioSource = this.GetComponent<AudioSource>();
		rb2D = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		windSlashAnim= windSlashObject.GetComponent<Animator>();
		//getting health player HealthBar from UI 
		greenHealth= GameObject.FindGameObjectWithTag("PlayerHealthBar").GetComponent<RectTransform>();
		maxHealth = health;
	}

	// Update is called once per frame
	void FixedUpdate () {
		movementFunction();
	}
	// attack made when E key is pressed 
	void movementFunction(){
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool("Ground",grounded);
		anim.SetFloat("vSpeed",rb2D.velocity.y);
//		if(grounded==false) return;					//this line makes it impossible to move in the air
		float move = Input.GetAxis("Horizontal");
		rb2D.velocity= new Vector2(move*maxSpeed,rb2D.velocity.y);
		anim.SetFloat("Speed", Mathf.Abs(move));
		//flip character to the other side
		if(move>0 &&  !facingRight)
			Flip();
		else if(move<0 && facingRight)
			Flip();
	}

	void windSlashAttack()
	{
		//check if delay time has elapsed
		if(Time.time >nextAttackTime){
			//check in which way the character is facing  and then trigger wind slash animation then set timer delay
			if(this.transform.localScale.x>0)
			{
				windSlashParent.transform.position = this.transform.position + new Vector3(-2,-1,0);
				windSlashParent.transform.localScale =new Vector2(-1,1);				
			}else 
			{
				windSlashParent.transform.position = this.transform.position + new Vector3(2,-1,0);
				windSlashParent.transform.localScale = new Vector2(1,1);	
			}
			windSlashParent.SetActive(true);
			windSlashAnim.SetTrigger("WindSlash");
			nextAttackTime = Time.time+hitDelay;
		}
	}
	void attackFunction()
	{
		if (Input.GetKey(KeyCode.E) && nextMeleeAttackTime<Time.time)
		{
			anim.SetTrigger("Hit");
			//I movet this method to be called from animation
		//	windSlashAttack();
			nextMeleeAttackTime = Time.time + melee_hitDelay;
		}
	}

	void jumpFunction()
	{
		if (grounded && Input.GetKey(KeyCode.Space))
		{
			anim.SetBool("Ground",false);
			playJumpAudio();
			rb2D.AddForce(new Vector2(0,jumpForce));
		}
	}

	void Update()
	{
		checkTooLowCondition();
		doubleSpeedTimer();
		attackFunction();
		jumpFunction();
	}

	//ok we will hard code it here I am going to create methods that give character bonuses like double speed, double jump or far attack
	public void doubleSpeed()
	{
		timeOfDeactivation= Time.time+timeOfDuration;
		doubleSpeedSet=true;
		speedBuff.gameObject.SetActive(true);
		maxSpeed = 2*maxSpeed;
	}
	public void endDoubleSpeed()
	{	
		doubleSpeedSet=false;
		maxSpeed=(int)(maxSpeed/2);
		speedBuff.gameObject.SetActive(false);
	}
	//it will be called in child class of player in update to check if double speed was activated
	public void doubleSpeedTimer()
	{
		if(doubleSpeedSet)
		if(Time.time>timeOfDeactivation)
			endDoubleSpeed();
	}
}
