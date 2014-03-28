using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Minion[] Minions = new Minion[4];

	private int player_number = -1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Rotate()
	{

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
}