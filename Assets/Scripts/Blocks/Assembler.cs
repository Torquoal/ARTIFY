using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using TMPro;

public class Assembler : BaseBlock
{
    [Header("Settings")]
    [SerializeField] public GameObject inputSource1;
    [SerializeField] public string input_required1;
    [SerializeField] public GameObject inputSource2;
    [SerializeField] public string input_required2;
    [SerializeField] public string output;

    // UI Elements
    public TMP_Text inBlock1Label;
    public TMP_Text inBlock2Label;
    public TMP_Text input1Label;
    public TMP_Text input2Label;
    public TMP_Text outputLabel;

    private LineRenderer lineRenderer1;
    private LineRenderer lineRenderer2;


    protected override void Awake()
    {
        SaveSystem.assemblers.Add(this);
    }

    private void OnDestroy()
    {
        SaveSystem.assemblers.Remove(this);
    }

    private void Start()
    {
        Setup();
        InitializeLineRenderers();
    }

    private void InitializeLineRenderers()
    {
        // Create first line renderer
        var lineObj1 = new GameObject("LineRenderer1");
        lineObj1.transform.parent = transform;
        lineRenderer1 = lineObj1.AddComponent<LineRenderer>();
        InitializeLineRenderer(lineRenderer1);

        // Create second line renderer
        var lineObj2 = new GameObject("LineRenderer2");
        lineObj2.transform.parent = transform;
        lineRenderer2 = lineObj2.AddComponent<LineRenderer>();
        InitializeLineRenderer(lineRenderer2);
    }

    public override void Update()
    {
        base.Update();
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
        outputLabel.text = "prod: " + output;
        inBlock1Label.text = "in1: " + (inputSource1 != null ? sharedLogic.GetBlockTitle(inputSource1) : "None");
        inBlock2Label.text = "in2: " + (inputSource2 != null ? sharedLogic.GetBlockTitle(inputSource2) : "None");
    }

    private void UpdateLineRenderers()
    {
        UpdateLineRenderer(lineRenderer1, gameObject, inputSource1);
        UpdateLineRenderer(lineRenderer2, gameObject, inputSource2);
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
					//Debug.Log("Assembler input incorrect");
                } else if (currentInputObject == inputSource1)
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

}
