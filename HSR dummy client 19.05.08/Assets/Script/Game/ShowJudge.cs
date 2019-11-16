using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowJudge : MonoBehaviour {

	public GameObject bad;
	public GameObject good;
	public GameObject great;

	public float time;

	// Use this for initialization
	void Start () {
		time = 0;
		bad.SetActive(false);
		good.SetActive(false);
		great.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (time > 0)
			time -= Time.deltaTime;
		else
		{
			bad.SetActive(false);
			good.SetActive(false);
			great.SetActive(false);
		}
	}

	public void JudgeBad()
	{
		time = 0.5f;
		bad.SetActive(true);
		good.SetActive(false);
		great.SetActive(false);
	}

	public void JudgeGood()
	{
		time = 0.5f;
		bad.SetActive(false);
		good.SetActive(true);
		great.SetActive(false);
	}

	public void JudgeGreat()
	{
		time = 0.5f;
		bad.SetActive(false);
		good.SetActive(false);
		great.SetActive(true);
	}
}
