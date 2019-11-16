using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NetworkManager : SingletonBase<NetworkManager>
{
    // public List<DummyClient.Network> networkList_ = new List<DummyClient.Network>();
    public Dictionary<string, DummyClient.Network> networkDic_ = new Dictionary<string, DummyClient.Network>();
    

    //네트워크의 사이즈 리턴
    public int size()
    {
        return networkDic_.Count;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    //비었는지 확인
    public bool isEmpty()
    {
        if(networkDic_.Count == 0)
        {
            return true;
        }
        return false;
    }

    //네트워크 넣어줌
    public void addNetwork(string serverName, DummyClient.Network network)
    {
        DummyClient.Network tempNetwork = null;
        if(networkDic_.TryGetValue(serverName, out tempNetwork))
        {
            Debug.Log("add : 같은 네트워크가 존재합니다.");
            return;
        }
        networkDic_.Add(serverName, network);
    }

    //네트워크 없애줌
    public bool deleteNetwork(string serverName)
    {
        DummyClient.Network tempNetwork = null;
        if (!networkDic_.TryGetValue(serverName, out tempNetwork))
        {
            Debug.Log("delete : 네트워크가 존재하지 않음.");
            return false;
        }
        tempNetwork.close();
        networkDic_.Remove(serverName);
        return true;
    }

    public DummyClient.Network findNetwork(string serverName)
    {
        DummyClient.Network tempNetwork = null;
        if (networkDic_.TryGetValue(serverName, out tempNetwork))
        {
            return tempNetwork;
        }

        return null;
    }

    // Use this for initialization
    //public DummyClient.Network network;// = new DummyClient.Network();


    public override void OnDestroy()
    {
        // TODO : 다른 네트워크로 연결시켜주는 것이 완료 됐을때 닫거나 변경,
        if(NetworkManager.Instance.findNetwork("LoginNetwork") != null )
            NetworkManager.Instance.findNetwork("LoginNetwork").close();

        if (NetworkManager.Instance.findNetwork("ChattingNetwork") != null)
            NetworkManager.Instance.findNetwork("ChattingNetwork").close();

		if (NetworkManager.Instance.findNetwork("GameNetwork") != null)
			NetworkManager.Instance.findNetwork("GameNetwork").close();
	}
}
