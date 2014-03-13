using UnityEngine;
using System.Collections;

public class Slot_Object : MonoBehaviour {

	public Vector3 slotPos = new Vector3 (0,0,0);

	// Use this for initialization
	void Start () {
		slotPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

	}
}