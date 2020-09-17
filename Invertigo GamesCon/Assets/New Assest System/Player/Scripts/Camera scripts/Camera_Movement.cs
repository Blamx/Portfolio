using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
	public float Camera_Speed = 3.0f; //Speed the camera will move with WASD
	public float Camera_Sprint_Mult = 2.5f; //How much faster the camer moves when sprinting
	public float Camera_Rotation_Sensitivity = 2.0f; //How much the camera rotates with the mouse

	public Vector3 Camera_Up = Vector3.up; //what the camera uses as up when calculation rotation
	public float Camera_Max_Y = 90.0f;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		//Camera Orientation===============================================================================
		Vector2 Camera_Rotation = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); //get input
		transform.RotateAround(transform.position, Camera_Up, Camera_Rotation.x * Camera_Rotation_Sensitivity); //rotate on x
		transform.RotateAround(transform.position, transform.right, -Camera_Rotation.y * Camera_Rotation_Sensitivity); //rotate on y

		Debug.DrawRay(transform.position, transform.forward * 100.0f, Color.white);//forward
		Debug.DrawRay(transform.position, Camera_Up * 3.0f, Color.red);//camera up

		//Camera Speed=====================================================================================
		float Speed_Mod = 1.0f;
		if (Input.GetKey(KeyCode.LeftShift))
		{
			Speed_Mod = Camera_Sprint_Mult;
		}
		else
		{
			Speed_Mod = 1.0f;
		}

		//Camera Movement==================================================================================
		if (Input.GetKey(KeyCode.W))
		{
			transform.position += (transform.forward * Camera_Speed * Time.deltaTime) * Speed_Mod;
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.position -= (transform.forward * Camera_Speed * Time.deltaTime) * Speed_Mod;
		}
		//------------------------------------
		if (Input.GetKey(KeyCode.A))
		{
			transform.position -= (transform.right * Camera_Speed * Time.deltaTime) * Speed_Mod;
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.position += (transform.right * Camera_Speed * Time.deltaTime) * Speed_Mod;
		}
		//------------------------------------
		if (Input.GetKey(KeyCode.Space))
		{
			transform.position += (Vector3.up * Camera_Speed * Time.deltaTime) * Speed_Mod;
		}
		if (Input.GetKey(KeyCode.LeftControl))
		{
			transform.position -= (Vector3.up * Camera_Speed * Time.deltaTime) * Speed_Mod;
		}

		//Camera Flip======================================================================================
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit rayOutData;
			bool rayHit = Physics.Raycast(transform.position, transform.forward, out rayOutData, 100.00f);
			if (rayHit)
			{
				Vector3 forward = transform.forward; //store where looking
				transform.up = rayOutData.normal; //rotate camera so up = new up
				Camera_Up = transform.up; //store for use next frame
				transform.LookAt(transform.position + forward, transform.up); //look at where was looking before rotation
			}
		}
	}
}