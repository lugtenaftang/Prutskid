using UnityEngine;
using System.Collections;

public class GroundCheckerScript : MonoBehaviour 
{
	public GameObject playah;
	public GameObject jumpLight;
	public GameObject jumpLightEnv;
	private bool isGrounded = false;
	public bool hasJumped = false;
	private float timeAtJump = 0f;
	public float jumpWindow = 0.5f;

	public void setHasJumped()
	{
		hasJumped = true;
	}

	public bool isAbleToJump()
	{
		if(hasJumped)
		{
			return false;
		}
		if(!isGrounded && (Time.time - timeAtJump) > jumpWindow)
		{
			return false;
		}
		return true;

	}

	void Start () 
	{

	}

	void OnTriggerStay2D(Collider2D coll) 
	{
		if(coll.gameObject.tag != "Ball")
		{
			isGrounded = true;
			timeAtJump = Time.time;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) 
	{
		if(coll.gameObject.tag != "Ball")
		{
			isGrounded = true;
			hasJumped = false;
			timeAtJump = Time.time;
		}
	}

	
	void OnTriggerExit2D(Collider2D coll)
	{
		if(coll.gameObject.tag != "Ball")
		{
			isGrounded = false;
		}
	}

	void Update () 
	{
		transform.position = playah.transform.position;
		if(isAbleToJump())
		{
			jumpLight.light.intensity = 3f;
			jumpLightEnv.light.intensity = 3f;
		}
		else
		{
			jumpLight.light.intensity = 0f;
			jumpLightEnv.light.intensity = 0f;
		}
	}
}





