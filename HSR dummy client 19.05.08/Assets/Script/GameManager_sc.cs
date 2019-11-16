using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

namespace DummyClient
{

	public class GameManager_sc : MonoBehaviour
	{
		public GamePacketProcess gamePacketProcess;
		public GameObject notePrefab;
		public GameObject oddNotePrefab;
		public GameObject barPrefab;

		float beatPerBar = 32f; // Bar 당 비트수.
		int defaultSpeed = 10;
		int timeRateBySpeed = 2; // 거리 계수

		GameObject note;
		NoteObj_sc note_sc;

		GameObject enemy_note;
		NoteObj_sc enemy_note_sc;


		public string bmsName = "test";
		//public string bmsName = "_71";

		public int totalNote = 302;

		// 메트로놈 재생 여부
		public bool isTic;
		BeatCreator_sc beatCreator;
		

		public int perfect = 0;
		public int great = 0;
		public int fail = 0;

		public int enemy_perfect = 0;
		public int enemy_great = 0;
		public int enemy_fail = 0;

		public ulong enemySessionID = 0;

		public int playerCombo = 0;
		public int playerMaxCombo = 0;

		public int enemy_playerCombo = 0;
		public int enemy_playerMaxCombo = 0;

		public GameObject playerJudge;
		public GameObject enemyJudge;

		private bool sceneChangeActivated = false;

		public void increasePerfect() // great
		{
			perfect++;

			playerJudge.GetComponent<ShowJudge>().JudgeGreat();

			playerCombo++;
			if (playerCombo > playerMaxCombo)
				playerMaxCombo = playerCombo;
		}

		public void increaseGreat() // good
		{
			great++;

			playerJudge.GetComponent<ShowJudge>().JudgeGood();

			playerCombo++;
			if (playerCombo > playerMaxCombo)
				playerMaxCombo = playerCombo;
		}

		public void increaseFail() // bad
		{
			fail++;

			playerJudge.GetComponent<ShowJudge>().JudgeBad();

			playerCombo = 0;
		}

		public void decreaseFail()
		{
			fail--;
		}

		public void enemy_increasePerfect()
		{
			enemy_perfect++;
			enemyJudge.GetComponent<ShowJudge>().JudgeGreat();

			enemy_playerCombo++;
			if (enemy_playerCombo > enemy_playerMaxCombo)
				enemy_playerMaxCombo = enemy_playerCombo;
		}

		public void enemy_increaseGreat()
		{
			enemy_great++;
			enemyJudge.GetComponent<ShowJudge>().JudgeGood();

			enemy_playerCombo++;
			if (enemy_playerCombo > enemy_playerMaxCombo)
				enemy_playerMaxCombo = enemy_playerCombo;
		}

		public void enemy_increaseFail()
		{
			enemy_fail++;
			enemyJudge.GetComponent<ShowJudge>().JudgeBad();

			enemy_playerCombo = 0;
		}

		public void enemy_decreaseFail()
		{
			enemy_fail--;
		}

