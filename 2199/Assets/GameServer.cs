using UnityEngine;
using System.Collections;

public class GameServer : MonoBehaviour {

	public void CreateServer()
	{
		Network.InitializeServer(32, 25002, !Network.HavePublicAddress());
		MasterServer.RegisterHost("2199_Luke", "Test Game");
		Debug.Log ("Server Started");
		Network.AllocateViewID();
	}

	// Use this for initialization
	void Start () {
		ArrayList test = new ArrayList();

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnPlayerConnected(NetworkPlayer player) {
 		Debug.Log(player.ipAddress);
		networkView.RPC ("setPlayerNumber", player, 0, 2);
	}

	[RPC]
	void Rotate(int number)
	{
		networkView.RPC ("Rotate", RPCMode.All, 0);
	}

	[RPC] void setPlayerNumber(int number, int opponent) {}
}
