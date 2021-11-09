using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

	void Start()
	{
		this.transform.GetChild(0).gameObject.SetActive(false);
		//this.transform.GetChild(1).gameObject.SetActive(true);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (Time.timeScale != 0)
			{
				transform.SetAsLastSibling();
				this.transform.GetChild(0).gameObject.SetActive(true);
				Time.timeScale = 0;
			}
			else
			{
				this.transform.GetChild(0).gameObject.SetActive(false);
				Time.timeScale = 1;
			}
		}
	}
}