using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject[] Minions = new GameObject[4];
	public string slot = "Slot_1_p2";

	public GameObject[] Tactics = new GameObject[3];
	public string tacticsSlot = "Tactic";

	public int player_number = -1;

	public GameObject[] slots = new GameObject[4];


	// Use this for initialization
	void Start () {
		GameObject temp = GameObject.FindGameObjectWithTag(slot);

		for (int i = 0; i < 4; i++)
		{
			Minions[i] = (GameObject) Instantiate(GameObject.Find("Minion"), slots[i].transform.position, slots[i].transform.rotation);
			Minions[i].name = "minion" + i;
		}

		setNum(player_number);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.E))
		{
			ResolveCombat();
		}
	}

	public void setNum(int number)
	{
		gameObject.tag = "player_" + number;
		player_number = number;
	}

	public int getNum()
	{
		return player_number;
	}

	public void ResolveCombat()
	{
		//resolveTactics();

		if(Minions[0].GetComponent<Minion>().modifyHealth(GameObject.FindGameObjectWithTag("player_" + (1-player_number)).GetComponent<Player>().getActiveMinion().GetComponent<Minion>().getDamage()))
		{
			Destroy(Minions[0]);
		}

		Rotate();
	}

	public void Rotate()
	{
		GameObject tempObject = Minions[0];

		for (int i = 3; i >= 0; i--)
		{
			if(Minions[i])	StartCoroutine(Minions[(i + 1) % 4].GetComponent<Minion>().moveObject(slots[i].transform.position));

			Minions[(i + 1) % 4] = Minions[i];
		}
		Minions[1] = tempObject;
	}

	public void resolveTactics()
	{
		if (Tactics[0])	Tactics[0].GetComponent<Tactic>().Activate(GetComponent<Player>());
		Destroy(Tactics[0]);
		Tactics[0] = Tactics[1];
		Tactics[1] = Tactics[2];
		Destroy(Tactics[2]);
	}

	public GameObject getActiveMinion()
	{
		return Minions[0];
	}

	public void playMinion(int cardID, int position)
	{

	}

	public void changeMinion(int position, int stat, int value)
	{

	}

	public void playBuff(int cardID, int position)
	{

	}

	public void playTactics(int cardID)
	{

	}

}