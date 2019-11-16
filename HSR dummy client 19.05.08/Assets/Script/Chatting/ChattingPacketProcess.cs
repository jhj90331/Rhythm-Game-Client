using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;

namespace DummyClient
{
    public class ChattingPacketProcess : PacketProcess
    {
        private Network network;
        private ChattingSceneManager chattingSceneManager;
        public ChattingConnect chattingConnect;


        private void Start()
        {
            chattingConnect.chattingPacketProcess = this;
            chattingSceneManager = new ChattingSceneManager(chattingConnect);
        }

        public void Initialize()
        {
            network = NetworkManager.Instance.findNetwork("ChattingNetwork");

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
                case PacketType.E_S_ANS_CHATTING:
                    chattingSceneManager.E_S_ANS_CHATTING(packet);
                    return;

				case PacketType.E_S_ANS_MOVING:
					chattingSceneManager.E_S_ANS_MOVING(packet);
					return;
            }

            if (base.defaultRun(packet) == false) {
#if DEBUG
                //MessageBox.Show("잘못된 패킷이 수신되었습니다 : " + type.ToString(), "error", MessageBoxButtons.OK);
                //Application.Exit();
#endif
            }
        }
    }
}
