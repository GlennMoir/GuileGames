using UnityEngine;
using System.Collections;

public class Minion : MonoBehaviour {

	public int health = -1;
	public int damage = -1;
	public int defaultHealth = -1;
	public int defaultDamage = -1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*
	 * Modify the Health of a minion
	 * params:
	 * 			amount: the amount and direction of change (positive or negative)
	 * 
	 * returns: bool: true for health remaining, false otherwise.
	 * 
	 */ 

	public bool modifyHealth(int amount)
	{
		health += amount;
		return true;
	}

	/*
	 * Modify the Damage of a minion
	 * params:
	 * 			amount: the amount and direction of change (positive or negative)
	 * 
	 * returns: bool: true for damage remaining, false otherwise.
	 * 
	 */ 
	public bool modifyDamage(int amount)
	{
		damage += amount;
		return true;
	}
}
