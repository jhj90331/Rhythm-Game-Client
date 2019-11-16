using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// using DummyClient;

namespace DummyClient
{
    public class ChattingSceneManager
    {
        public ChattingConnect connect;
        public ChattingSceneManager(ChattingConnect connect_)
        {
            connect = connect_;
        }
        public void E_S_ANS_CHATTING(PacketInterface rowPacket)
        {
            PK_S_ANS_CHATTING packet = (PK_S_ANS_CHATTING)rowPacket;
            connect.PrintText(packet);
            
            // Debug.Log(packet.text_);
        }

		public void E_S_ANS_MOVING(PacketInterface rowPacket)
		{
			PK_S_ANS_MOVING packet = (PK_S_ANS_MOVING)rowPacket;
			connect.MoveThisPlayer(packet);
		}
    }

}