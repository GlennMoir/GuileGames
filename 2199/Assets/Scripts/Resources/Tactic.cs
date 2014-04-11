using UnityEngine;
using System.Collections;

public class Tactic : MonoBehaviour {

	public int ID;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Activate(Player player)
	{
		switch (ID)
		{
		case 0: // Rotate my Board
			player.Rotate();
			break;
		case 1: // Add 2 Attack to my Active Minion
			player.getActiveMinion().GetComponent<Minion>().modifyDamage(2);
			break;
		case 2: // Add 2 Health to my Active Minion
			player.getActiveMinion().GetComponent<Minion>().modifyHealth(2);
			break;
		}
	}

	public IEnumerator moveObject (Vector3 pointB)
	{
		float timeSpeed = 2.0f;
		float time = 1.0f;
		float i = 0.0f;
		float rate = 1.0f/time;
		
		while (i<1.0){
			yield return null;
			i += timeSpeed * Time.deltaTime * rate;
			transform.position = Vector3.Lerp (transform.position, pointB, i);
		}
	}
}
