using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System;

public class NetworkManager : MonoBehaviour {

	/* 
	 * Minion Card played = 'a'
	 * Minion Card updated = 'b'
	 * Combat command = 'c'
	 * Buff card played = 'd'
	 * Tactics card played = 'e'
	 */

	Socket client;

	float countdown = 1.0f;

	byte[] dataBuffer;

	// Use this for initialization
	void Awake () {
		//client = new TcpClient("localhost", 1234).Client;
	}

	void Update()
	{
		/*
		countdown -= Time.deltaTime;
		if (countdown >= 0.0){
			if (client.Available != 0)
			{
				byte[] data = new byte[client.Available];
				client.Receive(data);

				ArrayList listData = new ArrayList();
				listData.AddRange(data);

				parseData (data);
			}
			countdown = 1.0f;
		}
	}

	void parseData(byte[] data)
	{
		byte[] packet = new byte[5];

		byte[] remainder = new byte[data.Length];

		if(data.Length >= 5)
		{
			int i = 0;

			while (i <= data.Length)
			{
				if (i < 5)
				{
					packet[i] = data[i];
				}
				else
				{

				}
			}

		}
		else
		{

		}




		Player play1 = GameObject.FindGameObjectWithTag("player_0").GetComponent<Player>();
		Player play2 = GameObject.FindGameObjectWithTag("player_1").GetComponent<Player>();

		Player me = GameObject.Find("Player Me").GetComponent<Player>();
		Player opponent = GameObject.Find("Player Opponent").GetComponent<Player>();

		switch (Convert.ToChar(packet[0])){
		case 'a':
			// pull card ID out of data[1]
			// pull position out of data[2]

			opponent.playMinion(packet[1], packet[2]);

			break;
		case 'b':
			// pull player number out of data[1]
			// pull card position out of data[2]
			// pull data value out of data[3]
			// pull changed value out of data[4]

			GameObject.FindGameObjectWithTag("player_" + packet[1]).GetComponent<Player>().changeMinion(packet[2], packet[3], packet[4]);

			break;
		case 'c':
			// Perform end of turn combat.

			play1.Rotate();
			play2.Rotate();

			play1.resolveTactics();
			play2.resolveTactics();

			Minion active1 = play1.getActiveCard();
			Minion active2 = play2.getActiveCard();

			active1.modifyHealth(-active2.getDamage());
			active2.modifyHealth(-active1.getDamage());

			break;
		case 'd':
			// pull card id out of data[1]
			// pull position out of data[2]

			opponent.playBuff(packet[1], packet[2]);

			break;
		case 'e':
			// pull card id out of data[1]

			opponent.playTactics(packet[1]);

			break;
		case 'z':
			// this is the end of the packet, ready to move into the next.

			break;
		default:

			break;
		}
*/
	}

	public void sendUpdate(byte[] data)
	{
		client.Send(data);
	}
	
}