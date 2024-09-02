using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using TMPro;

public class Assembler : MonoBehaviour
{

	public string title;

	public GameObject inputSource1;
	public string input_required1;
	public GameObject inputSource2;
	public string input_required2;
	public string output;

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
		t.text = "Ass:" + title;
	}

	// Update is called once per frame
	void Update()
    {
		SetAttributes();
		CheckMultiInput();
		sharedLogic.ManageColour(gameObject, active, correct, grey, green, red);

	}

	void CheckMultiInput()
	{

		if (inputSource1 == inputSource2)
		{
			if (inputSource1.tag == "Disassembler")
			{
				Disassembler inputAttributes = inputSource1.GetComponent<Disassembler>();
				correct = ((inputAttributes.correct) &&
							(inputAttributes.active) &&
							(inputAttributes.output1 == input_required1) &&
							(inputAttributes.output2 == input_required2));
			}
			else if (inputSource1.tag == "Multiprocessor")
			{
				Multiprocessor inputAttributes = inputSource1.GetComponent<Multiprocessor>();
				correct = ((inputAttributes.correct) &&
							(inputAttributes.active) &&
							(inputAttributes.output1 == input_required1) &&
							(inputAttributes.output2 == input_required2));
			}

		}
		else
		{

			GameObject currentInputObject = inputSource1;
			string currentInputRequired = input_required1;


			for (int i = 0; i < 2; i++) // loop twice, once for each input
			{

				correct = sharedLogic.CheckInput(gameObject, currentInputObject, correct, currentInputRequired);

				if (correct == false) break;

				if (currentInputObject == inputSource1)
				{
					currentInputObject = inputSource2;
					currentInputRequired = input_required2;
				}
			}
		}
	}

	[ContextMenu("SetTitle")]
	void SetAttributes()
    {
		gameObject.name = title;
		// make a UI or something that allows text to be entered and then set to the attributes
		//gameObject.input_required = 
		//gameObject.output = 
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
