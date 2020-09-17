using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleRotate : MonoBehaviour
{

    public Vector3 speed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(speed * Time.deltaTime);

        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2);
        if (transform.position.y > -23.31f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
        }
    }
}
