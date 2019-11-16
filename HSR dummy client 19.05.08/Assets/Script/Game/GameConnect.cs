using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace DummyClient
{

	public class GameConnect : MonoBehaviour
	{
		public Network network;
		public GamePacketProcess gamePacketProcess;
		private ChattingConnect chattingconnect;

		private string gameName;

		private void Start()
		{
			DontDestroyOnLoad(this.gameObject);
			//chattingconnect = GameObject.Find("ChattingConnect").GetComponent<ChattingConnect>();
			gameName = GameObject.Find("PlayerData").GetComponent<playerData>().GetPlayerName();	// 우아한 종료 처리 하기!
			GameInit();

		}

		void GameInit()
		{
			if (NetworkManager.Instance.findNetwork("GameNetwork") != null)
				Destroy(this.gameObject);

			if (NetworkManager.Instance.findNetwork("GameNetwork") == null)
			{
				network = new Network();

				NetworkManager.Instance.addNetwork("GameNetwork", network);
				network.connect("223.194.100.66", 9300);
			}

			if(NetworkManager.Instance.findNetwork("GameNetwork") == null)
			{
				Debug.Log("GameConnect - GameNetwork is NULL!");
			}
			

			//NetworkManager.Instance.deleteNetwork("ChattingNetwork");
			//Destroy(GameObject.Find("ChattingConnect"));

		}

		//private void OnGUI()
		//{
		//	if (GUI.Button(new Rect(120, 230, 150, 20), "Back to the Lobby"))
		//	{
		//		NetworkManager.Instance.deleteNetwork("GameNetwork");
		//		SceneManager.LoadScene("Scenes/ChattingScene");
		//	}
		//}

		private void Update()
		{
			//if(Input.GetKeyDown(KeyCode.A))
			//{
			//	PK_C_REQ_GAME_NAME packet = new PK_C_REQ_GAME_NAME();
			//	packet.name_ = gameName;

			//	NetworkManager.Instance.findNetwork("GameNetwork").sendPacket(packet);
			//}

		}

		public void SceneChange(ulong enemySessionID)
		{
			GameObject.Find("ChattingConnect").GetComponent<ChattingConnect>().chatStatus = ChattingConnect.chattingSceneStatus.gamming;
			GameObject.Find("PlayerData").GetComponent<playerData>().SetEnemySessionID(enemySessionID);
			GameObject.Find("Background Music").GetComponent<TitleBackgroundMusic>().StopBGM();
			SceneManager.LoadScene("Scenes/GameScene");

			Invoke("SendGameStartPacket", 3);
		}

		public void SendGameStartPacket()
		{
			PK_C_REQ_GAME_NAME packet = new PK_C_REQ_GAME_NAME();
			packet.name_ = gameName;
			packet.enemySessionID_ = GameObject.Find("PlayerData").GetComponent<playerData>().GetEnemySessionID();

			NetworkManager.Instance.findNetwork("GameNetwork").sendPacket(packet);
			Debug.Log("GameStart Packet SEND");
		}

	}
}		