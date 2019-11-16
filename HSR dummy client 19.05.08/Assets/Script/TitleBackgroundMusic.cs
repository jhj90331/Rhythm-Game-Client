using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBackgroundMusic : MonoBehaviour
{

	private AudioSource bgm;

	// Use this for initialization
	void Start()
	{
		DontDestroyOnLoad(this.gameObject);
		bgm = this.gameObject.GetComponent<AudioSource>();
	}

	public void StartBGM()
	{
		if (bgm.isPlaying == false)
			bgm.Play();
	}

	public void StopBGM()
	{
		bgm.Stop();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
