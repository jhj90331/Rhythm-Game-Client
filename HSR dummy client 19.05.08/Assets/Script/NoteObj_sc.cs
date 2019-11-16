using UnityEngine;
using System.Collections;


namespace DummyClient
{

	public class NoteObj_sc : MonoBehaviour
	{

		public int speed;
		public bool isStart = false;

		public int channel;
		public float noteTime;

		public float destroyPositionY;
		public float destroyDelayTime;

		ParticleSystem testParticle = null;
		public string particleName;

		public GameManager_sc gameManager;
		public GameObject GMgameobject;

		//NoteObj_sc parentNoteObj = null;

		public enum noteJudge
		{
			Out = 0,
			Perfect,
			Great,
			Fail
		}

		public noteJudge noteJudgeStatus;

		public enum notePlayer
		{
			player = 0,
			enemy,
		}

		public notePlayer notePlayerStatus;

		public bool longNoteHit = false;

		// Use this for initialization
		void Start()
		{
			GMgameobject = GameObject.Find("GameManager");
			gameManager = GameObject.Find("GameManager").GetComponent<GameManager_sc>();
			noteJudgeStatus = noteJudge.Out;			
		}

		void OnTriggerEnter(Collider col)
		{


			if (col.transform.tag == "LineJudgment_Great")
			{
				noteJudgeStatus = noteJudge.Great;
			}

			if (col.transform.tag == "LineJudgment_Fail")
			{
				noteJudgeStatus = noteJudge.Fail;
			}

			if (col.transform.tag == "LineJudgment_Bottom")
			{

				noteJudgeStatus = noteJudge.Out;

				//if (longNoteHit == false)
				//	gameObject.GetComponent<Renderer>().material.color = Color.red;
				if(transform.parent.gameObject)
				{
					transform.parent.GetChild(1).gameObject.SetActive(false);
				}

				if (notePlayerStatus == notePlayer.player)
				{
					gameManager.increaseFail();

					//if (longNoteHit == true)	// 이미 눌린 롱노트가 bottom을 지나가면서 fail을 증가시켜버리는 문제 보정
					//	gameManager.decreaseFail();
					

					if (channel == 11 || (channel == 51 && longNoteHit != true))
					{
						gameManager.increaseIndex1();
					}

					if (channel == 12 || (channel == 52 && longNoteHit != true))
					{
						gameManager.increaseIndex2();
					}

					if (channel == 13 || (channel == 53 && longNoteHit != true))
					{
						gameManager.increaseIndex3();
					}

					if (channel == 14 || (channel == 54 && longNoteHit != true))
					{
						gameManager.increaseIndex4();
					}

					if (channel == 15 || (channel == 55 && longNoteHit != true))
					{
						gameManager.increaseIndex5();
					}
				}

				if (notePlayerStatus == notePlayer.enemy)
				{
					gameManager.enemy_increaseFail();

					if (longNoteHit == true)
						gameManager.enemy_decreaseFail();


					if (channel == 11 || (channel == 51 && longNoteHit != true))
					{
						gameManager.enemy_increaseIndex1();
					}

					if (channel == 12 || (channel == 52 && longNoteHit != true))
					{
						gameManager.enemy_increaseIndex2();
					}

					if (channel == 13 || (channel == 53 && longNoteHit != true))
					{
						gameManager.enemy_increaseIndex3();
					}

					if (channel == 14 || (channel == 54 && longNoteHit != true))
					{
						gameManager.enemy_increaseIndex4();
					}

					if (channel == 15 || (channel == 55 && longNoteHit != true))
					{
						gameManager.enemy_increaseIndex5();
					}
				}

			}

			if (col.transform.tag == "LineJudgment")    // 노트가 판정선에 닿았을 경우
			{
				noteJudgeStatus = noteJudge.Perfect;

				//gameObject.GetComponent<Renderer>().material.color = Color.green;
				//gameObject.GetComponentInParent<Renderer>().material.color = Color.green;

				//NoteHit();


			}
		}

		public uint NoteHit(uint judge_)
		{
			//Debug.Log("Note Hit");

			Transform particleObject = (Transform)Instantiate(Resources.Load("Particle/Effect1", typeof(Transform)), new Vector3(transform.position.x, -29f, -1), Quaternion.identity);
			testParticle = (ParticleSystem)particleObject.GetComponent(typeof(ParticleSystem));
			testParticle.Play();

			//gameObject.GetComponent<Renderer>().material.color = Color.green;

			if (judge_ == 0)
			{
				if (notePlayerStatus == notePlayer.player)
				{
					if (noteJudgeStatus == noteJudge.Perfect)
					{
						gameManager.increasePerfect();
						return 1;
					}

					if (noteJudgeStatus == noteJudge.Great)
					{
						gameManager.increaseGreat();
						return 2;
					}

					if (noteJudgeStatus == noteJudge.Fail)
					{
						gameManager.increaseFail();
						return 3;
					}
				}

				if (notePlayerStatus == notePlayer.enemy)
				{
					if (noteJudgeStatus == noteJudge.Perfect)
					{
						gameManager.enemy_increasePerfect();
					}

					if (noteJudgeStatus == noteJudge.Great)
					{
						gameManager.enemy_increaseGreat();
					}

					if (noteJudgeStatus == noteJudge.Fail)
					{
						gameManager.enemy_increaseFail();
					}
				}

				//if (channel == 51 || channel == 52 || channel == 53 || channel == 54 || channel == 55)
				//{
				//	longNoteHit = true;
				//}

			}

			if(judge_ == 1)
			{
				gameManager.enemy_increasePerfect();
			}
			if(judge_ == 2)
			{
				gameManager.enemy_increaseGreat();
			}
			if(judge_ == 3)
			{
				gameManager.enemy_increaseFail();
			}

			return 0;
		}

		void destroyParticle()
		{
			testParticle.Stop();
			testParticle.Clear();
		}

		// Update is called once per frame
		void Update()
		{
			if (isStart == true)
			{
				StartCoroutine(move());
			}

		}

		IEnumerator move()
		{

			//Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
			if (transform.position.y > destroyPositionY)
			{
				if(transform.parent)
					transform.parent.Translate(Vector3.down * speed * Time.smoothDeltaTime);

			}
			else
			{
				PK_C_REQ_GAME_NAME packet = new PK_C_REQ_GAME_NAME();
				packet.name_ = "NOTE";

				//NetworkManager.Instance.findNetwork("GameNetwork").sendPacket(packet);
				if(transform.parent)
					Destroy(transform.parent.gameObject);

			}

			yield return null;
		}

		public void setPosition(float x, float y, float z)
		{
			transform.position = new Vector3(x, y, z);
		}

	}


}