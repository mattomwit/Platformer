using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public RectTransform hpBarTransform;

	// Use this for initialization
	void Start () {
		hpBarTransform = this.gameObject.GetComponent<RectTransform>();
	}


	// Update is called once per frame
	void Update () {
	
	}




}
