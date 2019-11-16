using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using UnityEngine.SceneManagement;
using System.Text;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace DummyClient
{

    public class TestConnect : MonoBehaviour
    {
        public Network network;
        public LoginPacketProcess loginPacketProcess;
        private string m_address = "127.0.0.1";

        private string text_ID = "test-01";

        private string text_PASSWORD = "1q2w3e";

		GameObject playerDataObj;

        public enum ClientState
        {
            ServerReady = 0,
            LoginState
            //ChattingState
        }

        private ClientState c_state;

        public void SetClientState(ClientState c_state_)
        {
            c_state = c_state_;
        }

		public void StateToLoginState()
		{
			network = new Network();
			
			
			if(network.connect("223.194.100.66", 9000) != false)
			{
				NetworkManager.Instance.addNetwork("LoginNetwork", network);

				loginPacketProcess.Initialize();


				SetClientState(ClientState.LoginState);
				Debug.Log("Login Server Connected!");
				GameObject.Find("StartButton").SetActive(false);
			}
			else
			{
				Debug.Log("Login Server Not Connected!");
			}
		}

        // Use this for initialization
        void Start()
        {
			Screen.SetResolution(720, 480, false);

			playerDataObj = GameObject.Find("PlayerData");
            c_state = ClientState.ServerReady;
        }

        private void OnGUI()
        {
            if (c_state == ClientState.ServerReady)
            {
                OnGUIServerReady();
            }
            else if(c_state == ClientState.LoginState)
            {
                OnGUILoginState();
            }
        }

        void OnGUIServerReady()
        {
			// -> UI 버튼(StateToLoginState)으로 대체

            // m_address = GUI.TextField(new Rect(120, 200, 200, 20), m_address);

            //if (GUI.Button(new Rect(120, 230, 150, 20), "Connect to server"))
            //{

            //    network = new Network();
            //    NetworkManager.Instance.addNetwork("LoginNetwork", network);
            //    network.connect("127.0.0.1", 9000);
            //    loginPacketProcess.Initialize();


            //    SetClientState(ClientState.LoginState);
            //}
        }

        void OnGUILoginState()
        {
            // ID 쓰는 부분 GUI
            text_ID = GUI.TextField(new Rect(120, 160, 200, 20), text_ID);

            // PASSWORD 쓰는 부분 GUI
            text_PASSWORD = GUI.TextField(new Rect(120, 200, 200, 20), text_PASSWORD);

            if (GUI.Button(new Rect(120, 240, 150, 20), "Send to server"))
            {
                PK_C_REQ_ID_PW packet = new PK_C_REQ_ID_PW();
                packet.id_ = text_ID;
                packet.password_ = text_PASSWORD;
                NetworkManager.Instance.findNetwork("LoginNetwork").sendPacket(packet);
			}
        }
		
		/*
        public PK_C_REQ_REGIST_CHATTING_NAME registPacket;

        public void MoveToChatScene(PK_C_REQ_REGIST_CHATTING_NAME packet_)
        {
            registPacket = packet_;			
			playerDataObj.GetComponent<playerData>().SetPlayerName(packet_.name_);

            DontDestroyOnLoad(this);
            SceneManager.LoadScene("Scenes/ChattingScene");
        }
		*/
		public void MoveToChatScene(PK_C_REQ_REGIST_CHATTING_NAME packet_)
		{
			playerDataObj.GetComponent<playerData>().SetPlayerName(packet_.name_);
			NetworkManager.Instance.deleteNetwork("LoginNetwork");
			SceneManager.LoadScene("Scenes/ChattingScene");
		}

	}
}