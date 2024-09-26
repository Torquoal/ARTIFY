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

	[Header("Settings")]
	[SerializeField] public string title;
	[SerializeField] public GameObject inputSource;
	[SerializeField] public string input_required;
	[SerializeField] public GameObject outputBlock1;
	[SerializeField] public string output1;
	[SerializeField] public GameObject outputBlock2;
	[SerializeField] public string output2;

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

	private LineRenderer lineRenderer;

	void Awake()
	{
		SaveSystem.disassemblers.Add(this);
	}

	void OnDestroy()
	{
		Debug.Log(this.title + "deleted");
		SaveSystem.disassemblers.Remove(this);

	}


	// Start is called before the first frame update
	void Start()
	{
		sharedLogic = GameObject.FindWithTag("ScriptHost").GetComponent<SharedLogic>();
		Transform child = transform.Find("TitleCanvas");
		TMP_Text t = child.GetComponent<TMP_Text>();
		t.text = title;
		// Get the LineRenderer component attached to this GameObject
		lineRenderer = GetComponent<LineRenderer>();
		// Set the number of positions to 2 (start and end points)
		lineRenderer.positionCount = 2;

	}

	// Update is called once per frame
	void Update()
	{

		SetName();
		correct = sharedLogic.CheckInput(gameObject, inputSource, correct, input_required);
		sharedLogic.ManageColour(gameObject, active, correct, grey, green, red);
		titleLabel.text = title;
		inputLabel.text = "req: " + input_required;
		output1Label.text = "prod1: " + output1;
		output2Label.text = "prod2: " + output2;
		if (inputSource != null)
		{
			inBlockLabel.text = "in: " + sharedLogic.GetBlockTitle(inputSource);
			// Update the positions of the LineRenderer to connect the blocks
			lineRenderer.SetPosition(0, transform.position);             // Start at this block's position
			lineRenderer.SetPosition(1, inputSource.transform.position); // End at the InputSource block's position
		}
		else
		{
			inBlockLabel.text = "in: None";
			// If InputSource is null, disable the line by setting both positions to the same point
			lineRenderer.SetPosition(0, transform.position);
			lineRenderer.SetPosition(1, transform.position);
		}

		if (outputBlock1 != null)
		{
			outBlock1Label.text = "out1: " + sharedLogic.GetBlockTitle(outputBlock1);
		}
		else
		{
			outBlock1Label.text = "out1: None";
		}
		if (outputBlock2 != null)
		{
			outBlock2Label.text = "out2: " + sharedLogic.GetBlockTitle(outputBlock2);
		}
		else
		{
			outBlock2Label.text = "out2: None";
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
	public void Actuate()
	{
		active = !active;
	}

	public void ToggleEditMenu()
	{
		editMenu.SetActive(!editMenu.activeInHierarchy);
	}

}
