using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using TMPro;

public class Multiprocessor : BaseBlock
{

	[Header("Settings")]
	[SerializeField] public GameObject inputSource1;
	[SerializeField] public string input_required1;
	[SerializeField] public GameObject inputSource2;
	[SerializeField] public string input_required2;
	[SerializeField] public GameObject outputBlock1;
	[SerializeField] public string output1;
	[SerializeField] public GameObject outputBlock2;
	[SerializeField] public string output2;

	public TMP_Text inBlock1Label;
	public TMP_Text inBlock2Label;
	public TMP_Text input1Label;
	public TMP_Text input2Label;
	public TMP_Text output1Label;
	public TMP_Text output2Label;
	public TMP_Text outBlock1Label;
	public TMP_Text outBlock2Label;

	private LineRenderer lineRenderer1; 
    private LineRenderer lineRenderer2; 


	protected override void Awake()
	{
		SaveSystem.multiprocessors.Add(this);
	}

	private void OnDestroy()
	{
		SaveSystem.multiprocessors.Remove(this);

	}

	// Start is called before the first frame update
	void Start()
    {
		Setup();

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
	new void Update()
    {
		SetName();
		CheckMultiInput();
		UpdatePanelColor();
		UpdateLabels();
		UpdateLineRenderers();
	}

	public override void UpdateLabels()
	{
		base.UpdateLabels();
		input1Label.text = "req1: " + input_required1;
		input2Label.text = "req2: " + input_required2;
		output1Label.text = "prod1: " + output1;
		output2Label.text = "prod2: " + output2;
		inBlock1Label.text = "in1: " + (inputSource1 != null ? sharedLogic.GetBlockTitle(inputSource1) : "None");
		inBlock2Label.text = "in2: " + (inputSource2 != null ? sharedLogic.GetBlockTitle(inputSource2) : "None");
		outBlock1Label.text = "out1: " + (outputBlock1 != null ? sharedLogic.GetBlockTitle(outputBlock1) : "None");
		outBlock2Label.text = "out2: " + (outputBlock2 != null ? sharedLogic.GetBlockTitle(outputBlock2) : "None");
	}

	void CheckMultiInput()
	{

		if ((inputSource1 == null) || (inputSource2 == null))
        {
			correct = false;
        }
		else if (inputSource1 == inputSource2)
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

	public void SetInput1()
	{
		sharedLogic.EditObjectText(gameObject, "input1");
	}

	public void SetInput2()
	{
		sharedLogic.EditObjectText(gameObject, "input2");
	}

	public void SetInputBlock1()
	{
		sharedLogic.EditObjectText(gameObject, "inblock1");
	}

	public void SetInputBlock2()
	{
		sharedLogic.EditObjectText(gameObject, "inblock2");
	}

	public void SetOutput1()
	{
		sharedLogic.EditObjectText(gameObject, "output1");
	}

	public void SetOutputBlock1()
	{
		sharedLogic.EditObjectText(gameObject, "outblock1");
	}

	public void SetOutput2()
	{
		sharedLogic.EditObjectText(gameObject, "output2");
	}

	public void SetOutputBlock2()
	{
		sharedLogic.EditObjectText(gameObject, "outblock2");
	}

	private void UpdateLineRenderers()
	{
		UpdateLineRenderer(lineRenderer1, gameObject, inputSource1);
		UpdateLineRenderer(lineRenderer2, gameObject, inputSource2);
	}

}
