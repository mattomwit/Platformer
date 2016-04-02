using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public float interpVelocity;
	public float nimDistance;
	public float followDistnace;
	public Transform target;
	public Vector3 offset;
	Vector3 targetPos;
	// Use this for initialization
	void Start () {
		targetPos=transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(target)
		{
			Vector3 posNoz = transform.position;
			posNoz.z = target.position.z;
			Vector3 targetDirection = target.position -posNoz;
			interpVelocity = targetDirection.magnitude*5f;
			targetPos = transform.position +(targetDirection.normalized*interpVelocity*Time.fixedDeltaTime);
			transform.position = Vector3.Lerp(transform.position, targetPos+offset,0.25f);
		}
	}

}
