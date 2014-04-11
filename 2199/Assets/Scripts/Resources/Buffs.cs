using UnityEngine;
using System.Collections;

public class Buffs : MonoBehaviour {

	public Vector3 mousePos = new Vector3(0,0,0);
	Transform buffTransfrom;
	public bool overPad;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		mousePos = Input.mousePosition;

		if (overPad == false){
			this.transform.position = new Vector3(mousePos.x, this.transform.position.y, mousePos.z);
		}
	}

	public void onTriggerEnter(GameObject other){
		GameObject[] opslots = GameObject.FindGameObjectsWithTag("Buff_Slot");
		GameObject[] opslotsP2 = GameObject.FindGameObjectsWithTag("Buff_Slot_p2");

		foreach (GameObject cur in opslots)
		{
			if ((other == cur) && (Input.GetMouseButtonDown(0))){
				this.transform.position = other.transform.position;
				overPad = true;
			}
		}
	}
}

	
