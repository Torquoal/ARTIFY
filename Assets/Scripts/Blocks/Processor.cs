using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using TMPro;

public class Processor : BaseBlock
{
    [Header("Settings")]
    [SerializeField] public GameObject inputSource;
    [SerializeField] public string input_required;
    [SerializeField] public string output;

    // UI Elements
    public TMP_Text inputLabel;
    public TMP_Text outputLabel;
    public TMP_Text inBlockLabel;

    private LineRenderer lineRenderer;


	protected override void Awake()
	{
		SaveSystem.processors.Add(this);
	}
    private void OnDestroy()
    {
        SaveSystem.processors.Remove(this);
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
        outputLabel.text = "prod: " + output;
        inBlockLabel.text = "in: " + (inputSource != null ? sharedLogic.GetBlockTitle(inputSource) : "None");
    }

    public void SetInput()
    {
        sharedLogic.EditObjectText(gameObject, "input");
    }

    public void SetOutput()
    {
        sharedLogic.EditObjectText(gameObject, "output");
    }

    public void SetInputBlock()
    {
        sharedLogic.EditObjectText(gameObject, "inblock");
    }
}
