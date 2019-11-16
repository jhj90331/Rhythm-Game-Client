using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;

namespace DummyClient
{

	public class GamePacketProcess : PacketProcess
	{
		private Network network;
		private GameSceneManager gameSceneManager;
		public GameConnect gameConnect;
		public GameManager_sc gameManager = null;

		private void Start()
		{
			gameConnect.gamePacketProcess = this;
			//gameManager = GameObject.Find("GameManager").GetComponent<GameManager_sc>();
			gameSceneManager = new GameSceneManager(gameConnect);
			
			Invoke("Initialize", 0.1f);
		}
		
		public void SetGameManager()
		{
			gameManager = GameObject.Find("GameManager").GetComponent<GameManager_sc>();
			gameSceneManager.gameManager = GameObject.Find("GameManager").GetComponent<GameManager_sc>();
		}
		
		public void Initialize()
		{
			network = NetworkManager.Instance.findNetwork("GameNetwork");
			if (network == null)
			{
				Debug.Log("GamePakcetProcess - GameNetwork is NULL");
			}
			StartCoroutine("queueProcess");
		}

		IEnumerator queueProcess()
		{
			while (true)
			{
				yield return null;
				while (network.recvPacketQueue.Count != 0)
				{
					run(network.recvPacketQueue.Dequeue());
					yield return null;
				}

			}

		}


		public override void run(PacketInterface packet)
		{
			PacketType type = (PacketType)packet.type();
			switch (type)
			{
				case PacketType.E_C_REQ_GAME_NAME:
					gameSceneManager.E_C_REQ_GAME_NAME(packet);
					return;

				case PacketType.E_C_REQ_ENEMY_NOTEHIT:
					gameSceneManager.E_C_REQ_ENEMY_NOTEHIT(packet);
					return;
				case PacketType.E_S_ANS_GAME_CONNECT: // Scene 이동
					// gameManager = GameObject.Find("GameManager").GetComponent<GameManager_sc>();
					gameSceneManager.E_S_ANS_GAME_CONNECT(packet);
					return;
			}

			if (base.defaultRun(packet) == false)
			{
#if DEBUG
				//MessageBox.Show("잘못된 패킷이 수신되었습니다 : " + type.ToString(), "error", MessageBoxButtons.OK);
				//Application.Exit();
#endif
			}
		}


	}

}