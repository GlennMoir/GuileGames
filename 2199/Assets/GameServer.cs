using UnityEngine;
using System.Collections;

public class GameServer : MonoBehaviour {

	public void CreateServer()
	{
		Network.InitializeServer(32, 25002, !Network.HavePublicAddress());
		MasterServer.RegisterHost("2199_Luke", "Test Game");
		Debug.Log ("Server Started");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	[RPC] void ServerRotate(int number)
	{
		networkView.RPC ("Rotate", RPCMode.All, number);
	}


}
