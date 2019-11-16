using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerData : MonoBehaviour {

	private string playerName;
	private ulong enemySessionID;


	public void SetPlayerName(string name_)
	{
		playerName = name_;
	}

	public string GetPlayerName()
	{
		return playerName;
	}

	public void SetEnemySessionID(ulong enemySessionID_)
	{
		enemySessionID = enemySessionID_;
	}
	
	public ulong GetEnemySessionID()
	{
		return enemySessionID;
	}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this.gameObject);	
	}
	
	
}
