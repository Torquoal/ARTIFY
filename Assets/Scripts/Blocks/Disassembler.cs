using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using TMPro;

public class Disassembler : BaseBlock
{
    [Header("Settings")]
	[SerializeField] public GameObject inputSource;
	[SerializeField] public string input_required;
	[SerializeField] public GameObject outputBlock1;
	[SerializeField] public string output1;
	[SerializeField] public GameObject outputBlock2;
	[SerializeField] public string output2;

	public TMP_Text inBlockLabel;
	public TMP_Text inputLabel;
	public TMP_Text output1Label;
	public TMP_Text output2Label;
	public TMP_Text outBlock1Label;
	public TMP_Text outBlock2Label;

    private LineRenderer lineRenderer;
    

    protected override void Awake()
    {
        SaveSystem.disassemblers.Add(this);
    }

    private void OnDestroy()
    {
        SaveSystem.disassemblers.Remove(this);
    }

    private void Start()
    {
        Setup();
        lineRenderer = GetComponent<LineRenderer>();
        InitializeLineRenderer(lineRenderer);
    }

    public override void Update()
    {
        SetName();
        correct = sharedLogic.CheckInput(gameObject, inputSource, correct, input_required);
        sharedLogic.ManageColour(gameObject, active, correct, grey, green, red);
        UpdateLabels();
        UpdateLineRenderer(lineRenderer, gameObject, inputSource);
    }

    public override void UpdateLabels()
    {
        base.UpdateLabels();
        inputLabel.text = "req: " + input_required;
        output1Label.text = "prod1: " + output1;
        output2Label.text = "prod2: " + output2;
        inBlockLabel.text = "in: " + (inputSource != null ? sharedLogic.GetBlockTitle(inputSource) : "None");
    }

    public void SetInput()
    {
        sharedLogic.EditObjectText(gameObject, "input");
    }

    public void SetOutput1()
    {
        sharedLogic.EditObjectText(gameObject, "output1");
    }

    public void SetOutput2()
    {
        sharedLogic.EditObjectText(gameObject, "output2");
    }

    public void SetInputBlock()
    {
        sharedLogic.EditObjectText(gameObject, "inblock");
    }
}
