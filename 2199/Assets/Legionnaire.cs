using UnityEngine;
using System.Collections;

public class Legionnaire : MonoBehaviour {

	public int health = 3;
	public int damage = 2;

	public Unit_Class card;

	// Use this for initialization
	void Start () {

		card = this.GetComponent<Unit_Class>();
	
	}
	
	// Update is called once per frame
	void Update () {

		card.defaultHealth = this.health;
		card.defaultDamage = this.damage;
		card.health = this.health;
		card.damage = this.damage;
	}
}
