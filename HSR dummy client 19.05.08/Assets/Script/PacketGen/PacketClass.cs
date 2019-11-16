using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
//using System.Data;
using System.IO;
using System.Collections;
using UnityEngine;

namespace DummyClient
{
    public class PK_C_REQ_EXIT : PacketData, PacketInterface
    {
        Int64 PacketInterface.type() { return (Int64) PacketType.E_C_REQ_EXIT; }
        Int64 type() { return (Int64) PacketType.E_C_REQ_EXIT; }
        void PacketInterface.encode()
        {
            PacketUtil.encodeHeader(packet_, this.type());
        }
        void PacketInterface.decode(byte[] packet, ref int offset)
        { 
        }
        MemoryStream PacketInterface.getStream()
        {
            return packet_;
        }
    }
    public class PK_S_ANS_EXIT : PacketData, PacketInterface
    {
        Int64 PacketInterface.type() { return (Int64) PacketType.E_S_ANS_EXIT; }
        Int64 type() { return (Int64) PacketType.E_S_ANS_EXIT; }
        void PacketInterface.encode()
        {
            PacketUtil.encodeHeader(packet_, this.type());
        }
        void PacketInterface.decode(byte[] packet, ref int offset)
        { 
        }
        MemoryStream PacketInterface.getStream()
        {
            return packet_;
        }
    }
    public class PK_I_NOTIFY_TERMINAL : PacketData, PacketInterface
    {
        Int64 PacketInterface.type() { return (Int64) PacketType.E_I_NOTIFY_TERMINAL; }
        Int64 type() { return (Int64) PacketType.E_I_NOTIFY_TERMINAL; }
        void PacketInterface.encode()
        {
            PacketUtil.encodeHeader(packet_, this.type());
        }
        void PacketInterface.decode(byte[] packet, ref int offset)
        { 
        }
        MemoryStream PacketInterface.getStream()
        {
            return packet_;
        }
    }
    public class PK_C_NOTIFY_HEARTBEAT : PacketData, PacketInterface
    {
        Int64 PacketInterface.type() { return (Int64) PacketType.E_C_NOTIFY_HEARTBEAT; }
        Int64 type() { return (Int64) PacketType.E_C_NOTIFY_HEARTBEAT; }
        void PacketInterface.encode()
        {
            PacketUtil.encodeHeader(packet_, this.type());
        }
        void PacketInterface.decode(byte[] packet, ref int offset)
        { 
        }
        MemoryStream PacketInterface.getStream()
        {
            return packet_;
        }
    }
    public class PK_C_REQ_ID_PW : PacketData, PacketInterface
    {
        Int64 PacketInterface.type() { return (Int64) PacketType.E_C_REQ_ID_PW; }
        Int64 type() { return (Int64) PacketType.E_C_REQ_ID_PW; }
        public string         id_;
        public string         password_;

        void PacketInterface.encode()
        {
            PacketUtil.encodeHeader(packet_, this.type());
            PacketUtil.encode(packet_, id_);
            PacketUtil.encode(packet_, password_);
        }
        void PacketInterface.decode(byte[] packet, ref int offset)
        { 
            id_ = PacketUtil.decodestring(packet, ref offset);
            password_ = PacketUtil.decodestring(packet, ref offset);
        }
        MemoryStream PacketInterface.getStream()
        {
            return packet_;
        }
    }
    public class PK_S_ANS_ID_PW_FAIL : PacketData, PacketInterface
    {
        Int64 PacketInterface.type() { return (Int64) PacketType.E_S_ANS_ID_PW_FAIL; }
        Int64 type() { return (Int64) PacketType.E_S_ANS_ID_PW_FAIL; }
        void PacketInterface.encode()
        {
            PacketUtil.encodeHeader(packet_, this.type());
        }
        void PacketInterface.decode(byte[] packet, ref int offset)
        { 
        }
        MemoryStream PacketInterface.getStream()
        {
            return packet_;
        }
    }
    public class PK_S_ANS_ID_PW_SUCCESS : PacketData, PacketInterface
    {
        Int64 PacketInterface.type() { return (Int64) PacketType.E_S_ANS_ID_PW_SUCCESS; }
        Int64 type() { return (Int64) PacketType.E_S_ANS_ID_PW_SUCCESS; }
        public string         ip_;
        public UInt32         port_;
        public string         name_;

