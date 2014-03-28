using UnityEngine;
using System.Collections;

public class Game_Controller : MonoBehaviour {

	public GameObject[] selectedCardsPlayer1 = new GameObject[4];
	public GameObject[] cardsInPlayPlayer1 = new GameObject[4];
	public GameObject[] deckPlayer1 = new GameObject[4];
	public GameObject[] tacticCardsPlayer1 = new GameObject[4];
	public Unit_Class[] slotsPlayer1 = new Unit_Class[4];
	public GameObject selectedTacticCardPlayer1;

	public Unit_Class currentFriendlyUnit;
	public Unit_Class currentEnemyUnit;
	public Unit_Class currentEnemy;
	public Unit_Class currentFriendly;

	public Vector3 slotOnePosition = new Vector3(0,0,0);
	public Vector3 slotTwoPosition = new Vector3(0,0,0);
	public Vector3 slotThreePosition = new Vector3(0,0,0);
	public Vector3 slotFourPosition = new Vector3(0,0,0);

	public Unit_Class minion1Player1;
	public Unit_Class minion2Player1;
	public Unit_Class minion3Player1;
	public Unit_Class minion4Player1;

	private GameObject slotOne;
	private GameObject slotTwo;
	private GameObject slotThree;
	private GameObject slotFour;

	private GameObject Player1;
	
	//--------------------------------------------------

	public GameObject[] selectedCardsPlayer2 = new GameObject[4];
	public GameObject[] cardsInPlayPlayer2 = new GameObject[4];
	public GameObject[] deckPlayer2 = new GameObject[4];
	public GameObject[] tacticCardsPlayer2 = new GameObject[4];
	public Unit_Class[] slotsPlayer2 = new Unit_Class[4];
	public GameObject selectedTacticCardPlayer2;

	public Unit_Class currentFriendlyUnitPlayer2;
	public Unit_Class currentEnemyUnitPlayer2;
	public Unit_Class currentEnemyPlayer2;
	public Unit_Class currentFriendlyPlayer2;

	public Vector3 slotOnePosition_p2 = new Vector3(0,0,0);
	public Vector3 slotTwoPosition_p2 = new Vector3(0,0,0);
	public Vector3 slotThreePosition_p2 = new Vector3(0,0,0);
	public Vector3 slotFourPosition_p2 = new Vector3(0,0,0);

	public Unit_Class minion1Player2;
	public Unit_Class minion2Player2;
	public Unit_Class minion3Player2;
	public Unit_Class minion4Player2;

	private GameObject slotOne_p2;
	private GameObject slotTwo_p2;
	private GameObject slotThree_p2;
	private GameObject slotFour_p2;

	private GameObject Player2;
	


	// Use this for initialization
	void Start () {
		currentEnemy = getCurrentEnemyUnit().GetComponent<Unit_Class>();
		currentFriendly = getCurrentFriendlyUnit().GetComponent<Unit_Class>();


	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (player1.hero.health <= 0){
			gameOver(player2);
		} else if (player2.hero.health <= 0){
			gameOver(player1);
		}*/
	}
	/*
	 void setBoard(){


	}*/

	Unit_Class getCurrentFriendlyUnit(){
		if (Network.isServer){
			if (slotsPlayer1[0] == null){
				return null;
			}else 
			currentFriendlyUnit = slotsPlayer1[0];
			return currentFriendlyUnit;
		} else if (Network.isClient){
			if (slotsPlayer2[0] == null){
				return null;
			}else 
			currentFriendlyUnitPlayer2 = slotsPlayer2[0];
			return currentFriendlyUnitPlayer2;
		}else
			return null;
	}

	Unit_Class getCurrentEnemyUnit(){
		if (Network.isServer){
			currentEnemyUnit =  slotsPlayer2[0];
			return currentEnemyUnit;
		} else if (Network.isClient){
			currentEnemyUnitPlayer2 =  slotsPlayer1[0];
			return currentEnemyUnitPlayer2;
		} else
			return null;
	}

	void attack(){

		if (Network.isServer){
			currentFriendly.health = currentFriendly.health - currentEnemy.damage;
		} else if (Network.isClient){
			currentFriendly.health = currentFriendly.health - currentEnemy.damage;
		}
	}/*

	void shuffleTacticsDeck(){

	}

	void chosenTactics(){

	}

	void currentTactic(){

	}

	void chosenUnits(){

	}

	void gameOver(GameObject winner){

	}*/
}
