using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Aperture : MonoBehaviour
{
	float distance = 0;
	bool closed = false;
	float velocity = 0;

	float timer = 0;
	static float timeToDoorOpen = 50.0f;
	static float timeToDoorClose = 12.0f;

	public float speed = 0.1f;
	public float maxDistance = 20.0f;
	public bool startsClosed = false;

	public static bool gatesClosed = false;

	// Use this for initialization
	void Start()
	{

		closed = startsClosed;
		gatesClosed = closed;
		if (!startsClosed)
		{
			distance = -maxDistance;
			transform.Translate(new Vector3(distance, 0, 0), Space.Self);
		}
	}

	// Update is called once per frame
	void Update()
	{
		timer += Time.deltaTime;
		if (closed && (timer >= timeToDoorOpen))
		{
			timer = 0;

			//open door
			velocity = -speed;
			closed = !closed;
			gatesClosed = closed;
		}
		else if (!closed && (timer >= timeToDoorClose))
		{
			timer = 0;

			//close door
			velocity = speed;
			closed = !closed;
			gatesClosed = closed;
		}

        if (Input.GetKeyDown("o")) // save current position to an arry
        {
            if (closed)
            {
                velocity = -speed;
            }
            else
            {
                velocity = speed;
            }
            closed = !closed;
            gatesClosed = closed;
        }

        //transform.localPosition = new Vector3(transform.localPosition.x + velocity, transform.localPosition.y, transform.localPosition.z);
        //transform.localPosition += transform.forward * velocity; //new Vector3(velocty, 0, 0);

        transform.Translate(new Vector3(velocity, 0, 0), Space.Self);
		distance += velocity;

		if (distance <= -maxDistance || distance >= 0)
		{
			velocity = 0;
		}
	}
}
