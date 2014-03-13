using UnityEngine;
using System.Collections;

public class Basic_Minion_Behaviour : MonoBehaviour {

	public Vector3 slotOnePosition = new Vector3(0,0,0);
	public Vector3 slotTwoPosition = new Vector3(0,0,0);
	public Vector3 slotThreePosition = new Vector3(0,0,0);
	public Vector3 slotFourPosition = new Vector3(0,0,0);
	
	private GameObject slotOne;
	private GameObject slotTwo;
	private GameObject slotThree;
	private GameObject slotFour;

	// Use this for initialization
	void Start () {

		slotOne = GameObject.FindGameObjectWithTag("Slot_1");
		slotTwo = GameObject.FindGameObjectWithTag("Slot_2");
		slotThree = GameObject.FindGameObjectWithTag("Slot_3");
		slotFour = GameObject.FindGameObjectWithTag("Slot_4");
	
	}
	
	// Update is called once per frame
	void Update () {

		slotOnePosition = slotOne.transform.position;
		slotTwoPosition = slotTwo.transform.position;
		slotThreePosition = slotThree.transform.position;
		slotFourPosition = slotFour.transform.position;

		if (this.transform.position == slotOnePosition){
			if (Input.GetKeyDown("e")){
				this.transform.position = slotTwoPosition;
			}
		}
		else if (this.transform.position == slotTwoPosition){
			if (Input.GetKeyDown("e")){
				this.transform.position = slotThreePosition;
			}
		}
		else if (this.transform.position == slotThreePosition){
			if (Input.GetKeyDown("e")){
				this.transform.position = slotFourPosition;
			}
		}
		else if (this.transform.position == slotFourPosition){
			if (Input.GetKeyDown("e")){
				this.transform.position = slotOnePosition;
			}
		}
	}
}