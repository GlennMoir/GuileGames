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
		Debug.Log (health);
		return (health <= 0);
	}

	public int getHealth()
	{
		return health;
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

	public int getDamage()
	{
		return -damage;
	}

	public IEnumerator moveObject (Vector3 pointB)
	{
		float timeSpeed = 2.0f;
		float time = 1.0f;
		float i = 0.0f;
		float rate = 1.0f/time;
		
		while (i < 1.0){
			yield return null;
			i += timeSpeed * Time.deltaTime * rate;
			transform.position = Vector3.Lerp (transform.position, pointB, i);
		}
	}
}
