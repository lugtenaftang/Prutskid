using UnityEngine;
using System.Collections;

public class WallCheckerScript : MonoBehaviour 

{
	public GroundCheckerScript groundCheckerScript;
	public GameObject playah;
	public GameObject jumpLight;
	public GameObject jumpLight2;
	public GameObject jumpLightEnv;
	private bool isGrounded = false;
	private bool hasJumped = false;
	private float timeAtJump = 0f;
	public float jumpWindow = 0.5f;

	public void setHasWallJumped()
	{
		hasJumped = true;
	}

	public bool isAbleToWallJump()
	{
		if(groundCheckerScript.hasJumped)
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
		else
		{
			return false;
		}


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
		if(isAbleToWallJump())
		{
			jumpLight.light.intensity = 6f;
			jumpLight2.light.intensity = 6f;
			jumpLightEnv.light.intensity = 3f;
		}
		else
		{
			jumpLight.light.intensity = 0f;
			jumpLight2.light.intensity = 0f;
			jumpLightEnv.light.intensity = 0f;
		}
	}
}





