using UnityEngine;
using System.Collections;

public class Slot_Object : MonoBehaviour {

	public Vector3 slotPos = new Vector3 (0,0,0);

	//private GameObject slotTag;

	public Game_Controller gameController;

	public Unit_Class card;

	// Use this for initialization
	void Start () {

		slotPos = this.transform.position;
		//slotTag = this.tag;

		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_Controller>();
	}

	// Update is called once per frame
	void Update () {

	}

	void onTriggerEnter(Unit_Class other){
		if(other == card){
			slotPos.z = 1;
			other.transform.position = slotPos;
			if(this.tag == "Slot_1"){
				gameController.slotsPlayer1[0] = other;
			} else if(this.tag == "Slot_2"){
				gameController.slotsPlayer1[1] = other;
			} else if(this.tag == "Slot_3"){
				gameController.slotsPlayer1[2] = other;
			} else if(this.tag == "Slot_4"){
				gameController.slotsPlayer1[3] = other;
			} 
		}
	}
}