        void PacketInterface.encode()
        {
            PacketUtil.encodeHeader(packet_, this.type());
            PacketUtil.encode(packet_, ip_);
            PacketUtil.encode(packet_, port_);
            PacketUtil.encode(packet_, name_);
        }
        void PacketInterface.decode(byte[] packet, ref int offset)
        { 
            ip_ = PacketUtil.decodestring(packet, ref offset);
            port_ = PacketUtil.decodeUInt32(packet, ref offset);
            name_ = PacketUtil.decodestring(packet, ref offset);
        }
        MemoryStream PacketInterface.getStream()
        {
            return packet_;
        }
    }
    public class PK_I_DB_REQ_ID_PW : PacketData, PacketInterface
    {
        Int64 PacketInterface.type() { return (Int64) PacketType.E_I_DB_REQ_ID_PW; }
        Int64 type() { return (Int64) PacketType.E_I_DB_REQ_ID_PW; }
        public UInt64         clientId_;
        public string         id_;
        public string         password_;

        void PacketInterface.encode()
        {
            PacketUtil.encodeHeader(packet_, this.type());
            PacketUtil.encode(packet_, clientId_);
            PacketUtil.encode(packet_, id_);
            PacketUtil.encode(packet_, password_);
        }
        void PacketInterface.decode(byte[] packet, ref int offset)
        { 
            clientId_ = PacketUtil.decodeUInt64(packet, ref offset);
            id_ = PacketUtil.decodestring(packet, ref offset);
            password_ = PacketUtil.decodestring(packet, ref offset);
        }
        MemoryStream PacketInterface.getStream()
        {
            return packet_;
        }
    }
    public class PK_I_DB_ANS_ID_PW : PacketData, PacketInterface
    {
        Int64 PacketInterface.type() { return (Int64) PacketType.E_I_DB_ANS_ID_PW; }
        Int64 type() { return (Int64) PacketType.E_I_DB_ANS_ID_PW; }
        public UInt64         clientId_;
        public UInt64         oidAccountId_;
        public Byte         result_;

        void PacketInterface.encode()
        {
            PacketUtil.encodeHeader(packet_, this.type());
            PacketUtil.encode(packet_, clientId_);
            PacketUtil.encode(packet_, oidAccountId_);
            PacketUtil.encode(packet_, result_);
        }
        void PacketInterface.decode(byte[] packet, ref int offset)
        { 
            clientId_ = PacketUtil.decodeUInt64(packet, ref offset);
            oidAccountId_ = PacketUtil.decodeUInt64(packet, ref offset);
            result_ = PacketUtil.decodeByte(packet, ref offset);
        }
        MemoryStream PacketInterface.getStream()
        {
            return packet_;
        }
    }
    public class PK_I_CHTTING_NOTIFY_ID : PacketData, PacketInterface
    {
        Int64 PacketInterface.type() { return (Int64) PacketType.E_I_CHTTING_NOTIFY_ID; }
        Int64 type() { return (Int64) PacketType.E_I_CHTTING_NOTIFY_ID; }
        public UInt64         clientId_;
        public UInt64         oidAccountId_;

        void PacketInterface.encode()
        {
            PacketUtil.encodeHeader(packet_, this.type());
            PacketUtil.encode(packet_, clientId_);
            PacketUtil.encode(packet_, oidAccountId_);
        }
        void PacketInterface.decode(byte[] packet, ref int offset)
        { 
            clientId_ = PacketUtil.decodeUInt64(packet, ref offset);
            oidAccountId_ = PacketUtil.decodeUInt64(packet, ref offset);
        }
        MemoryStream PacketInterface.getStream()
        {
            return packet_;
        }
    }
    public class PK_I_DB_REQ_LOAD_DATA : PacketData, PacketInterface
    {
        Int64 PacketInterface.type() { return (Int64) PacketType.E_I_DB_REQ_LOAD_DATA; }
        Int64 type() { return (Int64) PacketType.E_I_DB_REQ_LOAD_DATA; }
        public UInt64         clientId_;
        public UInt64         oidAccountId_;

