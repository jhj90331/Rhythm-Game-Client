using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;

namespace DummyClient
{
    public class LoginPacketProcess : PacketProcess
    {

        private Network network;
        private LoginSceneManager loginSceneManager;
        public TestConnect testConnect;

        private void Start()
        {
            testConnect.loginPacketProcess = this;
            loginSceneManager = new LoginSceneManager(testConnect);
        }

        public void Initialize()
        {
            network = NetworkManager.Instance.findNetwork("LoginNetwork");

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
                case PacketType.E_S_ANS_ID_PW_FAIL:
                    loginSceneManager.S_ANS_ID_PW_FAIL(packet);

                    return;

                case PacketType.E_S_ANS_ID_PW_SUCCESS:
                    loginSceneManager.S_ANS_ID_PW_SUCCESS(packet);

                    return;
            }

            Debug.Log("error Process");
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
