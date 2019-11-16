using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDown : MonoBehaviour {

	public bool D_keyPressed, F_keyPressed, Space_keyPressed, J_keyPressed, K_keyPressed;

	// Use this for initialization
	void Start () {
		D_keyPressed = false;
		F_keyPressed = false;
		Space_keyPressed = false;
		J_keyPressed = false;
		K_keyPressed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.D))
		{
			D_keyPressed = true;
		}
		if(Input.GetKeyUp(KeyCode.D))
		{
			D_keyPressed = false;
		}

		if (Input.GetKeyDown(KeyCode.F))
		{
			F_keyPressed = true;
		}
		if (Input.GetKeyUp(KeyCode.F))
		{
			F_keyPressed = false;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Space_keyPressed = true;
		}
		if (Input.GetKeyUp(KeyCode.Space))
		{
			Space_keyPressed = false;
		}

		if (Input.GetKeyDown(KeyCode.J))
		{
			J_keyPressed = true;
		}
		if (Input.GetKeyUp(KeyCode.J))
		{
			J_keyPressed = false;
		}

		if (Input.GetKeyDown(KeyCode.K))
		{
			K_keyPressed = true;
		}
		if (Input.GetKeyUp(KeyCode.K))
		{
			K_keyPressed = false;
		}

		PressedKeyRender();

	}

	public void PressedKeyRender()
	{

		if (D_keyPressed == true)
		{
			transform.GetChild(0).gameObject.SetActive(true);
		}
		else
		{
			transform.GetChild(0).gameObject.SetActive(false);
		}

		if (F_keyPressed == true)
		{
			transform.GetChild(1).gameObject.SetActive(true);
		}
		else
		{
			transform.GetChild(1).gameObject.SetActive(false);
		}

		if (Space_keyPressed == true)
		{
			transform.GetChild(2).gameObject.SetActive(true);
		}
		else
		{
			transform.GetChild(2).gameObject.SetActive(false);
		}

		if (J_keyPressed == true)
		{
			transform.GetChild(3).gameObject.SetActive(true);
		}
		else
		{
			transform.GetChild(3).gameObject.SetActive(false);
		}

		if (K_keyPressed == true)
		{
			transform.GetChild(4).gameObject.SetActive(true);
		}
		else
		{
			transform.GetChild(4).gameObject.SetActive(false);
		}
	}
}
