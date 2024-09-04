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

	public GameObject editMenu;
	public TMP_Text titleLabel;
	public TMP_Text inBlock1Label;
	public TMP_Text inBlock2Label;
	public TMP_Text input1Label;
	public TMP_Text input2Label;
	public TMP_Text outputLabel;

	private LineRenderer lineRenderer1; 
    private LineRenderer lineRenderer2; 
	private float lineWidth = 0.1f;
	public Material lineMaterial;



	// Start is called before the first frame update
	void Start()
    {
		sharedLogic = GameObject.FindWithTag("ScriptHost").GetComponent<SharedLogic>();
		Transform child = transform.Find("TitleCanvas");
		TMP_Text t = child.GetComponent<TMP_Text>();
		t.text = title;
		// Create and setup the first LineRenderer
        GameObject lineObj1 = new GameObject("LineRenderer1");
        lineObj1.transform.parent = this.transform;  // Make it a child of the current block
        lineRenderer1 = lineObj1.AddComponent<LineRenderer>();
        sharedLogic.SetupLineRenderer(lineRenderer1, lineWidth, lineMaterial);

        // Create and setup the second LineRenderer
        GameObject lineObj2 = new GameObject("LineRenderer2");
        lineObj2.transform.parent = this.transform;  // Make it a child of the current block
        lineRenderer2 = lineObj2.AddComponent<LineRenderer>();
        sharedLogic.SetupLineRenderer(lineRenderer2, lineWidth, lineMaterial);
	}

	// Update is called once per frame
	void Update()
    {
		SetName();
		CheckMultiInput();
		sharedLogic.ManageColour(gameObject, active, correct, grey, green, red);
		titleLabel.text = title;
		input1Label.text = "req1: " + input_required1;
		input2Label.text = "req2: " + input_required2;
		outputLabel.text = "prod: " + output;
		if (inputSource1 != null){ 
			inBlock1Label.text = "in1: " + sharedLogic.GetBlockTitle(inputSource1);
			lineRenderer1.SetPosition(0, transform.position);             // Start at this block's position
			lineRenderer1.SetPosition(1, inputSource1.transform.position); // End at the InputSource block's position
		} else {
			inBlock1Label.text = "in1: None";
			// If InputSource is null, disable the line by setting both positions to the same point
			lineRenderer1.SetPosition(0, transform.position);
			lineRenderer1.SetPosition(1, transform.position);
		}
		if (inputSource2 != null){
			inBlock2Label.text = "in2: " + sharedLogic.GetBlockTitle(inputSource2);
			lineRenderer2.SetPosition(0, transform.position);            
			lineRenderer2.SetPosition(1, inputSource2.transform.position); 
		} else {
			inBlock2Label.text = "in2: None";
			lineRenderer2.SetPosition(0, transform.position);
			lineRenderer2.SetPosition(1, transform.position);
		}
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

				if (correct == false)
                {
					Debug.Log("Assembler input incorrect");
                } else if (currentInputObject == inputSource1)
				{
					currentInputObject = inputSource2;
					currentInputRequired = input_required2;
				}
			}
		}
	}

	void SetName()
	{
		Transform child = transform.Find("TitleCanvas");
		TMP_Text t = child.GetComponent<TMP_Text>();
		title = gameObject.name;
		t.text = title;
	}

	public void SetTitle()
	{
		sharedLogic.EditObjectText(gameObject, "title");
	}


	public void SetInput1()
	{
		sharedLogic.EditObjectText(gameObject, "input1");
	}

	public void SetInput2()
	{
		sharedLogic.EditObjectText(gameObject, "input2");
	}

	public void SetOutput()
	{
		sharedLogic.EditObjectText(gameObject, "output");
	}

	public void SetInputBlock1()
	{
		sharedLogic.EditObjectText(gameObject, "inblock1");
	}

	public void SetInputBlock2()
	{
		sharedLogic.EditObjectText(gameObject, "inblock2");
	}

	[ContextMenu("Actuate")]
	void Actuate()
	{
		active = !active;
	}

	public void ToggleEditMenu()
	{
		editMenu.SetActive(!editMenu.activeInHierarchy);
	}

}
