using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using TMPro;

public class Destination : BaseBlock
{
    [Header("Settings")]
    [SerializeField] public GameObject inputSource;
    [SerializeField] public string input_required;

    // UI Elements
    public TMP_Text inputLabel;
    public TMP_Text inBlockLabel;

    private LineRenderer lineRenderer;


    protected override void Awake()
    {
        SaveSystem.destinations.Add(this);
    }

    protected void OnDestroy()
    {
        SaveSystem.destinations.Remove(this);
    }

    protected void Start()
    {
        Setup();
        lineRenderer = GetComponent<LineRenderer>();
        InitializeLineRenderer(lineRenderer);
    }

    public override void Update()
    {
        SetName();
        correct = sharedLogic.CheckInput(gameObject, inputSource, correct, input_required);
        UpdatePanelColor();
        UpdateLabels();
        UpdateLineRenderer(lineRenderer, gameObject, inputSource);
    }

    public override void UpdateLabels()
    {
        base.UpdateLabels();
        inputLabel.text = "req: " + input_required;
        inBlockLabel.text = "in: " + (inputSource != null ? sharedLogic.GetBlockTitle(inputSource) : "None");
    }

    public void SetInput()
    {
        sharedLogic.EditObjectText(gameObject, "input");
    }

    public void SetInputBlock()
    {
        sharedLogic.EditObjectText(gameObject, "inblock");
    }
}
