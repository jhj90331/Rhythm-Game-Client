// using System.Windows.Forms;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DummyClient
{
    public abstract class PacketProcess : MonoBehaviour
    {
        public bool defaultRun(PacketInterface packet)
        {
            PacketType type = (PacketType)packet.type();
            //Todo : 공통 처리 패킷에 대한 정의
            //switch (type) {
            
            //}

            return false;
        }

        public abstract void run(PacketInterface packet);
    }
}