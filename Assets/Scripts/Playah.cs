using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Playah : MonoBehaviour 
{

	public GroundCheckerScript groundCheckerScript;  
	public WallCheckerScript wallCheckerScript;  
	public GameObject Startpunkt1;
	public GameObject Startpunkt2;
	public int level = 0;
	public UnityEngine.UI.Text winText; 
	public UnityEngine.UI.Text timeText;
	public UnityEngine.UI.Text highscoreText;
	public UnityEngine.UI.Text controlsText;
	private Vector3 startPosition; 
	private float timeAtStart;
	private float timeAtFinish;
	private float timeTaken;
	private bool canIMove;
	public float speed;
	public float jumpHeight;
	
	void Start () 
	{ 
		startPosition = transform.position;
		setTextActive(true);
		setCanIMove(false);
		winText.text = "Welcome!\nPress \"" + (level + 1).ToString() + "\" to start!";
		timeAtStart = Time.time;
		highscoreText.gameObject.SetActive(false);
	}

	private void setTextActive(bool tf)
	{
		winText.gameObject.SetActive(tf);
	}
	private void setCanIMove(bool tf)
	{
		canIMove = tf;
		timeText.gameObject.SetActive(tf);
	}
	private void stopBall()
	{
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.angularVelocity = 0f;
	}
	private void updateScore()
	{
		if((timeTaken < PlayerPrefs.GetFloat(level.ToString())) || (PlayerPrefs.GetFloat(level.ToString()) == 0f))
		{
			PlayerPrefs.SetFloat(level.ToString(), timeTaken);
		}
		highscoreText.text = "Level " + level + " highscore: " + PlayerPrefs.GetFloat(level.ToString()).ToString("0.##");
	}
	private void displayScore()
	{
		highscoreText.gameObject.SetActive(true);
		highscoreText.text = "Level " + level + " highscore: " + PlayerPrefs.GetFloat(level.ToString()).ToString("0.##");
	}


	


	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Win")
		{
			setTextActive(true);
			setCanIMove(false);
			timeAtFinish = Time.time;
			timeTaken = timeAtFinish - timeAtStart;
			if(level != 2)
			{
				winText.text = "You Won!\nPress \"" + (level + 1).ToString() + "\" to continue!\n Time taken: " + timeTaken.ToString("0.##") + " seconds.";
			}
			else
			{
				winText.text = "Congratulations!\nYou have completed the game in its current form!\nIt took you " + ((PlayerPrefs.GetFloat("1") + timeTaken).ToString("0.##")) + " seconds in total."; 
			}
			updateScore();
		}
	}


	
	void Update() 
	{
		rigidbody2D.AddForce(new Vector2(0f, -10f));

		if(Input.GetKey(KeyCode.RightArrow) && canIMove)
		{
			rigidbody2D.AddForce(new Vector2(speed, 0f));
		}
		if(Input.GetKey(KeyCode.LeftArrow) && canIMove)
		{
			rigidbody2D.AddForce(new Vector2(-speed, 0f));
		}
		if(Input.GetKeyDown(KeyCode.UpArrow) && canIMove)
		{
			if(groundCheckerScript.isAbleToJump())
			{
				groundCheckerScript.setHasJumped();
				rigidbody2D.AddForce(new Vector2(0f, jumpHeight));
			}
			else if(wallCheckerScript.isAbleToWallJump())
			{
				wallCheckerScript.setHasWallJumped();
				rigidbody2D.AddForce(new Vector2(0f, 1.2f * jumpHeight));
			}
		}
		if(Input.GetKey(KeyCode.DownArrow) && canIMove)
		{
			rigidbody2D.AddForce(new Vector2(0f, -30f));
		}
		if(Input.GetKeyDown(KeyCode.R))
		{
			transform.position = startPosition;
			stopBall();
			setTextActive(false);
			setCanIMove(true);
			timeAtStart = Time.time;
		}
		if(Input.GetKey(KeyCode.S))
		{
			PlayerPrefs.DeleteKey(level.ToString());
			displayScore();
		}
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			transform.position = Startpunkt1.transform.position;
			startPosition = transform.position;
			stopBall();
			level = 1;
			setTextActive(false);
			setCanIMove(true);
			timeAtStart = Time.time;
			displayScore();
		}
		if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			transform.position = Startpunkt2.transform.position;
			startPosition = transform.position;
			stopBall();
			level = 2;
			setTextActive(false);
			setCanIMove(true);
			timeAtStart = Time.time;
			displayScore();
		}

		if(Input.GetKeyDown(KeyCode.H))
		{
			controlsText.text = "Controls:\nLeft/Right arrows: horisontal movement.\nUp arrow if green or yellow light is on: jump.\nDown arrow: increase gravity.\nPress \"R\" to restart level.\nPress \"S\" to reset this level\'s highscore.\n\"Esc\" quits the game.";
		}
		if(Input.GetKeyUp(KeyCode.H))
		{
			controlsText.text = "Hold down \"H\" to view controls.";
		}

//		if(Input.GetKey(KeyCode.S))
//		{
//			stopBall();
//		}


		
		
		if(canIMove)
		{
			timeText.text = "Time: " + (Time.time - timeAtStart).ToString("0.##");
		}


		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}



	}
}