		// Use this for initialization
		IEnumerator Start()
		{

			gamePacketProcess = GameObject.Find("GameConnect").GetComponent<GamePacketProcess>();
			gamePacketProcess.SetGameManager();

			enemySessionID = GameObject.Find("PlayerData").GetComponent<playerData>().GetEnemySessionID();

			// 화면 비율.
			//Screen.SetResolution(Screen.width * 16 / 9, Screen.width, true);

			// bms 파일 로드.
			string[] lineData = File.ReadAllLines(Application.dataPath + "/BmsFiles/" + "Lovely_Summer.bms");
			//string[] lineData2 = File.ReadAllLines("Assets/Resources/BmsFiles/" + "test.bms");

			// BMS 파일 찾기.
			// TextAsset ta = Resources.Load("Resources/BmsFiles/" + bmsName) as TextAsset;
			// string strData = "" + ta.text;
			// string[] lineData = strData.Split('\n');

			// BMS 노트데이터 파싱.
			BmsLoader_sc bmsLoader = GameObject.Find("BmsLoader").GetComponent<BmsLoader_sc>();
			bmsLoader.BmsLoad(lineData);
			Bms bms = bmsLoader.getBms();
			
			// 시작선.
			GameObject plane_Top = GameObject.Find("Plane_Top");
			float startPositionY = plane_Top.transform.position.y;

			GameObject enemy_plane_Top = GameObject.Find("Enemy_Plane_Top");
			float enemy_startPositionY = enemy_plane_Top.transform.position.y;

			// 판정선.
			GameObject lineJudgment = GameObject.Find("LineJudgment");
			float judgmentPositionY = lineJudgment.transform.position.y;

			GameObject enemy_lineJudgment = GameObject.Find("Enemy_LineJudgment");
			float enemy_judgmentPositionY = enemy_lineJudgment.transform.position.y;

			// 시작선 ~ 판정선 거리.
			//print(Vector3.Distance(plane_Top.transform.position, lineJudgment.transform.position));
			//float distance = 17.81831f;

			// 노트 라인.
			GameObject lineCenter = GameObject.Find("LineCenter");
			GameObject enemy_lineCenter = GameObject.Find("Enemy_LineCenter");

			// 노트 소멸 위치.
			float destroyDelayPositionY = 30.0f;

			// 노트 간격 비율.
			float noteWidthRate = 1.8f;

			// 노트 프리팹 생성 및 리스트 저장.
			List<NoteObj_sc> noteObj_Line_1 = new List<NoteObj_sc>();
			List<NoteObj_sc> noteObj_Line_2 = new List<NoteObj_sc>();
			List<NoteObj_sc> noteObj_Line_3 = new List<NoteObj_sc>();
			List<NoteObj_sc> noteObj_Line_4 = new List<NoteObj_sc>();
			List<NoteObj_sc> noteObj_Line_5 = new List<NoteObj_sc>();
			List<NoteObj_sc> bar_Line = new List<NoteObj_sc>();

			List<NoteObj_sc> enemy_noteObj_Line_1 = new List<NoteObj_sc>();
			List<NoteObj_sc> enemy_noteObj_Line_2 = new List<NoteObj_sc>();
			List<NoteObj_sc> enemy_noteObj_Line_3 = new List<NoteObj_sc>();
			List<NoteObj_sc> enemy_noteObj_Line_4 = new List<NoteObj_sc>();
			List<NoteObj_sc> enemy_noteObj_Line_5 = new List<NoteObj_sc>();
			List<NoteObj_sc> enemy_bar_Line = new List<NoteObj_sc>();

			bool isLongNoteStart_1 = true;
			bool isLongNoteStart_2 = true;
			bool isLongNoteStart_3 = true;
			bool isLongNoteStart_4 = true;
			bool isLongNoteStart_5 = true;

			float preNoteTime_Ln1 = 0f;
			float preNoteTime_Ln2 = 0f;
			float preNoteTime_Ln3 = 0f;
			float preNoteTime_Ln4 = 0f;
			float preNoteTime_Ln5 = 0f;

			// 노트 소멸 딜레이 타임.
			float destroyDelayTime = bms.getTotalPlayTime() + 1;

			// Bar (중간 마디) 생성.
			float secondPerBar = 60.0f / (float)bms.getBpm() * 4.0f; // Bar 당 시간(초).
			int barCount = 0;
			for (int i = 0; i < bms.totalBarCount; i++)
			{
				// barLine 생성
				float barTime = barCount * secondPerBar; // Bar 시작시간

				note = (GameObject)Instantiate(barPrefab, new Vector3(0, startPositionY, 0), Quaternion.identity);
				note_sc = note.GetComponent<NoteObj_sc>();
				note_sc.speed = defaultSpeed;
				note_sc.destroyPositionY = judgmentPositionY - destroyDelayPositionY;
				note_sc.destroyDelayTime = destroyDelayTime;
				note_sc.noteTime = barTime;
				note_sc.notePlayerStatus = NoteObj_sc.notePlayer.player;
				bar_Line.Add(note_sc);

				enemy_note = (GameObject)Instantiate(barPrefab, new Vector3(20, enemy_startPositionY, 0), Quaternion.identity);
				enemy_note_sc = enemy_note.GetComponent<NoteObj_sc>();
				enemy_note_sc.speed = defaultSpeed;
				enemy_note_sc.destroyPositionY = judgmentPositionY - destroyDelayPositionY;
				enemy_note_sc.destroyDelayTime = destroyDelayTime;
				enemy_note_sc.noteTime = barTime;
				enemy_note_sc.notePlayerStatus = NoteObj_sc.notePlayer.enemy;
				enemy_bar_Line.Add(enemy_note_sc);

				barCount++;
			}
			
			// 노트 생성.
			foreach (BarData barData in bms.getBarDataList())
			{
				float linePositionX = lineCenter.transform.position.x;
				float enemy_linePositionX = enemy_lineCenter.transform.position.x;
				bool isLongChannel = false;

				int channel = barData.getChannel();
				if (channel == 11 || channel == 51)
				{
					linePositionX = lineCenter.transform.position.x - 2;
					enemy_linePositionX = enemy_lineCenter.transform.position.x - 2;
				}
				else if (channel == 12 || channel == 52)
				{
					linePositionX = lineCenter.transform.position.x - 1;
					enemy_linePositionX = enemy_lineCenter.transform.position.x - 1;
				}
				else if (channel == 13 || channel == 53)
				{
					linePositionX = lineCenter.transform.position.x;
					enemy_linePositionX = enemy_lineCenter.transform.position.x;
				}
				else if (channel == 14 || channel == 54)
				{
					linePositionX = lineCenter.transform.position.x + 1;
					enemy_linePositionX = enemy_lineCenter.transform.position.x + 1;
				}
				else if (channel == 15 || channel == 55)
				{
					linePositionX = lineCenter.transform.position.x + 2;
					enemy_linePositionX = enemy_lineCenter.transform.position.x + 2;
				}

				if (channel == 51 || channel == 52 || channel == 53 || channel == 54 || channel == 55)
				{
					isLongChannel = true;
				}

				foreach (Dictionary<int, float> noteData in barData.getNoteDataList())
				{
					foreach (int key in noteData.Keys)
					{
						// 단노트.
						if (isLongChannel == false && key != 0 && channel != 16)
						{
							float noteTime = noteData[key];

							if(channel == 12 || channel == 14)
							{
								note = (GameObject)Instantiate(oddNotePrefab, new Vector3(linePositionX * noteWidthRate, startPositionY, 0), Quaternion.identity);

								enemy_note = (GameObject)Instantiate(oddNotePrefab, new Vector3(20.0f + linePositionX * noteWidthRate, enemy_startPositionY, 0), Quaternion.identity);
							}
							else
							{
								note = (GameObject)Instantiate(notePrefab, new Vector3(linePositionX * noteWidthRate, startPositionY, 0), Quaternion.identity);
							
								enemy_note = (GameObject)Instantiate(notePrefab, new Vector3(20.0f + linePositionX * noteWidthRate, enemy_startPositionY, 0), Quaternion.identity);
								// 적플레이어 라인센터 +- (-2,-1,0,1,2) * 노트너비 
							}

							// 플레이어 노트
							
							note_sc = note.transform.GetChild(0).gameObject.GetComponentInChildren<NoteObj_sc>();
							note_sc.speed = defaultSpeed;
							note_sc.destroyPositionY = judgmentPositionY - destroyDelayPositionY;
							note_sc.destroyDelayTime = destroyDelayTime;
							note_sc.noteTime = noteTime;
							note_sc.channel = channel;
							note_sc.notePlayerStatus = NoteObj_sc.notePlayer.player;
							note_sc.particleName = "particle/Effect1";

							// 적 플레이어 노트
							
							enemy_note_sc = enemy_note.transform.GetChild(0).gameObject.GetComponentInChildren<NoteObj_sc>();
							enemy_note_sc.speed = defaultSpeed;
							enemy_note_sc.destroyPositionY = enemy_judgmentPositionY - destroyDelayPositionY;
							enemy_note_sc.destroyDelayTime = destroyDelayTime;
							enemy_note_sc.noteTime = noteTime;
							enemy_note_sc.channel = channel;
							enemy_note_sc.notePlayerStatus = NoteObj_sc.notePlayer.enemy;
							enemy_note_sc.particleName = "particle/Effect1";

							if (channel == 11)
							{
								noteObj_Line_1.Add(note_sc);
								enemy_noteObj_Line_1.Add(enemy_note_sc);
							}
							else if (channel == 12)
							{
								noteObj_Line_2.Add(note_sc);
								enemy_noteObj_Line_2.Add(enemy_note_sc);
							}
							else if (channel == 13)
							{
								noteObj_Line_3.Add(note_sc);
								enemy_noteObj_Line_3.Add(enemy_note_sc);
							}
							else if (channel == 14)
							{
								noteObj_Line_4.Add(note_sc);
								enemy_noteObj_Line_4.Add(enemy_note_sc);
							}
							else if (channel == 15)
							{
								noteObj_Line_5.Add(note_sc);
								enemy_noteObj_Line_5.Add(enemy_note_sc);
							}
						}

						// 롱노트. 현재 롱노트는 전부 단노트로 출력됨
						if (isLongChannel == true && key != 0)
						{
							float secondPerBeat = 60.0f / (float)bms.getBpm() * 4.0f / beatPerBar; // Beat당 시간(초)
							float longHeightRate = 0f; // 롱노트 높이 배율.

							bool isLongNoteStart = false;
							if (channel == 51)
							{
								isLongNoteStart = isLongNoteStart_1;
							}
							else if (channel == 52)
							{
								isLongNoteStart = isLongNoteStart_2;
							}
							else if (channel == 53)
							{
								isLongNoteStart = isLongNoteStart_3;
							}
							else if (channel == 54)
							{
								isLongNoteStart = isLongNoteStart_4;
							}
							else if (channel == 55)
							{
								isLongNoteStart = isLongNoteStart_5;
							}

							if (isLongNoteStart == true)
							{
								if (channel == 51)
								{
									preNoteTime_Ln1 = noteData[key];
									isLongNoteStart_1 = false;
								}
								else if (channel == 52)
								{
									preNoteTime_Ln2 = noteData[key];
									isLongNoteStart_2 = false;
								}
								else if (channel == 53)
								{
									preNoteTime_Ln3 = noteData[key];
									isLongNoteStart_3 = false;
								}
								else if (channel == 54)
								{
									preNoteTime_Ln4 = noteData[key];
									isLongNoteStart_4 = false;
								}
								else if (channel == 55)
								{
									preNoteTime_Ln5 = noteData[key];
									isLongNoteStart_5 = false;
								}
							}
							else if (isLongNoteStart == false)
							{
								float noteTime = noteData[key];

								float preNoteTime_Ln = 0f;
								if (channel == 51)
								{
									preNoteTime_Ln = preNoteTime_Ln1;
								}
								else if (channel == 52)
								{
									preNoteTime_Ln = preNoteTime_Ln2;
								}
								else if (channel == 53)
								{
									preNoteTime_Ln = preNoteTime_Ln3;
								}
								else if (channel == 54)
								{
									preNoteTime_Ln = preNoteTime_Ln4;
								}
								else if (channel == 55)
								{
									preNoteTime_Ln = preNoteTime_Ln5;
								}

								longHeightRate = (noteTime - preNoteTime_Ln) / secondPerBeat;

								//print("preNoteTime_Ln = " + preNoteTime_Ln);
								//print("noteTime = " + noteTime);
								//print("longHeightRate = " + Mathf.Round(longHeightRate));

								// 플레이어 롱노트
								if (channel == 52 || channel == 54)
								{
									note = (GameObject)Instantiate(oddNotePrefab, new Vector3(linePositionX * noteWidthRate, startPositionY, 0), Quaternion.identity);

									enemy_note = (GameObject)Instantiate(oddNotePrefab, new Vector3(20.0f + linePositionX * noteWidthRate, enemy_startPositionY, 0), Quaternion.identity);
								}
								else
								{
									note = (GameObject)Instantiate(notePrefab, new Vector3(linePositionX * noteWidthRate, startPositionY, 0), Quaternion.identity);

									enemy_note = (GameObject)Instantiate(notePrefab, new Vector3(20.0f + linePositionX * noteWidthRate, enemy_startPositionY, 0), Quaternion.identity);
									// 적플레이어 라인센터 +- (-2,-1,0,1,2) * 노트너비 
								}
								//note = (GameObject)Instantiate(notePrefab, new Vector3(linePositionX * noteWidthRate, startPositionY, 0), Quaternion.identity);
								//float originalScaleX = note.transform.localScale.x;
								//float originalScaleY = note.transform.localScale.y;
								//float originalScaleZ = note.transform.localScale.z;
								//note.transform.localScale = new Vector3(originalScaleX, originalScaleY * Mathf.Round(longHeightRate) + originalScaleY, originalScaleZ);
								// 단노트를 늘여서 롱노트를 만듬
								note_sc = note.transform.GetChild(0).GetComponent<NoteObj_sc>();
								note_sc.destroyPositionY = judgmentPositionY - destroyDelayPositionY;
								note_sc.destroyDelayTime = destroyDelayTime;
								note_sc.noteTime = preNoteTime_Ln;
								note_sc.channel = channel;
								note_sc.notePlayerStatus = NoteObj_sc.notePlayer.player;

								// 적 롱노트
								//enemy_note = (GameObject)Instantiate(notePrefab, new Vector3(20.0f + linePositionX * noteWidthRate, startPositionY, 0), Quaternion.identity);
								//float enemy_originalScaleX = enemy_note.transform.localScale.x;
								//float enemy_originalScaleY = enemy_note.transform.localScale.y;
								//float enemy_originalScaleZ = enemy_note.transform.localScale.z;
								//enemy_note.transform.localScale = new Vector3(enemy_originalScaleX, enemy_originalScaleY * Mathf.Round(longHeightRate) + enemy_originalScaleY, enemy_originalScaleZ);
								
								enemy_note_sc = enemy_note.transform.GetChild(0).GetComponent<NoteObj_sc>();
								enemy_note_sc.destroyPositionY = judgmentPositionY - destroyDelayPositionY;
								enemy_note_sc.destroyDelayTime = destroyDelayTime;
								enemy_note_sc.noteTime = preNoteTime_Ln;
								enemy_note_sc.channel = channel;
								enemy_note_sc.notePlayerStatus = NoteObj_sc.notePlayer.enemy;

								if (channel == 51)
								{
									noteObj_Line_1.Add(note_sc);
									enemy_noteObj_Line_1.Add(enemy_note_sc);

									preNoteTime_Ln1 = 0;
									isLongNoteStart_1 = true;
								}
								else if (channel == 52)
								{
									noteObj_Line_2.Add(note_sc);
									enemy_noteObj_Line_2.Add(enemy_note_sc);

									preNoteTime_Ln2 = 0;
									isLongNoteStart_2 = true;
								}
								else if (channel == 53)
								{
									noteObj_Line_3.Add(note_sc);
									enemy_noteObj_Line_3.Add(enemy_note_sc);

									preNoteTime_Ln3 = 0;
									isLongNoteStart_3 = true;
								}
								else if (channel == 54)
								{
									noteObj_Line_4.Add(note_sc);
									enemy_noteObj_Line_4.Add(enemy_note_sc);

									preNoteTime_Ln4 = 0;
									isLongNoteStart_4 = true;
								}
								else if (channel == 55)
								{
									noteObj_Line_5.Add(note_sc);
									enemy_noteObj_Line_5.Add(enemy_note_sc);

									preNoteTime_Ln5 = 0;
									isLongNoteStart_5 = true;
								}
							}
						}
					}
				}
			}

			// 플레이어 노트 리스트 정렬
			noteObj_Line_1.Sort(delegate (NoteObj_sc a, NoteObj_sc b)
			{
				return a.noteTime.CompareTo(b.noteTime);
			});
			noteObj_Line_2.Sort(delegate (NoteObj_sc a, NoteObj_sc b)
			{
				return a.noteTime.CompareTo(b.noteTime);
			});
			noteObj_Line_3.Sort(delegate (NoteObj_sc a, NoteObj_sc b)
			{
				return a.noteTime.CompareTo(b.noteTime);
			});
			noteObj_Line_4.Sort(delegate (NoteObj_sc a, NoteObj_sc b)
			{
				return a.noteTime.CompareTo(b.noteTime);
			});
			noteObj_Line_5.Sort(delegate (NoteObj_sc a, NoteObj_sc b)
			{
				return a.noteTime.CompareTo(b.noteTime);
			});
			bar_Line.Sort(delegate (NoteObj_sc a, NoteObj_sc b)
			{
				return a.noteTime.CompareTo(b.noteTime);
			});

			// 적 노트 리스트 정렬
			enemy_noteObj_Line_1.Sort(delegate (NoteObj_sc a, NoteObj_sc b)
			{
				return a.noteTime.CompareTo(b.noteTime);
			});
			enemy_noteObj_Line_2.Sort(delegate (NoteObj_sc a, NoteObj_sc b)
			{
				return a.noteTime.CompareTo(b.noteTime);
			});
			enemy_noteObj_Line_3.Sort(delegate (NoteObj_sc a, NoteObj_sc b)
			{
				return a.noteTime.CompareTo(b.noteTime);
			});
			enemy_noteObj_Line_4.Sort(delegate (NoteObj_sc a, NoteObj_sc b)
			{
				return a.noteTime.CompareTo(b.noteTime);
			});
			enemy_noteObj_Line_5.Sort(delegate (NoteObj_sc a, NoteObj_sc b)
			{
				return a.noteTime.CompareTo(b.noteTime);
			});
			enemy_bar_Line.Sort(delegate (NoteObj_sc a, NoteObj_sc b)
			{
				return a.noteTime.CompareTo(b.noteTime);
			});

			// 비트 크리에이터.
			beatCreator = GameObject.Find("BeatCreator").GetComponent<BeatCreator_sc>();
			// 플레이어 노트
			beatCreator.noteObj_Line_1 = noteObj_Line_1;
			beatCreator.noteObj_Line_2 = noteObj_Line_2;
			beatCreator.noteObj_Line_3 = noteObj_Line_3;
			beatCreator.noteObj_Line_4 = noteObj_Line_4;
			beatCreator.noteObj_Line_5 = noteObj_Line_5;
			beatCreator.bar_Line = bar_Line;

			// 적 플레이어 노트
			beatCreator.enemy_noteObj_Line_1 = enemy_noteObj_Line_1;
			beatCreator.enemy_noteObj_Line_2 = enemy_noteObj_Line_2;
			beatCreator.enemy_noteObj_Line_3 = enemy_noteObj_Line_3;
			beatCreator.enemy_noteObj_Line_4 = enemy_noteObj_Line_4;
			beatCreator.enemy_noteObj_Line_5 = enemy_noteObj_Line_5;
			beatCreator.enemy_bar_Line = enemy_bar_Line;

			beatCreator.bpm = (float)bms.getBpm();
			beatCreator.beatPerBar = beatPerBar;
			beatCreator.timeRateBySpeed = timeRateBySpeed;


			// BGM 지정
			AudioClip bgm = Resources.Load("Sound/" + bmsName) as AudioClip;
			beatCreator.bgmSound = bgm;
			beatCreator.isTic = isTic;

			// 모든 렌더링작업이 끝날 때까지 대기
			yield return new WaitForEndOfFrame();

			// beatCreator.isStart = true; // beatcreator에서 create코루틴 실행하게 됨
		}

