using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using TMPro;

public class Disassembler : MonoBehaviour
{

	public string title;

	public GameObject inputSource;
	public string input_required;
	public GameObject outputBlock1;
	public string output1;
	public GameObject outputBlock2;
	public string output2;
	
	public bool active = true; // is the component currently turned on
	public bool correct = false; // does the component have the correct input

	public Material red;
	public Material green;
	public Material grey;

	private SharedLogic sharedLogic;

	public GameObject editMenu;
	public TMP_Text titleLabel;
	public TMP_Text inputLabel;
	public TMP_Text inBlockLabel;
	public TMP_Text output1Label;
	public TMP_Text output2Label;
	public TMP_Text outBlock1Label;
	public TMP_Text outBlock2Label;
	



	// Start is called before the first frame update
	void Start()
    {
		sharedLogic = GameObject.FindWithTag("ScriptHost").GetComponent<SharedLogic>();
		Transform child = transform.Find("TitleCanvas");
		TMP_Text t = child.GetComponent<TMP_Text>();
		t.text = title;
	}

	// Update is called once per frame
	void Update()
    {
		SetName();
		correct = sharedLogic.CheckInput(gameObject, inputSource, correct, input_required);
		sharedLogic.ManageColour(gameObject, active, correct, grey, green, red);
		titleLabel.text = title;
		inputLabel.text = "req: " + input_required;
		inBlockLabel.text = "in: " + sharedLogic.GetBlockTitle(inputSource);
		output1Label.text = "prod1: " + output1;
		output2Label.text = "prod2: " + output2;
		outBlock1Label.text = "out1: " + sharedLogic.GetBlockTitle(outputBlock1);
		outBlock2Label.text = "out2: " + sharedLogic.GetBlockTitle(outputBlock2);
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


	public void SetInput()
    {
		sharedLogic.EditObjectText(gameObject, "input");
	}

	public void SetInputBlock()
    {
		sharedLogic.EditObjectText(gameObject, "inblock");
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
