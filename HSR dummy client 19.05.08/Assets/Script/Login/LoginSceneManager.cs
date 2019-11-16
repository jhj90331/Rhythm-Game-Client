using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace DummyClient
{
    
    public class LoginSceneManager
    {
        public TestConnect connect;      
        public LoginSceneManager(TestConnect connect_)
        {
            connect = connect_;
        }

        public void S_ANS_ID_PW_FAIL(PacketInterface rowPacket)
        {
            Debug.Log("S_ANS_ID_PW_FAIL : 로그인 실패----");
        }

        public void S_ANS_ID_PW_SUCCESS(PacketInterface rowPacket)
        {
            Debug.Log("S_ANS_ID_PW_SUCCESS : 로그인 성공----");

            PK_S_ANS_ID_PW_SUCCESS packet = (PK_S_ANS_ID_PW_SUCCESS)rowPacket;
            
            PK_C_REQ_REGIST_CHATTING_NAME rPacket = new PK_C_REQ_REGIST_CHATTING_NAME();
            rPacket.name_ = packet.name_;

            connect.MoveToChatScene(rPacket);

            //SceneManager.LoadScene("Scenes/ChattingScene");
        }

        
    }

}