		public void setGameStart()
		{
			beatCreator.isStart = true;
		}

		public int index1 = 0, index2 = 0, index3 = 0, index4 = 0, index5 = 0;

		public void increaseIndex1()
		{
			index1++;
		}

		public void increaseIndex2()
		{
			index2++;
		}

		public void increaseIndex3()
		{
			index3++;
		}

		public void increaseIndex4()
		{
			index4++;
		}

		public void increaseIndex5()
		{
			index5++;
		}

		public int enemy_index1 = 0, enemy_index2 = 0, enemy_index3 = 0, enemy_index4 = 0, enemy_index5 = 0;

		public void enemy_increaseIndex1()
		{
			enemy_index1++;
		}

		public void enemy_increaseIndex2()
		{
			enemy_index2++;
		}

		public void enemy_increaseIndex3()
		{
			enemy_index3++;
		}

		public void enemy_increaseIndex4()
		{
			enemy_index4++;
		}

		public void enemy_increaseIndex5()
		{
			enemy_index5++;
		}

		public void EnemyNoteHit(uint enemy_KeyCode_, uint enemy_Judge_)
		{
			if (enemy_KeyCode_ == 1 && beatCreator.enemy_noteObj_Line_1[enemy_index1] != null)
			{
				beatCreator.enemy_noteObj_Line_1[enemy_index1].NoteHit(enemy_Judge_);

				//if (beatCreator.enemy_noteObj_Line_1[enemy_index1].longNoteHit != true)
				Destroy(beatCreator.enemy_noteObj_Line_1[enemy_index1].gameObject.transform.parent.gameObject);

				enemy_index1++;
			}

			if (enemy_KeyCode_ == 2 && beatCreator.enemy_noteObj_Line_2[enemy_index2] != null)
			{
				beatCreator.enemy_noteObj_Line_2[enemy_index2].NoteHit(enemy_Judge_);

				//if (beatCreator.enemy_noteObj_Line_2[enemy_index2].longNoteHit != true)
				Destroy(beatCreator.enemy_noteObj_Line_2[enemy_index2].gameObject.transform.parent.gameObject);

				enemy_index2++;
			}

			if (enemy_KeyCode_ == 3 && beatCreator.enemy_noteObj_Line_3[enemy_index3] != null)
			{
				beatCreator.enemy_noteObj_Line_3[enemy_index3].NoteHit(enemy_Judge_);

				//if (beatCreator.enemy_noteObj_Line_3[enemy_index3].longNoteHit != true)
				Destroy(beatCreator.enemy_noteObj_Line_3[enemy_index3].gameObject.transform.parent.gameObject);

				enemy_index3++;
			}

			if (enemy_KeyCode_ == 4 && beatCreator.enemy_noteObj_Line_4[enemy_index4] != null)
			{
				beatCreator.enemy_noteObj_Line_4[enemy_index4].NoteHit(enemy_Judge_);

				//if (beatCreator.enemy_noteObj_Line_4[enemy_index4].longNoteHit != true)
				Destroy(beatCreator.enemy_noteObj_Line_4[enemy_index4].gameObject.transform.parent.gameObject);

				enemy_index4++;
			}

			if (enemy_KeyCode_ == 5 && beatCreator.enemy_noteObj_Line_5[enemy_index5] != null)
			{
				beatCreator.enemy_noteObj_Line_5[enemy_index5].NoteHit(enemy_Judge_);

				//if (beatCreator.enemy_noteObj_Line_5[enemy_index5].longNoteHit != true)
				Destroy(beatCreator.enemy_noteObj_Line_5[enemy_index5].gameObject.transform.parent.gameObject);

				enemy_index5++;
			}
		}

