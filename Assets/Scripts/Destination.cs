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
	public GameObject editMenu;
	public TMP_Text titleLabel;
	public TMP_Text inputLabel;
	public TMP_Text inBlockLabel;



	// Start is called before the first frame update
	void Start()
	{
		sharedLogic = GameObject.FindWithTag("ScriptHost").GetComponent<SharedLogic>();
		Transform child = transform.Find("TitleCanvas");
		TMP_Text t = child.GetComponent<TMP_Text>();
		t.text =  title;
	}

	// Update is called once per frame
	void Update()
	{
		SetName();
		correct = sharedLogic.CheckInput(gameObject, inputSource, correct, input_required);
		sharedLogic.ManageColour(gameObject, active, correct, grey, green, red);
		titleLabel.text = title;
		inputLabel.text = "req: " + input_required;
		if(inputSource!=null) inBlockLabel.text = "in: " + sharedLogic.GetBlockTitle(inputSource);
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
