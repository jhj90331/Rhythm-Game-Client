using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DummyClient
{

	public class ShowTotalScore : MonoBehaviour
	{
		public GameManager_sc gameManager;

		public Text totalGreat, totalGood, totalBad;

		// Use this for initialization
		void Start()
		{
			totalGreat.text = "0";
			totalGood.text = "0";
			totalBad.text = "0";
		}

		// Update is called once per frame
		void Update()
		{
			totalGreat.text = gameManager.perfect.ToString();
			totalGood.text = gameManager.great.ToString();
			totalBad.text = gameManager.fail.ToString();
		}
	}











}