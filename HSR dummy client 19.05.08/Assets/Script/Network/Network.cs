using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DummyClient
{
    internal enum NET_STATE
    {
        START,
        CONNECTED,
        DISCONNECT,
        DISCONNECTED,
    }


    public class Network
    {

       // private string m_address = "127.0.0.1";

        private NetworkStream stream_;

        private Thread readWorker_;
        private Thread heartBeatWorker_;
        //private Thread queueWorker_;

        private TcpClient client_;
        private NET_STATE state_ = NET_STATE.START;
        
        public Queue<PacketInterface> recvPacketQueue;

		private Byte[] recvBuffer;

		private int totalOffset;

		public uint serverPort;

		public Network()
        {
            recvPacketQueue = new Queue<PacketInterface>();
        }

        NET_STATE state()
        {
            return state_;
        }

        public void close()
        {
            state_ = NET_STATE.DISCONNECTED;

            stream_.Close();
            stream_.Flush();
            client_.Close();
            readWorker_.Abort();
            heartBeatWorker_.Abort();
			Debug.Log("Network Close ! Port : " + serverPort.ToString());
        }

        public bool connect(string ip, uint port)
        {
            client_ = new TcpClient();
            try {
				serverPort = port;
				client_.Connect(ip, Convert.ToInt32(port));

            }
            catch {
                Debug.Log("서버 연결 실패");
                return false;
            }
            state_ = NET_STATE.CONNECTED;

            stream_ = client_.GetStream();
			
            readWorker_ = new Thread(new ThreadStart(receive));
            readWorker_.Start();

            heartBeatWorker_ = new Thread(new ThreadStart(heartBeat));
          //  heartBeatWorker_.Start();

			recvBuffer = new Byte[client_.ReceiveBufferSize];
			totalOffset = 0;
			return true;
        }


        public void disConnect()
        {
            state_ = NET_STATE.DISCONNECT;
            PK_C_REQ_EXIT packet = new PK_C_REQ_EXIT();
            this.sendPacket(packet);
        }

        public void setPacketProcess(PacketProcess packetProcess)
        {
        }

        private bool isConnected()
        {
            return state_ == NET_STATE.CONNECTED ? true : false;
        }

		public void receive()
		{

			try
			{
				while (this.isConnected())
				{

					Byte[] packetByte = new Byte[client_.ReceiveBufferSize];

					Int32 offset = 0;
					Int32 readLen = stream_.Read(packetByte, offset, packetByte.Length);
					//readLen = transbytes
					
					//Debug.Log("stream read");
					Int32 packetLen = PacketUtil.decodePacketLen(packetByte, ref offset);
					// packetLen  sizeof(size_t) + datalen( type + data);
			
					while (readLen < packetLen)
					{
						Byte[] remainPacket = new Byte[client_.ReceiveBufferSize];
						Int32 remainLen = 0;
						remainLen = stream_.Read(remainPacket, 0, remainPacket.Length);
						Buffer.BlockCopy(remainPacket, 0, packetByte, readLen, remainLen);
						readLen += remainLen;
					}

					Buffer.BlockCopy(packetByte, 0, recvBuffer, totalOffset, readLen);
					totalOffset += readLen;


					//Debug.Log("first block copy");

					const int packetHeaderSize = sizeof(Int32);

					Int32 recvOffset = 0;
				
					while (totalOffset != 0)
					{
						if (totalOffset < packetHeaderSize)
							break;
		
						Int32 recvPacketLen = PacketUtil.decodePacketLen(recvBuffer, ref recvOffset);
						
						if (totalOffset < recvPacketLen)
							break;

						Byte[] packetData = new Byte[client_.ReceiveBufferSize];

						Buffer.BlockCopy(recvBuffer, 4, packetData, 0, recvPacketLen - 4);
	
						PacketInterface rowPacket = PacketUtil.packetAnalyzer(packetData);

						Debug.Log("패킷 수신" + rowPacket.ToString());
						if (rowPacket == null && this.isConnected())
						{
							Debug.Log("잘못된 패킷 수신");
						}

						recvPacketQueue.Enqueue(rowPacket);
						Buffer.BlockCopy(recvBuffer, recvPacketLen, recvBuffer, 0,
								client_.ReceiveBufferSize - recvPacketLen);
						totalOffset -= recvPacketLen;
					}

				}
				this.close();
			}
			catch (Exception e)
			{
				if (this.isConnected())
				{
					//MessageBox.Show("잘못된 처리 : " + e.ToString(), "error", MessageBoxButtons.OK);
					//Application.Exit();
				}
			}
		}

		public void sendPacket(PacketInterface packet)
        {
            try {
                packet.encode();
                MemoryStream packetBlock = new MemoryStream();

                Int32 packetLen = sizeof(Int32) + (Int32)packet.getStream().Length;

                Byte[] packetHeader = BitConverter.GetBytes(packetLen);                
               // PacketObfuscation.encodingHeader(ref packetHeader, (int)packetHeader.Length);
                packetBlock.Write(packetHeader, 0, (Int32)packetHeader.Length);

                Byte[] packetData = packet.getStream().ToArray();
               // PacketObfuscation.encodingData(ref packetData, (int)packetData.Length);
                packetBlock.Write(packetData, 0, (Int32)packetData.Length);

                Byte[] packetBytes = packetBlock.ToArray();
				//Debug.Log("SendPacket " + packetBlock.Length);
                stream_.Write(packetBytes, 0, (int) packetBlock.Length);
                stream_.Flush();

                packetBlock = null;
            }
            catch (Exception e) {
                if (this.isConnected()) {
                    //MessageBox.Show("잘못된 처리 : " + e.ToString(), "error", MessageBoxButtons.OK);
                    //Application.Exit();
                }
            }
        }

        private void heartBeat()
        {
            while (this.isConnected()) {
                PK_C_NOTIFY_HEARTBEAT heartBeatPacket = new PK_C_NOTIFY_HEARTBEAT();
                this.sendPacket(heartBeatPacket);
                Thread.Sleep(100000);
            }
        }
    }
}