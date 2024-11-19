using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using TMPro;

public class Source : BaseBlock
{
    [Header("Settings")]
    [SerializeField] public string output;
    
    public TMP_Text outputLabel;


    protected override void Awake()
    {
        SaveSystem.sources.Add(this);
    }

    private void OnDestroy()
    {
        SaveSystem.sources.Remove(this);
    }

    private void Start()
    {
        Setup();

        player = GameObject.FindWithTag("MainCamera");
        Debug.Log("HEREEEEEEEEEEEEEEEEEE" + player);
    }

	public void FindShape(){
        shape = FindChildByName("Shape", transform);
    }

    public override void Update()
    {
        SetName();
        correct = active;
        UpdateLabels();
        UpdatePanelColor();
	}

    public override void UpdateLabels()
    {
        base.UpdateLabels();
        outputLabel.text = "prod: " + output;
    }

    public void SetOutput()
    {
        sharedLogic.EditObjectText(gameObject, "output");
    }

	[ContextMenu("Actuate")]
	public override void Actuate()
	{
    	active = !active;
    	correct = active;
    	UpdatePanelColor();
    }
}