        void PacketInterface.encode()
        {
            PacketUtil.encodeHeader(packet_, this.type());
            PacketUtil.encode(packet_, clientId_);
            PacketUtil.encode(packet_, oidAccountId_);
        }
        void PacketInterface.decode(byte[] packet, ref int offset)
        { 
            clientId_ = PacketUtil.decodeUInt64(packet, ref offset);
            oidAccountId_ = PacketUtil.decodeUInt64(packet, ref offset);
        }
        MemoryStream PacketInterface.getStream()
        {
            return packet_;
        }
    }
    public class PK_I_DB_ANS_PARSE_DATA : PacketData, PacketInterface
    {
        Int64 PacketInterface.type() { return (Int64) PacketType.E_I_DB_ANS_PARSE_DATA; }
        Int64 type() { return (Int64) PacketType.E_I_DB_ANS_PARSE_DATA; }
        public UInt64         clientId_;
        public string         name_;
        public Byte         result_;

        void PacketInterface.encode()
        {
            PacketUtil.encodeHeader(packet_, this.type());
            PacketUtil.encode(packet_, clientId_);
            PacketUtil.encode(packet_, name_);
            PacketUtil.encode(packet_, result_);
        }
        void PacketInterface.decode(byte[] packet, ref int offset)
        { 
            clientId_ = PacketUtil.decodeUInt64(packet, ref offset);
            name_ = PacketUtil.decodestring(packet, ref offset);
            result_ = PacketUtil.decodeByte(packet, ref offset);
        }
        MemoryStream PacketInterface.getStream()
        {
            return packet_;
        }
    }
    public class PK_I_LOGIN_NOTIFY_ID_LOADED : PacketData, PacketInterface
    {
        Int64 PacketInterface.type() { return (Int64) PacketType.E_I_LOGIN_NOTIFY_ID_LOADED; }
        Int64 type() { return (Int64) PacketType.E_I_LOGIN_NOTIFY_ID_LOADED; }
        public UInt64         clientId_;
        public string         name_;
        public Byte         result_;

        void PacketInterface.encode()
        {
            PacketUtil.encodeHeader(packet_, this.type());
            PacketUtil.encode(packet_, clientId_);
            PacketUtil.encode(packet_, name_);
            PacketUtil.encode(packet_, result_);
        }
        void PacketInterface.decode(byte[] packet, ref int offset)
        { 
            clientId_ = PacketUtil.decodeUInt64(packet, ref offset);
            name_ = PacketUtil.decodestring(packet, ref offset);
            result_ = PacketUtil.decodeByte(packet, ref offset);
        }
        MemoryStream PacketInterface.getStream()
        {
            return packet_;
        }
    }
    public class PK_C_REQ_REGIST_CHATTING_NAME : PacketData, PacketInterface
    {
        Int64 PacketInterface.type() { return (Int64) PacketType.E_C_REQ_REGIST_CHATTING_NAME; }
        Int64 type() { return (Int64) PacketType.E_C_REQ_REGIST_CHATTING_NAME; }
        public string         name_;

        void PacketInterface.encode()
        {
            PacketUtil.encodeHeader(packet_, this.type());
            PacketUtil.encode(packet_, name_);
        }
        void PacketInterface.decode(byte[] packet, ref int offset)
        { 
            name_ = PacketUtil.decodestring(packet, ref offset);
        }
        MemoryStream PacketInterface.getStream()
        {
            return packet_;
        }
    }
    public class PK_C_REQ_CHATTING : PacketData, PacketInterface
    {
        Int64 PacketInterface.type() { return (Int64) PacketType.E_C_REQ_CHATTING; }
        Int64 type() { return (Int64) PacketType.E_C_REQ_CHATTING; }
        public string         text_;

        void PacketInterface.encode()
        {
            PacketUtil.encodeHeader(packet_, this.type());
            PacketUtil.encode(packet_, text_);
        }
        void PacketInterface.decode(byte[] packet, ref int offset)
        { 
            text_ = PacketUtil.decodestring(packet, ref offset);
        }
        MemoryStream PacketInterface.getStream()
        {
            return packet_;
        }
    }
    public class PK_S_ANS_CHATTING : PacketData, PacketInterface
    {
        Int64 PacketInterface.type() { return (Int64) PacketType.E_S_ANS_CHATTING; }
        Int64 type() { return (Int64) PacketType.E_S_ANS_CHATTING; }
        public string         name_;
        public string         text_;

