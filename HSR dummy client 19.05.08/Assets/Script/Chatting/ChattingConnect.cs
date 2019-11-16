using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace DummyClient
{

    public class ChattingConnect : MonoBehaviour
    {
        public Network network;
        public ChattingPacketProcess chattingPacketProcess;
        //private TestConnect testconnect;
        string chatStr;
		
		[HideInInspector] public string chatName;
		//public GameObject player;

        private List<string> m_message;

        private float KADO_SIZE = 16.0f;
        private float FONT_SIZE = 13.0f;
        private float FONG_HEIGHT = 18.0f;

		public enum chattingSceneStatus
		{
			chatting = 0,
			gamming,
			matching
		}

		public chattingSceneStatus chatStatus;

		GameObject playerDataObj; 
		// Use this for initialization
		void Start()
        {
            //testconnect = GameObject.Find("LoginConnect").GetComponent<TestConnect>();
            playerDataObj = GameObject.Find("PlayerData");

            ChattingInit();
			
			GameObject.Find("Background Music").GetComponent<TitleBackgroundMusic>().StartBGM();
			
			
        }

		private void ChattingInit()
		{

			chatStatus = chattingSceneStatus.chatting;

			chatStr = "Chat Text";
			m_message = new List<string>();

			if (NetworkManager.Instance.findNetwork("ChattingNetwork") != null)
				Destroy(this.gameObject);



			if (NetworkManager.Instance.findNetwork("ChattingNetwork") == null)
			{
				network = new Network();

				NetworkManager.Instance.addNetwork("ChattingNetwork", network);
				network.connect("223.194.100.66", 9200);


				PK_C_REQ_REGIST_CHATTING_NAME rPacket = new PK_C_REQ_REGIST_CHATTING_NAME();//testconnect.registPacket;
				rPacket.name_ = playerDataObj.GetComponent<playerData>().GetPlayerName();

				NetworkManager.Instance.findNetwork("ChattingNetwork").sendPacket(rPacket);
				chattingPacketProcess.Initialize();
				chatName = rPacket.name_;
			}
			//NetworkManager.Instance.findNetwork("LoginNetwork").close();
			//NetworkManager.Instance.deleteNetwork("LoginNetwork");
			//Destroy(GameObject.Find("LoginConnect"));   // TODO : 로그인 커넥트 정상 종료 시키기

		}

		private void Update()
		{
			//player.transform.Translate(new Vector3(0.03f, 0.0f, 0.0f));
			if (chatStatus == chattingSceneStatus.chatting)
			{
				//if (Input.GetKey(KeyCode.Space))
				//{
				//	AskServerMove();
				//}
			}

		}

		private void AskServerMove()
		{
			// 채팅씬에서 패킷 잘 보내지는지 확인했던 패킷. 지금은 안씀
			//PK_C_REQ_MOVING packet = new PK_C_REQ_MOVING();
			//packet.posX_ = player.transform.position.x;
			//packet.posY_ = player.transform.position.y;
			//packet.posZ_ = player.transform.position.z;

			//NetworkManager.Instance.findNetwork("ChattingNetwork").sendPacket(packet);
		}

		public void MoveThisPlayer(PK_S_ANS_MOVING packet)
		{
			//player.transform.position = new Vector3(packet.posX_, packet.posY_, packet.posZ_);
			//Debug.Log("Moving (" + packet.posX_ + ", " + packet.posY_ + ", " + packet.posZ_ + ") ");
		}

		

        private void OnGUI()
        {
			if (chatStatus == chattingSceneStatus.chatting)
			{
				OnGUIChattingState();
			}



			if(chatStatus == chattingSceneStatus.matching)
			{
				OnGUIMatchingState();
			}
        }

        void OnGUIChattingState()
        {
            Vector2 position = new Vector2(200.0f, 100.0f);
            Vector2 size = new Vector2(200.0f, 200.0f);
            foreach (string s in m_message)
            {
                DrawText(s, position, size);
                position.y += FONG_HEIGHT;
            }

            chatStr = GUI.TextField(new Rect(120, 260, 200, 20), chatStr);

            if (GUI.Button(new Rect(120, 300, 150, 20), "Send to server"))
            {

                PK_C_REQ_CHATTING packet = new PK_C_REQ_CHATTING();
                packet.text_ = chatStr; 

                NetworkManager.Instance.findNetwork("ChattingNetwork").sendPacket(packet);
				
			}

			if (GUI.Button(new Rect(280, 300, 150, 20), "Game Matching Start"))
			{
				DontDestroyOnLoad(this.gameObject);
				chatStatus = chattingSceneStatus.matching;
				PK_C_REQ_GAME_CONNECT packet = new PK_C_REQ_GAME_CONNECT();
				packet.title_ = "test";
				NetworkManager.Instance.findNetwork("GameNetwork").sendPacket(packet);

				//NetworkManager.Instance.deleteNetwork("ChattingNetwork");
				//SceneManager.LoadScene("Scenes/GameScene");
			}



		}

		void OnGUIMatchingState()
		{
			if (GUI.Button(new Rect(120, 300, 150, 20), "Cancel GameMatching"))
			{
				PK_C_REQ_GAME_CONNECT_CANCEL packet = new PK_C_REQ_GAME_CONNECT_CANCEL();
				packet.title_ = "test";

				NetworkManager.Instance.findNetwork("GameNetwork").sendPacket(packet);

				chatStatus = chattingSceneStatus.chatting;
			}
		}


		void DrawText(string message, Vector2 position, Vector2 size)
        {
            if (message == "")
            {
                return;
            }

            GUIStyle style = new GUIStyle();
            style.fontSize = 16;
            style.normal.textColor = Color.black;

            Vector2 balloon_size, text_size;

            text_size.x = message.Length * FONT_SIZE;
            text_size.y = FONG_HEIGHT;

            balloon_size.x = text_size.x + KADO_SIZE * 2.0f;
            balloon_size.y = text_size.y + KADO_SIZE;

            Vector2 p;

            p.x = position.x - size.x / 2.0f + KADO_SIZE;
            p.y = position.y - size.y / 2.0f + KADO_SIZE;
            //p.x = position.x - text_size.x/2.0f;
            //p.y = position.y - text_size.y/2.0f;

            GUI.Label(new Rect(p.x, p.y, text_size.x, text_size.y), message, style);
        }

        public void PrintText(PK_S_ANS_CHATTING packet)
        {
            string packetText = packet.name_ + packet.text_;

            AddMessage(packetText);

            Debug.Log(packet.name_ +  packet.text_);
        }

        void AddMessage(string str)
        {
            while (m_message.Count >= 10)
            {
                m_message.RemoveAt(0);
            }

            m_message.Add(str);
        }

    }

}