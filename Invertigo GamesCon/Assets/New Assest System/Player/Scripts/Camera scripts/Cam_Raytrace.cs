using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Raytrace : MonoBehaviour
{

	public float rayDistance = 500.0f;//How far the ray travels before it stops checking

	public bool rayHit;//if a collision happens
	public RaycastHit rayOutData;//Place to store data of ray collision

    

    GameObject playerCamera;
    Cam_Rotation camRotation;

    GameObject ironSight;
    LineRenderer line = new LineRenderer();

    public LayerMask shotMask;

	//=====================================================================================================================================================
	void Start()
	{
       
        playerCamera = transform.gameObject;
        camRotation = playerCamera.GetComponent<Cam_Rotation>();
		//precautionary setting of data
		rayHit = false;
		rayOutData.point = new Vector3(0, 0, 0); 
		rayOutData.normal = new Vector3(0, 1, 0);
	}

	//=====================================================================================================================================================
	void Update()
    {        
        Vector3 cam_forwardVec = camRotation.cam_forwardVec;
		Debug.DrawRay(transform.position, cam_forwardVec * rayDistance, Color.white); //Draw where camera is looking      
       
        rayHit = Physics.Raycast(transform.position, cam_forwardVec, out rayOutData, rayDistance, shotMask);

      
		if (rayHit) //debug print
		{
			//Debug.DrawRay(rayOutData.point, rayOutData.normal, Color.cyan); //draw normal
		}
	}
}

