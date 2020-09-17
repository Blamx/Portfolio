using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Destroy(this.gameObject, 10f);
        //Observer.logList.Remove(this.gameObject.transform.parent.parent.parent.parent.parent.parent.parent.gameObject);
        //MatchLog.listCheck.Remove(this.gameObject.transform.parent.parent.parent.parent.parent.parent.parent.gameObject);
	}
}