		void MoveToChattingScene()
		{
			GameObject.Find("ChattingConnect").GetComponent<ChattingConnect>().chatStatus = ChattingConnect.chattingSceneStatus.chatting;
			SceneManager.LoadScene("ChattingScene");
		}

		private void Update()
		{
			// 모든 노트가 나왔을 경우 로비로 돌아감
			if (((index1 + index2 + index3 + index4 + index5) >= 302) && sceneChangeActivated == false)
			{
				Debug.Log("Move To ChatScene After 5 sec");

				sceneChangeActivated = true;
				Invoke("MoveToChattingScene", 5.0f);
			}

			
			if (Input.GetKeyDown(KeyCode.D))
			{

				if (beatCreator.noteObj_Line_1[index1].noteJudgeStatus != NoteObj_sc.noteJudge.Out && beatCreator.noteObj_Line_1[index1] != null)
				{
					uint judge = beatCreator.noteObj_Line_1[index1].NoteHit(0);

					PK_C_REQ_ENEMY_NOTEHIT packet = new PK_C_REQ_ENEMY_NOTEHIT();
					packet.keycode_ = 1;
					packet.enemyJudge_ = judge;
					packet.enemySessionID_ = enemySessionID;
					NetworkManager.Instance.findNetwork("GameNetwork").sendPacket(packet);
					
					//if (beatCreator.noteObj_Line_1[index1].longNoteHit != true)
					Destroy(beatCreator.noteObj_Line_1[index1].gameObject.transform.parent.gameObject);

					index1++;
				}
			}

			if (Input.GetKeyDown(KeyCode.F))
			{

				if (beatCreator.noteObj_Line_2[index2].noteJudgeStatus != NoteObj_sc.noteJudge.Out && beatCreator.noteObj_Line_2[index2] != null)
				{
					uint judge = beatCreator.noteObj_Line_2[index2].NoteHit(0);

					PK_C_REQ_ENEMY_NOTEHIT packet = new PK_C_REQ_ENEMY_NOTEHIT();
					packet.keycode_ = 2;
					packet.enemyJudge_ = judge;
					packet.enemySessionID_ = enemySessionID;

					NetworkManager.Instance.findNetwork("GameNetwork").sendPacket(packet);

					//if (beatCreator.noteObj_Line_2[index2].longNoteHit != true)
					Destroy(beatCreator.noteObj_Line_2[index2].gameObject.transform.parent.gameObject);

					index2++;
				}
			}
			if (Input.GetKeyDown(KeyCode.Space))
			{

				if (beatCreator.noteObj_Line_3[index3].noteJudgeStatus != NoteObj_sc.noteJudge.Out && beatCreator.noteObj_Line_3[index3] != null)
				{
					uint judge = beatCreator.noteObj_Line_3[index3].NoteHit(0);

					PK_C_REQ_ENEMY_NOTEHIT packet = new PK_C_REQ_ENEMY_NOTEHIT();
					packet.keycode_ = 3;
					packet.enemyJudge_ = judge;
					packet.enemySessionID_ = enemySessionID;

					NetworkManager.Instance.findNetwork("GameNetwork").sendPacket(packet);

					//if (beatCreator.noteObj_Line_3[index3].longNoteHit != true)
					Destroy(beatCreator.noteObj_Line_3[index3].gameObject.transform.parent.gameObject);

					index3++;
				}
			}
			if (Input.GetKeyDown(KeyCode.J))
			{

				if (beatCreator.noteObj_Line_4[index4].noteJudgeStatus != NoteObj_sc.noteJudge.Out && beatCreator.noteObj_Line_4[index4] != null)
				{
					uint judge =  beatCreator.noteObj_Line_4[index4].NoteHit(0);

					PK_C_REQ_ENEMY_NOTEHIT packet = new PK_C_REQ_ENEMY_NOTEHIT();
					packet.keycode_ = 4;
					packet.enemyJudge_ = judge;
					packet.enemySessionID_ = enemySessionID;

					NetworkManager.Instance.findNetwork("GameNetwork").sendPacket(packet);

					//if (beatCreator.noteObj_Line_4[index4].longNoteHit != true)
					Destroy(beatCreator.noteObj_Line_4[index4].gameObject.transform.parent.gameObject);

					index4++;
				}
			}
			if (Input.GetKeyDown(KeyCode.K))
			{

				if (beatCreator.noteObj_Line_5[index5].noteJudgeStatus != NoteObj_sc.noteJudge.Out && beatCreator.noteObj_Line_5[index5] != null)
				{
					uint judge =  beatCreator.noteObj_Line_5[index5].NoteHit(0);

					PK_C_REQ_ENEMY_NOTEHIT packet = new PK_C_REQ_ENEMY_NOTEHIT();
					packet.keycode_ = 5;
					packet.enemyJudge_ = judge;
					packet.enemySessionID_ = enemySessionID;

					NetworkManager.Instance.findNetwork("GameNetwork").sendPacket(packet);

					//if (beatCreator.noteObj_Line_5[index5].longNoteHit != true)
					Destroy(beatCreator.noteObj_Line_5[index5].gameObject.transform.parent.gameObject);

					index5++;
				}
			}

			//if (Input.GetKeyDown(KeyCode.A))
			//{
			//	PK_C_REQ_EXIT_GAME packet = new PK_C_REQ_EXIT_GAME();

			//	NetworkManager.Instance.findNetwork("GameNetwork").sendPacket(packet);
			//}

		}

		private void OnDestroy()
		{
			PK_C_REQ_EXIT_GAME packet = new PK_C_REQ_EXIT_GAME();

			if(NetworkManager.Instance.findNetwork("GameNetwork") != null)
				NetworkManager.Instance.findNetwork("GameNetwork").sendPacket(packet);
		}
	}
	
}