        void PacketInterface.encode()
        {
            PacketUtil.encodeHeader(packet_, this.type());
            PacketUtil.encode(packet_, name_);
            PacketUtil.encode(packet_, text_);
        }
        void PacketInterface.decode(byte[] packet, ref int offset)
        { 
            name_ = PacketUtil.decodestring(packet, ref offset);
            text_ = PacketUtil.decodestring(packet, ref offset);
        }
        MemoryStream PacketInterface.getStream()
        {
            return packet_;
        }
    }
	
	// 여기까지 기존 패킷

	public class PK_C_REQ_MOVING : PacketData, PacketInterface
	{
		Int64 PacketInterface.type() { return (Int64)PacketType.E_C_REQ_MOVING; }
		Int64 type() { return (Int64)PacketType.E_C_REQ_MOVING; }

		public float posX_;
		public float posY_;
		public float posZ_;

		void PacketInterface.encode()
		{
			PacketUtil.encodeHeader(packet_, this.type());
			PacketUtil.encode(packet_, posX_);
			PacketUtil.encode(packet_, posY_);
			PacketUtil.encode(packet_, posZ_);
		}

		void PacketInterface.decode(byte[] packet, ref int offset)
		{
			posX_ = PacketUtil.decodefloat(packet, ref offset);
			posY_ = PacketUtil.decodefloat(packet, ref offset);
			posZ_ = PacketUtil.decodefloat(packet, ref offset);
		}

		MemoryStream PacketInterface.getStream()
		{
			return packet_;
		}
	}

	public class PK_S_ANS_MOVING : PacketData, PacketInterface
	{
		Int64 PacketInterface.type() { return (Int64)PacketType.E_S_ANS_MOVING; }
		Int64 type() { return (Int64)PacketType.E_S_ANS_MOVING; }

		public float posX_;
		public float posY_;
		public float posZ_;

		void PacketInterface.encode()
		{
			PacketUtil.encodeHeader(packet_, this.type());
			PacketUtil.encode(packet_, posX_);
			PacketUtil.encode(packet_, posY_);
			PacketUtil.encode(packet_, posZ_);
		}

		void PacketInterface.decode(byte[] packet, ref int offset)
		{
			posX_ = PacketUtil.decodefloat(packet, ref offset);
			posY_ = PacketUtil.decodefloat(packet, ref offset);
			posZ_ = PacketUtil.decodefloat(packet, ref offset);
		}

		MemoryStream PacketInterface.getStream()
		{
			return packet_;
		}
	}

	public class PK_C_REQ_GAME_NAME : PacketData, PacketInterface
	{
		Int64 PacketInterface.type() { return (Int64)PacketType.E_C_REQ_GAME_NAME; }
		Int64 type() { return (Int64)PacketType.E_C_REQ_GAME_NAME; }

		public string name_;
		public UInt64 enemySessionID_;

		void PacketInterface.encode()
		{
			PacketUtil.encodeHeader(packet_, this.type());
			PacketUtil.encode(packet_, name_);
			PacketUtil.encode(packet_, enemySessionID_);
		}

		void PacketInterface.decode(byte[] packet, ref int offset)
		{
			name_ = PacketUtil.decodestring(packet, ref offset);
			enemySessionID_ = PacketUtil.decodeUInt64(packet, ref offset);
		}

		MemoryStream PacketInterface.getStream()
		{
			return packet_;
		}
	}

	public class PK_C_REQ_ENEMY_NOTEHIT : PacketData, PacketInterface
	{
		Int64 PacketInterface.type() { return (Int64)PacketType.E_C_REQ_ENEMY_NOTEHIT; }
		Int64 type() { return (Int64)PacketType.E_C_REQ_ENEMY_NOTEHIT; }

		public UInt32 keycode_;
		public UInt32 enemyJudge_;
		public UInt64 enemySessionID_;
		

		void PacketInterface.encode()
		{
			PacketUtil.encodeHeader(packet_, this.type());
			PacketUtil.encode(packet_, keycode_);
			PacketUtil.encode(packet_, enemyJudge_);
			PacketUtil.encode(packet_, enemySessionID_);
		}

