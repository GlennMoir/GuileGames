using UnityEngine;
using System.Collections;

public class Basic_Minion_Behaviour : MonoBehaviour {

	public Vector3 slotOnePosition = new Vector3(0,0,0);
	public Vector3 slotTwoPosition = new Vector3(0,0,0);
	public Vector3 slotThreePosition = new Vector3(0,0,0);
	public Vector3 slotFourPosition = new Vector3(0,0,0);
	
	public Vector3 slotOnePosition_p2 = new Vector3(0,0,0);
	public Vector3 slotTwoPosition_p2 = new Vector3(0,0,0);
	public Vector3 slotThreePosition_p2 = new Vector3(0,0,0);
	public Vector3 slotFourPosition_p2 = new Vector3(0,0,0);
	
	private GameObject slotOne;
	private GameObject slotTwo;
	private GameObject slotThree;
	private GameObject slotFour;
	
	private GameObject slotOne_p2;
	private GameObject slotTwo_p2;
	private GameObject slotThree_p2;
	private GameObject slotFour_p2;
	
	private GameObject Player1;
	private GameObject Player2;
	

	// Use this for initialization
	void Start () {

		slotOne = GameObject.FindGameObjectWithTag("Slot_1");
		slotTwo = GameObject.FindGameObjectWithTag("Slot_2");
		slotThree = GameObject.FindGameObjectWithTag("Slot_3");
		slotFour = GameObject.FindGameObjectWithTag("Slot_4");
		
		slotOne_p2 = GameObject.FindGameObjectWithTag("Slot_1_p2");
		slotTwo_p2 = GameObject.FindGameObjectWithTag("Slot_2_p2");
		slotThree_p2 = GameObject.FindGameObjectWithTag("Slot_3_p2");
		slotFour_p2 = GameObject.FindGameObjectWithTag("Slot_4_p2");
		
		Player1 = GameObject.FindGameObjectWithTag("Player1");
		Player2 = GameObject.FindGameObjectWithTag("Player2");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void Movement(){
		
		
		if (Network.isServer){
			
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
		
		if(Network.isClient){
			
			slotOnePosition_p2 = slotOne_p2.transform.position;
			slotTwoPosition_p2 = slotTwo_p2.transform.position;
			slotThreePosition_p2 = slotThree_p2.transform.position;
			slotFourPosition_p2 = slotFour_p2.transform.position;
	
			if (this.transform.position == slotOnePosition_p2){
				if (Input.GetKeyDown("e")){
					this.transform.position = slotTwoPosition_p2;
				}
			}
			else if (this.transform.position == slotTwoPosition_p2){
				if (Input.GetKeyDown("e")){
					this.transform.position = slotThreePosition_p2;
				}
			}
			else if (this.transform.position == slotThreePosition_p2){
				if (Input.GetKeyDown("e")){
					this.transform.position = slotFourPosition_p2;
				}
			}
			else if (this.transform.position == slotFourPosition_p2){
				if (Input.GetKeyDown("e")){
					this.transform.position = slotOnePosition_p2;
				}
			}
		}
	}
}