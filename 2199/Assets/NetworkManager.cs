using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		MasterServer.ipAddress = "localhost";
		MasterServer.port = 23466;
		MasterServer.RequestHostList ("2199_Luke");
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			HostData[] data = MasterServer.PollHostList();
			if (data.Length >= 1){
				Network.Connect (data [0]);
				Debug.Log("Server Connected");
				Debug.Log (data);
			}
		} else if (Input.GetKeyDown (KeyCode.C)) {
			GameObject.FindGameObjectWithTag("gameserver").GetComponent<GameServer>().CreateServer();
		} else if (Input.GetKeyDown (KeyCode.E)) {
			networkView.RPC("ServerRotate", RPCMode.Server, GameObject.Find("Player Me").GetComponent<Player>().getNum());
		}
	}

	[RPC] void setPlayerNumber(int number, int opponent)
	{
		GameObject.Find("Player Me").GetComponent<Player>().setNum(number);
		GameObject.Find("Player Opponent").GetComponent<Player>().setNum(opponent);
		Debug.Log("Player Numbers Set");
	}

	[RPC] void Rotate(int player)
	{
		GameObject.FindGameObjectWithTag("player_" + player).GetComponent<Player>().Rotate();
		Debug.Log("Boards rotated");
	}
}
