using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DummyClient
{
	public class GameSceneManager
	{
		public GameConnect connect;
		public GameManager_sc gameManager;

		public GameSceneManager(GameConnect connect_)
		{
			connect = connect_;
		}

		
		public void E_C_REQ_GAME_NAME(PacketInterface rowpacket)
		{
			PK_C_REQ_GAME_NAME packet = (PK_C_REQ_GAME_NAME)rowpacket;
			Debug.Log("GAME START Packet Received");
			gameManager.setGameStart();

		}

		public void E_C_REQ_ENEMY_NOTEHIT(PacketInterface rowpacket)
		{
			// Debug.Log("E_C_REQ_ENEMY_NOTEHIT 수신");
			PK_C_REQ_ENEMY_NOTEHIT packet = (PK_C_REQ_ENEMY_NOTEHIT)rowpacket;
			
			gameManager.EnemyNoteHit(packet.keycode_, packet.enemyJudge_);
		}

		public void E_S_ANS_GAME_CONNECT(PacketInterface rowpacket)
		{
			Debug.Log("GameSceneManager - GAME_CONNECT Packet Recevied");
			PK_S_ANS_GAME_CONNECT packet = (PK_S_ANS_GAME_CONNECT)rowpacket;
			connect.SceneChange(packet.enemySessionID_);
		}

	}

}