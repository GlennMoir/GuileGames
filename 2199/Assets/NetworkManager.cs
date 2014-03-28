using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MasterServer.ipAddress = "localhost";
		MasterServer.port = 23466;
		MasterServer.RequestHostList ("2199_Luke");
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) 
		{
			HostData[] data = MasterServer.PollHostList();
			if (data.Length >= 1)
			Network.Connect(data[0]);
			Debug.Log(data);
			Debug.Log("connected to the Server Hopefully");
		}
	}

	[RPC] void setPlayerNumber(int number, int opponent)
	{
		GameObject.FindGameObjectWithTag("player_me").tag = "player_" + number;
		GameObject.FindGameObjectWithTag("player_opponent").tag = "player_" + opponent;
		Debug.Log("Player Numbers Set");
	}

	[RPC] void Rotate(int player)
	{
		GameObject.FindGameObjectWithTag("player_" + player).GetComponent<Player>().Rotate();
		Debug.Log("Boards rotated");
	}
}
