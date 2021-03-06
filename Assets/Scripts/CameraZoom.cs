﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraZoom : MonoBehaviour 
{
	public GameObject playah;
	public GameObject cameraPoint;

	public UnityEngine.UI.Text camZoomText; 

	public float smoothTime = 1.5f;
	private float yVelocity = 0f;
	private Vector3 camVelocity = Vector3.zero;
	private float ballVelocityX;
	private float ballVelocityY;
	private float ballVelocity;
//	public float smoothingPole = 0.9f;
//	private Vector3 filter;

	void Start () 
	{

	}
	
	void Update () 
	{
		transform.position = Vector3.SmoothDamp (transform.position, cameraPoint.transform.position, ref camVelocity, smoothTime / 8f);

		ballVelocityX = playah.rigidbody2D.velocity.x;
		ballVelocityY = playah.rigidbody2D.velocity.y;

		if (ballVelocityX < 0f) 
		{
			ballVelocityX = ballVelocityX * (-1);
		}
		if (ballVelocityY < 0f) 
		{
			ballVelocityY = ballVelocityY * (-1);
		}
		ballVelocity = ballVelocityX + ballVelocityY; 

		float zoomTarget = ballVelocity / 3;
		camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, zoomTarget, ref yVelocity, smoothTime);

		if (camera.orthographicSize < 4f) 
		{
			camera.orthographicSize = 4f;
		}
		if (camera.orthographicSize > 7f) 
		{
			camera.orthographicSize = 7f;
		}

		camZoomText.text = "Camera Zoom  (please ignore): " + camera.orthographicSize.ToString("0.##");

//		float newX = playah.transform.position.x + smoothingPole * (transform.position.x - playah.transform.position.x);
//		float newY = playah.transform.position.y + smoothingPole * (transform.position.y - playah.transform.position.y);
//		
//		transform.position = new Vector3(newX, newY, transform.position.z);
		
	}
}
