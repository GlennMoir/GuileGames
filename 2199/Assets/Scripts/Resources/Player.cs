using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject[] Minions = new GameObject[4];
	public GameObject[] reserveMinions = new GameObject[2];
	public string slot = "Slot_1_p2";

	public GameObject[] BuffSlots = new GameObject[4];

	public GameObject[] Tactics = new GameObject[3];
	public string tacticsSlot = "Tactic";

	public int player_number = -1;
	public int leaderHealth = 10;

	public GameObject[] slots = new GameObject[5];


	// Use this for initialization
	// added reserve minions array
	void Start () {
		GameObject temp = GameObject.FindGameObjectWithTag(slot);

		Screen.showCursor = true;

		for (int i = 0; i < 4; i++){
			Debug.Log (Resources.Load("Minion_" + i));
			Minions[i] = (GameObject) Instantiate(Resources.Load("Minion_" + i), slots[i].transform.position, slots[i].transform.rotation);
			Minions[i].name = "minion" + i;
		}

		reserveMinions[0] = (GameObject) Instantiate(Resources.Load("Minion_4"), slots[4].transform.position, slots[4].transform.rotation);
		reserveMinions[0].name = "minion" + 4;
		reserveMinions[1] = (GameObject) Instantiate(Resources.Load("Minion_5"), slots[4].transform.position, slots[4].transform.rotation);
		reserveMinions[1].name = "minion" + 5;

		setNum(player_number);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.E)){
			ResolveCombat();
		}
	}

	public void setNum(int number){
		gameObject.tag = "player_" + number;
		player_number = number;
	}

	public int getNum(){
		return player_number;
	}

	public void ResolveCombat(){
		// if there are active minions in both attack slots
		resolveTactics();
		//   <----------------my minion---------------->	<--------------------------enemy minion-------------------------->
		if (Minions[0] && GameObject.FindGameObjectWithTag("player_" + (1-player_number)).GetComponent<Player>().getActiveMinion()){
		//		<------------my minions health------------>	    <--------------------------------------------modified by enemy minions damage-------------------------------------------->
			if(Minions[0].GetComponent<Minion>().modifyHealth(GameObject.FindGameObjectWithTag("player_" + (1-player_number)).GetComponent<Player>().getActiveMinion().GetComponent<Minion>().getDamage())){
				Destroy(Minions[0]);
				if (reserveMinions[0] != null){
					GameObject temp = reserveMinions[0];
					Minions[0] = temp;
					cycleReserve();				
				}
			}
		//if facing empty spot, attack faction leader health directly
		} else if (attackEmpty()){
			//if (this.GetComponent<Player>().modifyLeaderHealth(GameObject.FindGameObjectWithTag("player_" + (1-player_number)).GetComponent<Player>().getActiveMinion().GetComponent<Minion>().getDamage()))
				//print ("game is over");
		}
		if (minionsleft())
			Rotate();
	}

	//checks if minions are still on board
	bool minionsleft (){
		if (Minions[0] && !(Minions[1]) && !(Minions[2])  && !(Minions[3])){
			return false;
		} else {
			return true;
		}
	}
	//checks if player is attacking empty spot
	bool attackEmpty (){
		return(Minions[0] && !(GameObject.FindGameObjectWithTag("player_" + (1-player_number)).GetComponent<Player>().getActiveMinion()));
	}

	//when card is removed to be put into main queue, the next card in the reserve pile is moved ahead
	void cycleReserve(){
		if (reserveMinions[1] == null && reserveMinions[0] == null){
			return;
		} else if (reserveMinions[1] != null){
			GameObject tempRes = reserveMinions[1];
			reserveMinions[0] = tempRes;
			reserveMinions[1] = null;
		} else if (reserveMinions[1] == null && reserveMinions[0] != null){
			reserveMinions[0] = null;
		}
	}

	//changed if statements in for loop
	public void Rotate(){
		Debug.Log (Minions[0]);
		GameObject tempObject = Minions[0];

		for (int i = 3; i >= 0; i--){
			if(Minions[(i+1) % 4])	StartCoroutine(Minions[(i + 1) % 4].GetComponent<Minion>().moveObject(slots[(i+2) % 4].transform.position));
			Minions[(i + 1) % 4] = Minions[i];
		}
		Minions[1] = tempObject;
	}

	public void resolveTactics(){
		if (Tactics[0])	Tactics[0].GetComponent<Tactic>().Activate(GetComponent<Player>());
			Destroy(Tactics[0]);
			Tactics[0] = Tactics[1];
			Tactics[1] = Tactics[2];
			Destroy(Tactics[2]);
	}

	public bool modifyLeaderHealth(int amount){
		leaderHealth += amount;
		Debug.Log ("leader health: " + leaderHealth);
		return (leaderHealth <= 0);
	}

	public GameObject getActiveMinion(){
		return Minions[0];
	}

	public void playMinion(int cardID, int position){

	}

	public void changeMinion(int position, int stat, int value){

	}
	/*
	public void playBuff(int cardID, int position){
		for (int i = 0; i < 0; i++){

		}
	}*/

	public void playTactics(int cardID){

	}
}