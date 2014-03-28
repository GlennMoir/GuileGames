using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	
	private string typeName = "UniqueGameNameGlenn";
	private string gameName = "RoomName";
	private HostData[] hostList;
	
	public GameObject minion1;
	public GameObject minion2;
	public GameObject minion3;
	public GameObject minion4;
	

	public GameObject minion1_p2;
	public GameObject minion2_p2;
	public GameObject minion3_p2;
	public GameObject minion4_p2;
	
	public Vector3 slotOnePosition = new Vector3(0,0,0);
	public Vector3 slotTwoPosition = new Vector3(0,0,0);
	public Vector3 slotThreePosition = new Vector3(0,0,0);
	public Vector3 slotFourPosition = new Vector3(0,0,0);
	
	public Vector3 slotOnePosition_p2 = new Vector3(0,0,0);
	public Vector3 slotTwoPosition_p2 = new Vector3(0,0,0);
	public Vector3 slotThreePosition_p2 = new Vector3(0,0,0);
	public Vector3 slotFourPosition_p2 = new Vector3(0,0,0);
	
	private GameObject slotOne;
	private GameObject slotTwo;
	private GameObject slotThree;
	private GameObject slotFour;
	
	private GameObject slotOne_p2;
	private GameObject slotTwo_p2;
	private GameObject slotThree_p2;
	private GameObject slotFour_p2;
 
	private void StartServer()
	{
	    Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
	    MasterServer.RegisterHost(typeName, gameName);
	}
	
	void OnServerInitialized()
	{
	    SpawnCards();
	}

	private void RefreshHostList()
	{
	    MasterServer.RequestHostList(typeName);
	}
	 
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
	    if (msEvent == MasterServerEvent.HostListReceived)
	        hostList = MasterServer.PollHostList();
	}
	
	private void JoinServer(HostData hostData)
	{
	    Network.Connect(hostData);
	}
	 
	void OnConnectedToServer()
	{
	    SpawnCards();
	}
	
	private void SpawnCards()
	{
		slotOne = GameObject.FindGameObjectWithTag("Slot_1");
		slotTwo = GameObject.FindGameObjectWithTag("Slot_2");
		slotThree = GameObject.FindGameObjectWithTag("Slot_3");
		slotFour = GameObject.FindGameObjectWithTag("Slot_4");
		
		slotOnePosition = slotOne.transform.position;
		slotTwoPosition = slotTwo.transform.position;
		slotThreePosition = slotThree.transform.position;
		slotFourPosition = slotFour.transform.position;
		
		slotOne_p2 = GameObject.FindGameObjectWithTag("Slot_1_p2");
		slotTwo_p2 = GameObject.FindGameObjectWithTag("Slot_2_p2");
		slotThree_p2 = GameObject.FindGameObjectWithTag("Slot_3_p2");
		slotFour_p2 = GameObject.FindGameObjectWithTag("Slot_4_p2");
		
		slotOnePosition_p2 = slotOne_p2.transform.position;
		slotTwoPosition_p2 = slotTwo_p2.transform.position;
		slotThreePosition_p2 = slotThree_p2.transform.position;
		slotFourPosition_p2 = slotFour_p2.transform.position;
		
		if(Network.isServer){
		    Network.Instantiate(minion1, slotOne.transform.position, Quaternion.identity, 0);
			Network.Instantiate(minion2, slotTwo.transform.position, Quaternion.identity, 0);
			Network.Instantiate(minion3, slotThree.transform.position, Quaternion.identity, 0);
			Network.Instantiate(minion4, slotFour.transform.position, Quaternion.identity, 0);
		}
		
		if(Network.isClient){
		    Network.Instantiate(minion1_p2, slotOne_p2.transform.position, Quaternion.identity, 0);
			Network.Instantiate(minion2_p2, slotTwo_p2.transform.position, Quaternion.identity, 0);
			Network.Instantiate(minion3_p2, slotThree_p2.transform.position, Quaternion.identity, 0);
			Network.Instantiate(minion4_p2, slotFour_p2.transform.position, Quaternion.identity, 0);
		}
	}
	
	void OnGUI()
	{
	    if (!Network.isClient && !Network.isServer)
	    {
	        if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
	            StartServer();
	 
	        if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
	            RefreshHostList();
	 
	        if (hostList != null)
	        {
	            for (int i = 0; i < hostList.Length; i++)
	            {
	                if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
	                    JoinServer(hostList[i]);
	            }
	        }
	    }
	}
	
}
