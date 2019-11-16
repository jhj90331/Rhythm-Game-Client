using UnityEngine;
using System.Collections;

namespace DummyClient
{


	public class ShowCombo : MonoBehaviour
	{		
		public GameManager_sc gameManager;

		public Sprite[] spr;

		private Sprite[] showSpr;

		private int prevCombo;

		// Use this for initialization
		void Awake()
		{			
			gameManager = GameObject.Find("GameManager").GetComponent<GameManager_sc>();
			showSpr = new Sprite[1];
		}

		void intToSpr(int n, ref Sprite[] res)
		{
			int forCount = n;

			int count = 0;

			while (forCount != 0)
			{
				forCount /= 10;

				++count;
			}

			if (res.Length != count)
			{
				res = new Sprite[count];
			}

			for (int i = 0; i < count; ++i)
			{
				int mod = n % 10;

				res[count - 1 - i] = spr[mod];

				n /= 10;
			}
		}

		// Update is called once per frame
		void Update()
		{
			if (gameManager.playerCombo != 0)
			{
				if (gameManager.playerCombo != prevCombo)
				{
					intToSpr(gameManager.playerCombo, ref showSpr);
					
					int count = showSpr.Length - transform.childCount;

					if (count > 0)
					{
						for (int i = transform.childCount; i < showSpr.Length; ++i)
						{
							GameObject temp = new GameObject(i.ToString());

							temp.transform.parent = gameObject.transform;

							SpriteRenderer sr = temp.AddComponent<SpriteRenderer>();

							sr.sortingLayerName = "UI";
						}
					}
					else if (count < 0)
					{
						for (int i = showSpr.Length; i < transform.childCount; ++i)
						{
							Destroy(transform.GetChild(i).gameObject);
						}
					}

					count = showSpr.Length;

					for (int i = 0; i < count; ++i)
					{
						Transform tr = transform.GetChild(i);
						
						// 콤보수 위치조정 할 것!
						tr.position = new Vector3(((float)i * 1.7f), transform.position.y, transform.position.z );
						if(count == 2)
						{
							tr.position = tr.position - new Vector3(1.0f, 0, 0);
						}
						if(count == 3)
						{
							tr.position = tr.position - new Vector3(1.7f, 0, 0);
						}
						if(count == 4)
						{
							tr.position = tr.position - new Vector3(2.4f, 0, 0);
						}


						tr.localScale = new Vector3(0.6f, 0.6f, 0.6f);



						tr.gameObject.GetComponent<SpriteRenderer>().sprite = showSpr[i];
						//tr.gameObject.transform.position = tr.position;
					}

					prevCombo = gameManager.playerCombo;
				}
			}
			else
			{
				for (int i = 0; i < transform.childCount; ++i)
				{
					Destroy(transform.GetChild(i).gameObject);
				}
			}
		}
	}













}