		void PacketInterface.decode(byte[] packet, ref int offset)
		{
			keycode_ = PacketUtil.decodeUInt32(packet, ref offset);
			enemyJudge_ = PacketUtil.decodeUInt32(packet, ref offset);
			enemySessionID_ = PacketUtil.decodeUInt64(packet, ref offset);
		}

		MemoryStream PacketInterface.getStream()
		{
			return packet_;
		}
	}

	public class PK_C_REQ_GAME_CONNECT : PacketData, PacketInterface
	{
		Int64 PacketInterface.type() { return (Int64)PacketType.E_C_REQ_GAME_CONNECT; }
		Int64 type() { return (Int64)PacketType.E_C_REQ_GAME_CONNECT; }

		public string title_;

		void PacketInterface.encode()
		{
			PacketUtil.encodeHeader(packet_, this.type());
			PacketUtil.encode(packet_, title_);
		}

		void PacketInterface.decode(byte[] packet, ref int offset)
		{
			title_ = PacketUtil.decodestring(packet, ref offset);
		}

		MemoryStream PacketInterface.getStream()
		{
			return packet_;
		}
	}

	public class PK_S_ANS_GAME_CONNECT : PacketData, PacketInterface
	{
		Int64 PacketInterface.type() { return (Int64)PacketType.E_S_ANS_GAME_CONNECT; }
		Int64 type() { return (Int64)PacketType.E_S_ANS_GAME_CONNECT; }

		public string title_;
		public UInt64 enemySessionID_;

		void PacketInterface.encode()
		{
			PacketUtil.encodeHeader(packet_, this.type());
			PacketUtil.encode(packet_, title_);
			PacketUtil.encode(packet_, enemySessionID_);
		}

		void PacketInterface.decode(byte[] packet, ref int offset)
		{
			title_ = PacketUtil.decodestring(packet, ref offset);
			enemySessionID_ = PacketUtil.decodeUInt64(packet, ref offset);
		}

		MemoryStream PacketInterface.getStream()
		{
			return packet_;
		}
	}

	public class PK_C_REQ_GAME_CONNECT_CANCEL : PacketData, PacketInterface
	{
		Int64 PacketInterface.type() { return (Int64)PacketType.E_C_REQ_GAME_CONNECT_CANCEL; }
		Int64 type() { return (Int64)PacketType.E_C_REQ_GAME_CONNECT_CANCEL; }

		public string title_;

		void PacketInterface.encode()
		{
			PacketUtil.encodeHeader(packet_, this.type());
			PacketUtil.encode(packet_, title_);
		}

		void PacketInterface.decode(byte[] packet, ref int offset)
		{
			title_ = PacketUtil.decodestring(packet, ref offset);
		}

		MemoryStream PacketInterface.getStream()
		{
			return packet_;
		}
	}

	public class PK_C_REQ_EXIT_GAME : PacketData, PacketInterface
	{
		Int64 PacketInterface.type() { return (Int64)PacketType.E_C_REQ_EXIT_GAME; }
		Int64 type() { return (Int64)PacketType.E_C_REQ_EXIT_GAME; }
		

		void PacketInterface.encode()
		{
			PacketUtil.encodeHeader(packet_, this.type());
		}

		void PacketInterface.decode(byte[] packet, ref int offset)
		{
		}

		MemoryStream PacketInterface.getStream()
		{
			return packet_;
		}
	}

	
	
	////////////////////////////////////////
	//// 미완성 패킷  //////////////////////
	//////////////////////////////////////
	public class PK_C_REQ_ENEMY_KEY_PRESSED : PacketData, PacketInterface
	{
		Int64 PacketInterface.type() { return (Int64)PacketType.E_C_REQ_ENEMY_KEY_PRESSED; }
		Int64 type() { return (Int64)PacketType.E_C_REQ_ENEMY_KEY_PRESSED; }

		public UInt64 enemySessionID_;
		public UInt32 keycode_;
		public UInt32 keyPressed_;

		void PacketInterface.encode()
		{
			PacketUtil.encodeHeader(packet_, this.type());
		}

		void PacketInterface.decode(byte[] packet, ref int offset)
		{
		}

		MemoryStream PacketInterface.getStream()
		{
			return packet_;
		}
	}


































}
