using UnityEngine;
using System.Collections;

public class Broken_Steel_Weapons : MonoBehaviour {

	public int health = 1;
	public int damage = 4;

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
