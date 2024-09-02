using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using TMPro;

public class Destination : MonoBehaviour
{

	public string title;

	public GameObject inputSource;
	public string input_required;

	public bool active = true; // is the component currently turned on
	public bool correct = false; // does the component have the correct input

	public Material red;
	public Material green;
	public Material grey;

	public SharedLogic sharedLogic;



	// Start is called before the first frame update
	void Start()
	{
		Transform child = transform.Find("TitleCanvas");
		TMP_Text t = child.GetComponent<TMP_Text>();
		t.text = "Des:" + title;
	}

	// Update is called once per frame
	void Update()
	{
		SetAttributes();
		correct = sharedLogic.CheckInput(gameObject, inputSource, correct, input_required);
		sharedLogic.ManageColour(gameObject, active, correct, grey, green, red);
	}

	[ContextMenu("SetTitle")]
	void SetAttributes()
	{
		gameObject.name = title;
	}

	[ContextMenu("GetTitle")]
	String GetTitle()
	{
		return title;
	}

	[ContextMenu("Actuate")]
	void Actuate()
	{
		active = !active;
	}
}
